using IndexBuilder.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;

using AllowableValuesEnum = IndexBuilder.ElasticSearchUtility.AllowableValuesEnum;

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

            // create the descriptions index
            //BuildIndexFromFile(descriptionIndexName, entity, baseUrl, descriptionsFilePath, delimiterCode);

            // build the relationships index
            //BuildIndexFromFile(relationshipIndexName, entity, baseUrl, relationshipsFilePath, delimiterCode);

            // build clinical findings index
            string descriptionsIndexUrl = baseUrl + "/" + descriptionIndexName + "/" + entity;
            string relationshipsIndexUrl = baseUrl + "/" + relationshipIndexName + "/" + entity;
            BuildIndexFromIndex(clinicalFindingsIndexName, entity, baseUrl, descriptionsIndexUrl, relationshipsIndexUrl, "404684003",AllowableValuesEnum.ThisCodeAndDescendants);

            mainWatch.Stop();
            Console.WriteLine("Index builder process completed successfully.");
            Console.WriteLine(string.Format("Total time elapsed: {0}:{1}:{2}", mainWatch.Elapsed.Hours, mainWatch.Elapsed.Minutes, mainWatch.Elapsed.Seconds));
            Console.ReadLine();
        }

        private static void BuildIndexFromFile(string indexName, string entity, string baseUrl, string filePath, int delimiterCode)
        {
            Stopwatch indexWatch = new Stopwatch();
            indexWatch.Start();

            Console.WriteLine(string.Format("Building of \"index {0}\" started", indexName));

            // delete index
            Console.WriteLine(string.Format("Trying to delete index \"{0}\"", indexName));
            try
            {
                ElasticSearchUtility.DeleteIndex(indexName, baseUrl);
            }
            catch (WebException ex)
            {
                Console.WriteLine(string.Format("An error has occured trying to delete index \"{0}\"", indexName));
                Console.WriteLine(ex.Message);
            }

            // create index
            Console.WriteLine(string.Format("Creating index \"{0}\"", indexName));
            string[] headerNames;
            using(var fileStream = File.OpenText(filePath))
            {
                var headerLine = fileStream.ReadLine();
                headerNames = headerLine.Split((char)delimiterCode);
            }
            ElasticSearchUtility.CreateIndex(indexName, entity, baseUrl, headerNames);

            // add data
            Console.WriteLine(string.Format("Will now add data to index \"{0}\"", indexName));
            AddIndexDataFromFile(filePath, headerNames, baseUrl, indexName, entity, (char)delimiterCode);

            indexWatch.Stop();
            Console.WriteLine(string.Format("Finished building index \"{0}\"", indexName));
            Console.WriteLine(string.Format("Index build time: {0}:{1}:{2}", indexWatch.Elapsed.Hours, indexWatch.Elapsed.Minutes, indexWatch.Elapsed.Seconds));
        }

        public static void BuildIndexFromIndex(string indexName, string entity, string baseUrl, string descriptionIndexUrl, string relationshipsIndexUrl, string conceptId, AllowableValuesEnum allowedValues)
        {
            Stopwatch indexWatch = new Stopwatch();
            indexWatch.Start();

            Console.WriteLine(string.Format("Building of \"index {0}\" started", indexName));

            // delete index
            Console.WriteLine(string.Format("Trying to delete index \"{0}\"", indexName));
            try
            {
                ElasticSearchUtility.DeleteIndex(indexName, baseUrl);
            }
            catch (WebException ex)
            {
                Console.WriteLine(string.Format("An error has occured trying to delete index \"{0}\"", indexName));
                Console.WriteLine(ex.Message);
            }

            // create index
            string[] headerNames = new string[] { "conceptId", "term" };
            ElasticSearchUtility.CreateIndex(indexName, entity, baseUrl, headerNames);

            // add data
            Console.WriteLine(string.Format("Will now add data to index \"{0}\"", indexName));
            AddIndexDataFromIndex(baseUrl, indexName, entity, descriptionIndexUrl, relationshipsIndexUrl, conceptId, allowedValues);

            indexWatch.Stop();
            Console.WriteLine(string.Format("Finished building index \"{0}\"", indexName));
            Console.WriteLine(string.Format("Index build time: {0}:{1}:{2}", indexWatch.Elapsed.Hours, indexWatch.Elapsed.Minutes, indexWatch.Elapsed.Seconds));
        }

        public static void AddIndexDataFromIndex(string baseUrl, string indexName, string entity, string descriptionsIndexUrl, string relationshipsIndexUrl, string conceptId, AllowableValuesEnum allowedValues)
        {
            string indexUrl = baseUrl + "/" + indexName + "/" + entity;

            var parentIds = new List<string>();
            var childrenIds = new List<string>();

            int total = 1;
            int count = 1;
            int level = 1;

            Console.WriteLine("Starting level: " + level.ToString());
            Stopwatch mainWatch = new Stopwatch();
            mainWatch.Start();

            if (allowedValues == AllowableValuesEnum.ThisCodeAndDescendants || allowedValues == AllowableValuesEnum.ThisCodeOnly)
            {
                string description = ElasticSearchUtility.GetDescription(descriptionsIndexUrl, conceptId);
                ElasticSearchUtility.WriteToIndex(indexUrl, conceptId, description, total);
                total++;
            }

            parentIds.Add(conceptId);
            while (parentIds.Count > 0)
            {
                Stopwatch levelWatch = new Stopwatch();
                levelWatch.Start();

                Console.WriteLine(string.Format("\n\nStarted level {0}", level));

                count = 1;
                childrenIds = ElasticSearchUtility.GetChildren(indexUrl, relationshipsIndexUrl, parentIds.ToArray());
                foreach (var child in childrenIds)
                {
                    var description = ElasticSearchUtility.GetDescription(descriptionsIndexUrl, child);
                    ElasticSearchUtility.WriteToIndex(indexUrl, child, description, total);
                    total++;
                    count++;
                }
                string[] ids = new string[childrenIds.Count];
                childrenIds.CopyTo(ids, 0);
                parentIds.Clear();
                parentIds = ids.ToList();

                //Console.WriteLine(string.Format("Children retrieved: {0}", retrieved));
                Console.WriteLine(string.Format("Children added: {0}", count));
                //Console.WriteLine(string.Format("Children with duplicates: {0}", duplicates));


                Console.WriteLine(string.Format("Total children added so far: {0}", total));
                Console.WriteLine(string.Format("Level process complete: {0}| In {1}:{2}:{3}.", level, levelWatch.Elapsed.Hours, levelWatch.Elapsed.Minutes, levelWatch.Elapsed.Seconds));

                level++;
            }

            mainWatch.Stop();

            Console.WriteLine(string.Format("Total children added: {0}", total));
            Console.WriteLine(string.Format("Completed: {0}:{1}:{2}", mainWatch.Elapsed.Hours, mainWatch.Elapsed.Minutes, mainWatch.Elapsed.Seconds));
            Console.ReadLine();
        }

        public static void AddIndexDataFromFile(string filePath, string[] headerNames, string baseUrl, string indexName, string entity, char delimiter)
        {
            string indexUrl = baseUrl + "/" + indexName + "/" + entity;

            using (StreamReader reader = new StreamReader(filePath))
            {
                var line = string.Empty;
                int rowIndex = 1;

                while ((line = reader.ReadLine()) != null)
                {
                    if (rowIndex > 1)
                    {
                        string[] rowValues = line.Split(delimiter);

                        StringBuilder row = new StringBuilder();

                        row.Append("{");

                        for (var i = 0; i < headerNames.Length; i++)
                        {
                            //int columnIndex = headers.IndexOf(columns[i]);
                            row.Append("\"" + headerNames[i] + "\": \"" + rowValues[i] + "\"");
                            if (i < headerNames.Length - 1)
                            {
                                row.Append(",");
                            }
                        }

                        row.Append("}");

                        // sent to REST service
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(indexUrl);
                        request.Method = "POST";
                        string dataString = row.ToString();
                        byte[] data = Encoding.UTF8.GetBytes(dataString);
                        request.ContentType = "application/x-www-form-urlencoded";
                        request.Accept = "application/json";
                        request.ContentLength = data.Length;

                        using (var stream = request.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                        }

                        try
                        {
                            using (var response = request.GetResponse())
                            {
                                // TODO : get actual response
                                Console.WriteLine(rowIndex);
                            }
                        }
                        catch (Exception ex)
                        {
                            File.AppendAllText(Path.Combine(Environment.CurrentDirectory, "log.txt"), string.Format("Index: {0}; Error occured at line: {1}.\n", indexName, rowIndex));
                        }
                    }

                    rowIndex++;
                }
            }
        }

        


    }
}
