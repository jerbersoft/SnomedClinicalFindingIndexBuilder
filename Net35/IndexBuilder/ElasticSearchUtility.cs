using IndexBuilder.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace IndexBuilder
{
    public class ElasticSearchUtility
    {
        public static List<string> GetChildren(string url, string relationshipUrl, string[] ids)
        {
            relationshipUrl = relationshipUrl + "/_search";

            int retrieved = 0;
            var childrenIds = new List<string>();

            var request = (HttpWebRequest)WebRequest.Create(relationshipUrl);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "application/json";

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < ids.Length; i++)
            {
                builder.Append(ids[i]);
                if (i < (ids.Length - 1))
                    builder.Append(" OR ");
            }

            var data = "{\"query\": { \"bool\": { \"must\": [ { \"query_string\": { \"default_field\": \"destinationId\", \"query\": \"" + builder.ToString() + "\" }}, { \"match\": { \"typeId\": \"116680003\" }}, { \"match\": { \"active\": 1 }}, {\"match\": { \"characteristicTypeId\": \"900000000000011006\" }} ]}},\"size\":5000000}";
            var dataBytes = Encoding.UTF8.GetBytes(data);
            request.ContentLength = dataBytes.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(dataBytes, 0, dataBytes.Length);
            }

            try
            {
                Result result = null;
                using (var response = request.GetResponse())
                {

                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        //Console.WriteLine(string.Format("Deserializing JSON response."));

                        var jsonResult = reader.ReadToEnd();
                        result = JsonConvert.DeserializeObject<Result>(jsonResult);
                        retrieved = result.hits.hits.Count;

                        //Console.WriteLine(string.Format("Number of hits received: {0}.", result.hits.hits.Count));

                        foreach (var item in result.hits.hits)
                        {
                            if (!Exists(url, item._source.sourceId))
                                childrenIds.Add(item._source.sourceId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            request = null;
            return childrenIds;
        }

        public static string GetDescription(string descriptionUrl, string conceptId)
        {
            descriptionUrl = descriptionUrl + "/_search";
            var description = string.Empty;

            // get description
            var descriptionRequest = (HttpWebRequest)WebRequest.Create(descriptionUrl);
            descriptionRequest.Method = "POST";
            descriptionRequest.ContentType = "application/x-www-form-urlencoded";
            descriptionRequest.Accept = "application/json";
            var descriptionQuery = "{\"query\": { \"bool\": { \"must\": [ { \"query_string\": { \"default_field\": \"conceptId\", \"query\": \"" + conceptId + "\" }}, { \"match\": { \"active\": 1 }}, {\"match\": { \"typeId\": \"900000000000003001\" }} ]}} }";
            var descriptionQueryData = Encoding.UTF8.GetBytes(descriptionQuery);
            descriptionRequest.ContentLength = descriptionQueryData.Length;
            using (var stream = descriptionRequest.GetRequestStream())
            {
                stream.Write(descriptionQueryData, 0, descriptionQueryData.Length);
            }

            try
            {
                using (var descriptionResponse = descriptionRequest.GetResponse())
                {
                    using (var descriptionReader = new StreamReader(descriptionResponse.GetResponseStream()))
                    {
                        var descriptionResultJson = descriptionReader.ReadToEnd();
                        var descriptionResult = JsonConvert.DeserializeObject<Result>(descriptionResultJson);
                        if (descriptionResult != null && descriptionResult.hits.hits.Count > 0)
                            description = descriptionResult.hits.hits[0]._source.term;
                    }
                }

                //Console.WriteLine(string.Format("Level {0}:{1} written to index. {2}", level, item._source.sourceId, count));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return description;
        }

        public static bool Exists(string url, string conceptId)
        {
            bool exists = false;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "/_search");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "application/json";

            string query = "{ \"query\": { \"match\": { \"conceptId\":\"" + conceptId + "\" }}}";

            byte[] data = Encoding.UTF8.GetBytes(query);
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            try
            {
                using (var response = request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        var result = reader.ReadToEnd();
                        exists = result.Contains(conceptId);
                    }
                }

                //Console.WriteLine(string.Format("Level {0}:{1} written to index. {2}", level, item._source.sourceId, count));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return exists;
        }

        public static void WriteToIndex(string indexUrl, string conceptId, string term, int id)
        {
            HttpWebRequest putRequest = (HttpWebRequest)WebRequest.Create(indexUrl + "/" + id.ToString());
            putRequest.Method = "PUT";
            putRequest.ContentType = "application/x-www-form-urlencoded";
            putRequest.Accept = "application/json";

            string putString = "{ \"conceptId\":\"" + conceptId + "\",\"term\":\"" + term + "\" }";

            byte[] putData = Encoding.UTF8.GetBytes(putString);
            putRequest.ContentLength = putData.Length;

            using (var stream = putRequest.GetRequestStream())
            {
                stream.Write(putData, 0, putData.Length);
            }

            try
            {
                using (var putResponse = putRequest.GetResponse())
                {
                    using (var putReader = new StreamReader(putResponse.GetResponseStream()))
                    {
                        var putResult = putReader.ReadToEnd();
                        if (putResult.Contains("\"ok\":true") == false)
                        {
                            throw new Exception("Response not good.");
                        }
                    }
                }

                //Console.WriteLine(string.Format("Level {0}:{1} written to index. {2}", level, item._source.sourceId, count));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void CreateIndex(string indexName, string entity, string baseUrl, string[] properties)
        {
            // create index
            var createRequest = (HttpWebRequest)WebRequest.Create(string.Format("{0}/{1}", baseUrl, indexName));
            createRequest.Method = "PUT";
            createRequest.ContentType = "application/x-www-form-urlencoded";
            createRequest.Accept = "application/json";

            StringBuilder propertyBuilder = new StringBuilder();
            for (var i = 0; i < properties.Length; i++)
            {
                propertyBuilder.Append(string.Format("\"{0}\" : {{ \"type\" : \"string\" }}", properties[i]));
                if ((i + 1) < properties.Length)
                {
                    propertyBuilder.Append(",");
                }
            }

            string createString = "{ \"mappings\" : { \"" + entity + "\" : { \"properties\" : { " + propertyBuilder.ToString() + " }}}} ";
            byte[] createStringBytes = Encoding.UTF8.GetBytes(createString);
            createRequest.ContentLength = createStringBytes.Length;

            using (var requestStream = createRequest.GetRequestStream())
            {
                requestStream.Write(createStringBytes, 0, createStringBytes.Length);
            }

            try
            {
                using (var createResponse = createRequest.GetResponse())
                {
                    using (var reader = new StreamReader(createResponse.GetResponseStream()))
                    {
                        string responseText = reader.ReadToEnd();
                        // TODO : check if response is ok
                    }
                }
            }
            catch (WebException webEx)
            {
                Console.WriteLine(webEx.Message);
                throw webEx;
            }
        }


        public static void DeleteIndex(string indexName, string baseUrl)
        {
            // delete index
            var deleteRequest = (HttpWebRequest)WebRequest.Create(string.Format("{0}/{1}", baseUrl, indexName));
            deleteRequest.Method = "DELETE";
            deleteRequest.ContentType = "application/x-www-form-urlencoded";
            deleteRequest.Accept = "application/json";


            using (var requestStream = deleteRequest.GetRequestStream())
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes("");
                requestStream.Write(dataBytes, 0, dataBytes.Length);
            }

            try
            {
                using (var response = deleteRequest.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        string text = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException webEx)
            {
                Console.WriteLine(webEx.Message);
                throw webEx;
            }
        }

        public static void WriteToService()
        {

        }

        public enum AllowableValuesEnum
        {
            None = 0,
            ThisCodeAndDescendants = 1,                                     // <<
            DescendantsOnly = 2,                                            // <
            DescendantsOnlyExceptForSupercategoryGroupers = 3,              // <=
            ThisCodeOnly = 4,                                               // ==
            DescendantsOnlyWhenQualifyingRelationship = 5,                  // < Q
            DescendantsOnlyAndOnlyAllowedInQualifyingRelationship = 6       // < Q Only
        }
    }
}
