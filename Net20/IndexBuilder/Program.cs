using IndexBuilder.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace IndexBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseUrl = Settings.Default.BaseIndexUrl;
            string descriptionIndexName = Settings.Default.DescriptionIndexName;
            string relationshipIndexName = Settings.Default.RelationshipIndexName;
            string clinicalFindingsIndexName = Settings.Default.ClinicalFindingIndexName;
            string findingSiteIndexName = Settings.Default.FindingSiteIndexName;
            string entity = Settings.Default.Entity;
            int delimiterCode = Settings.Default.DelimiterCode;

            string descriptionsFilePath = Settings.Default.DescriptionsFilePath;
            string relationshipsFilePath = Settings.Default.RelationshipsFilePath;

            Stopwatch mainWatch = new Stopwatch();
            mainWatch.Start();

            // TODO: create the descriptions index
            CreateIndex(descriptionIndexName, entity, baseUrl, descriptionsFilePath, delimiterCode);

            mainWatch.Stop();
            Console.WriteLine("Index builder process completed successfully.");
            Console.WriteLine(string.Format("Total time elapsed: {0}:{1}:{2}", mainWatch.Elapsed.Hours, mainWatch.Elapsed.Minutes, mainWatch.Elapsed.Seconds));
            Console.ReadLine();
        }

        private static void CreateIndex(string indexName, string entity, string baseUrl, string filePath, int delimiterCode)
        {
            // read from headers
            string headerLine = string.Empty;
            using (var fileStream = File.OpenText(filePath))
            {
                headerLine = fileStream.ReadLine();

                string[] headers = headerLine.Split((char)delimiterCode);

                // delete index
                var deleteRequest = (HttpWebRequest)WebRequest.Create(string.Format("{0}/{1}", baseUrl, indexName));
                deleteRequest.Method = "DELETE";
                deleteRequest.ContentType = "application/x-www-form-urlencoded";
                deleteRequest.Accept = "application/json";
                

                using(var requestStream = deleteRequest.GetRequestStream())
                {
                    byte[] dataBytes = Encoding.UTF8.GetBytes("");
                    requestStream.Write(dataBytes,0,dataBytes.Length);
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
                }

                // create index
                var createRequest = (HttpWebRequest)WebRequest.Create(string.Format("{0}/{1}",baseUrl,indexName));
                createRequest.Method = "PUT";
                createRequest.ContentType = "application/x-www-form-urlencoded";
                createRequest.Accept = "application/json";

                StringBuilder propertyBuilder = new StringBuilder();
                for(var i = 0; i < headers.Length; i++)
                {
                    propertyBuilder.Append(string.Format("\"{0}\" : {{ \"type\" : \"string\" }}", headers[i]));
                    if ((i + 1) < headers.Length)
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
                }

                //string recordLine = string.Empty;
                //while ((recordLine = fileStream.ReadLine()) != null)
                //{
                //    HttpWebRequest dataRequest = (HttpWebRequest)WebRequest.Create()
                //}


            }

            

        }

        private static void CreateIndex(string indexName, string entity, string baseUrl, string[] properties)
        {
            throw new NotImplementedException();
        }
    }
}
