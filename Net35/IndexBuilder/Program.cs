﻿using IndexBuilder.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;

using AllowableValuesEnum = IndexBuilder.ElasticSearchUtility.AllowableValuesEnum;
using Newtonsoft.Json;
using IndexBuilder.Dtos;

namespace IndexBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseUrl = Settings.Default.BaseIndexUrl;
            string conceptIndexName = Settings.Default.ConceptIndexName;
            string descriptionIndexName = Settings.Default.DescriptionIndexName;
            string relationshipIndexName = Settings.Default.RelationshipIndexName;
            string clinicalFindingsIndexName = Settings.Default.ClinicalFindingIndexName;
            string findingSiteIndexName = Settings.Default.FindingSiteIndexName;
            string associatedMorphologyIndexName = Settings.Default.AssociatedMorphologyIndexName;
            string associatedWithIndexName = Settings.Default.AssociatedWithIndexName;
            string causativeAgentIndexName = Settings.Default.CausativeAgentIndexName;
            string dueToIndexName = Settings.Default.DueToIndexName;
            string afterIndexName = Settings.Default.AfterIndexName;
            string severityIndexName = Settings.Default.SeverityIndexName;
            string clinicalCourseIndexName = Settings.Default.ClinicalCourseIndexName;
            string episodicityIndexName = Settings.Default.EpisodicityIndexName;
            string interpretsIndexName = Settings.Default.InterpretsIndexName;
            string hasInterpretationIndexName = Settings.Default.HasInterpritationIndexName;
            string pathologicalProcessIndexName = Settings.Default.PathologicalProcessIndexName;
            string hasDefinitionalManifestationIndexName = Settings.Default.HasDefinitionalManifestationIndexName;
            string occurenceIndexName = Settings.Default.OccurenceIndexName;
            string findingMethodIndexName = Settings.Default.FindingMethodIndexName;
            string findingInformerIndexName = Settings.Default.FindingInformerIndexName;
            string entity = Settings.Default.Entity;
            int delimiterCode = Settings.Default.DelimiterCode;
            bool buildConcepts = Settings.Default.BuildConcepts;
            bool buildDescriptions = Settings.Default.BuildDescriptions;
            bool buildRelationships = Settings.Default.BuildRelationships;
            bool buildClinicalFindings = Settings.Default.BuildClinicalFindings;
            bool buildFindingSites = Settings.Default.BuildFindingSites;
            bool buildAssociatedMorphologies = Settings.Default.BuildAssociatedMorphologies;
            bool buildAssociatedWith = Settings.Default.BuildAssociatedWith;
            bool buildCausativeAgents = Settings.Default.BuildCausativeAgents;
            bool buildDueTos = Settings.Default.BuildDueTos;
            bool buildAfters = Settings.Default.BuildAfters;
            bool buildSeverities = Settings.Default.BuildSeverities;
            bool buildClinicalCourses = Settings.Default.BuildClinicalCourses;
            bool buildEpisodities = Settings.Default.BuildEpisodicities;
            bool buildInterprets = Settings.Default.BuildInterprets;
            bool buildHasInterpretations = Settings.Default.BuildHasInterpretations;
            bool buildPathologicalProcesses = Settings.Default.BuildPathologicalProcesses;
            bool buildHasDefinitionalManifestations = Settings.Default.BuildHasDefinitionalManifestations;
            bool buildOccurences = Settings.Default.BuildOccurences;
            bool buildFindingMethods = Settings.Default.BuildFindingMethods;
            bool buildFindingInformers = Settings.Default.BuildFindingInformers;

            string conceptsFilePath = Settings.Default.ConceptsFilePath;
            string descriptionsFilePath = Settings.Default.DescriptionsFilePath;
            string relationshipsFilePath = Settings.Default.RelationshipsFilePath;

            Stopwatch mainWatch = new Stopwatch();
            mainWatch.Start();

            string conceptsIndexSearchUrl = baseUrl + "/" + conceptIndexName + "/" + entity + "/_search";
            string conceptsIndexUrl = baseUrl + "/" + conceptIndexName + "/" + entity;
            string descriptionsIndexUrl = baseUrl + "/" + descriptionIndexName + "/" + entity;
            string relationshipsIndexUrl = baseUrl + "/" + relationshipIndexName + "/" + entity;

            // create the concept index
            if (buildConcepts)
                BuildIndexFromFile(conceptIndexName, entity, baseUrl, conceptsFilePath, delimiterCode, null, new string[] {});

            // create the descriptions index
            if (buildDescriptions)
                BuildIndexFromFile(descriptionIndexName, entity, baseUrl, descriptionsFilePath, delimiterCode, conceptsIndexSearchUrl, new string[] { "conceptId" });

            // build the relationships index
            if (buildRelationships)
                BuildIndexFromFile(relationshipIndexName, entity, baseUrl, relationshipsFilePath, delimiterCode, conceptsIndexSearchUrl, new string[] { "sourceId", "destinationId" });

            if (buildClinicalFindings)
            {
                CreateIndex(clinicalFindingsIndexName, entity, baseUrl);
                BuildIndexFromIndex(clinicalFindingsIndexName, entity, baseUrl, descriptionsIndexUrl, relationshipsIndexUrl, new string[] { "404684003" }, AllowableValuesEnum.ThisCodeAndDescendants);
            }

            if (buildFindingSites)
            {
                CreateIndex(findingSiteIndexName, entity, baseUrl);
                BuildIndexFromIndex(findingSiteIndexName, entity, baseUrl, descriptionsIndexUrl, relationshipsIndexUrl, new string[] { "442083009" }, AllowableValuesEnum.ThisCodeAndDescendants);
            }

            if (buildAssociatedMorphologies)
            {
                CreateIndex(associatedMorphologyIndexName, entity, baseUrl);
                BuildIndexFromIndex(associatedMorphologyIndexName, entity, baseUrl, descriptionsIndexUrl, relationshipsIndexUrl, new string[] { "442083009" }, AllowableValuesEnum.ThisCodeAndDescendants);
            }

            if (buildAssociatedWith)
            {
                CreateIndex(associatedWithIndexName, entity, baseUrl);
                BuildIndexFromIndex(associatedWithIndexName, entity, baseUrl, descriptionsIndexUrl, relationshipsIndexUrl, new string[] { "404684003", "71388002", "272379006", "410607006", "105590001", "260787004", "78621006" }, AllowableValuesEnum.ThisCodeAndDescendants);
                BuildIndexFromIndex(associatedWithIndexName, entity, baseUrl, descriptionsIndexUrl, relationshipsIndexUrl, new string[] { "138875005" }, AllowableValuesEnum.ThisCodeOnly);
                BuildIndexFromIndex(associatedWithIndexName, entity, baseUrl, descriptionsIndexUrl, relationshipsIndexUrl, new string[] { "373873005" }, AllowableValuesEnum.DescendantsOnlyAndOnlyAllowedInQualifyingRelationship);
            }

            if (buildCausativeAgents)
            {
                CreateIndex(causativeAgentIndexName, entity, baseUrl);
                BuildIndexFromIndex(causativeAgentIndexName, entity, baseUrl, descriptionsIndexUrl, relationshipsIndexUrl, new string[] { "410607006", "105590001", "260787004", "78621006" }, AllowableValuesEnum.ThisCodeAndDescendants);
                BuildIndexFromIndex(causativeAgentIndexName, entity, baseUrl, descriptionsIndexUrl, relationshipsIndexUrl, new string[] { "138875005" }, AllowableValuesEnum.ThisCodeOnly);
                BuildIndexFromIndex(causativeAgentIndexName, entity, baseUrl, descriptionsIndexUrl, relationshipsIndexUrl, new string[] { "373873005" }, AllowableValuesEnum.DescendantsOnlyAndOnlyAllowedInQualifyingRelationship);
            }

            if (buildDueTos)
            {
                CreateIndex(dueToIndexName, entity, baseUrl);
                BuildIndexFromIndex(dueToIndexName, entity, baseUrl, descriptionsIndexUrl, relationshipsIndexUrl, new string[] { "404684003", "272379006" }, AllowableValuesEnum.DescendantsOnlyExceptForSupercategoryGroupers);
            }

            if (buildAfters)
            {
                CreateIndex(afterIndexName, entity, baseUrl);
                BuildIndexFromIndex(afterIndexName, entity, baseUrl, descriptionsIndexUrl, relationshipsIndexUrl, new string[] { "404684003", "71388002" }, AllowableValuesEnum.ThisCodeAndDescendants);
            }

            if (buildSeverities)
            {
                CreateIndex(severityIndexName, entity, baseUrl);
                BuildIndexFromIndex(severityIndexName, entity, baseUrl, descriptionsIndexUrl, relationshipsIndexUrl, new string[] { "272141005" }, AllowableValuesEnum.DescendantsOnlyExceptForSupercategoryGroupers);
            }

            if (buildClinicalCourses)
            {
                CreateIndex(clinicalCourseIndexName, entity, baseUrl);
                BuildIndexFromIndex(clinicalCourseIndexName, entity, baseUrl, descriptionsIndexUrl, relationshipsIndexUrl, new string[] { "288524001" }, AllowableValuesEnum.DescendantsOnlyExceptForSupercategoryGroupers);
            }

            if (buildEpisodities)
            {
                CreateIndex(episodicityIndexName, entity, baseUrl);
                BuildIndexFromIndex(episodicityIndexName, entity, baseUrl, descriptionsIndexUrl, relationshipsIndexUrl, new string[] { "288526004" }, AllowableValuesEnum.DescendantsOnlyExceptForSupercategoryGroupers);
            }

            if (buildInterprets)
            {
                CreateIndex(interpretsIndexName, entity, baseUrl);
                BuildIndexFromIndex(interpretsIndexName, entity, baseUrl, descriptionsIndexUrl, relationshipsIndexUrl, new string[] { "363787002", "108252007", "386053000" }, AllowableValuesEnum.ThisCodeAndDescendants);
            }

            if (buildHasInterpretations)
            {
                CreateIndex(hasInterpretationIndexName, entity, baseUrl);
                BuildIndexFromIndex(hasInterpretationIndexName, entity, baseUrl, descriptionsIndexUrl, relationshipsIndexUrl, new string[] { "260245000" }, AllowableValuesEnum.ThisCodeAndDescendants);
            }

            if (buildPathologicalProcesses)
            {
                CreateIndex(pathologicalProcessIndexName, entity, baseUrl);
                BuildIndexFromIndex(pathologicalProcessIndexName, entity, baseUrl, descriptionsIndexUrl, relationshipsIndexUrl, new string[] { "441862004" }, AllowableValuesEnum.ThisCodeAndDescendants);
                BuildIndexFromIndex(pathologicalProcessIndexName, entity, baseUrl, descriptionsIndexUrl, relationshipsIndexUrl, new string[] { "263680009" }, AllowableValuesEnum.ThisCodeOnly);
            }

            if (buildHasDefinitionalManifestations)
            {
                CreateIndex(hasDefinitionalManifestationIndexName, entity, baseUrl);
                BuildIndexFromIndex(hasDefinitionalManifestationIndexName, entity, baseUrl, descriptionsIndexUrl, relationshipsIndexUrl, new string[] { "404684003" }, AllowableValuesEnum.ThisCodeAndDescendants);
            }

            if (buildOccurences)
            {
                CreateIndex(occurenceIndexName, entity, baseUrl);
                BuildIndexFromIndex(occurenceIndexName, entity, baseUrl, descriptionsIndexUrl, relationshipsIndexUrl, new string[] { "282032007" }, AllowableValuesEnum.DescendantsOnly);
            }

            if (buildFindingMethods)
            {
                CreateIndex(findingMethodIndexName, entity, baseUrl);
                BuildIndexFromIndex(findingInformerIndexName, entity, baseUrl, descriptionsIndexUrl, relationshipsIndexUrl, new string[] { "71388002" }, AllowableValuesEnum.DescendantsOnlyExceptForSupercategoryGroupers);
            }

            if (buildFindingInformers)
            {
                CreateIndex(findingInformerIndexName, entity, baseUrl);
                BuildIndexFromIndex(findingInformerIndexName, entity, baseUrl, descriptionsIndexUrl, relationshipsIndexUrl, new string[] { "420158005", "419358007" }, AllowableValuesEnum.ThisCodeAndDescendants);
            }

            mainWatch.Stop();
            Console.WriteLine("Index builder process completed successfully.");
            Console.WriteLine(string.Format("Total time elapsed: {0}:{1}:{2}", mainWatch.Elapsed.Hours, mainWatch.Elapsed.Minutes, mainWatch.Elapsed.Seconds));
            Console.ReadLine();
        }

        private static void BuildIndexFromFile(string indexName, string entity, string baseUrl, string filePath, int delimiterCode, string conceptsIndexUrl, string[] conceptIdColumnNames)
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
            using (var fileStream = File.OpenText(filePath))
            {
                var headerLine = fileStream.ReadLine();
                headerNames = headerLine.Split((char)delimiterCode);
            }
            ElasticSearchUtility.CreateIndex(indexName, entity, baseUrl, headerNames);

            // add data
            Console.WriteLine(string.Format("Will now add data to index \"{0}\"", indexName));
            AddIndexDataFromFile(filePath, headerNames, baseUrl, indexName, entity, (char)delimiterCode, conceptsIndexUrl, conceptIdColumnNames);

            indexWatch.Stop();
            Console.WriteLine(string.Format("Finished building index \"{0}\"", indexName));
            Console.WriteLine(string.Format("Index build time: {0}:{1}:{2}", indexWatch.Elapsed.Hours, indexWatch.Elapsed.Minutes, indexWatch.Elapsed.Seconds));
        }

        public static void BuildIndexFromIndex(string indexName, string entity, string baseUrl, string descriptionIndexUrl, string relationshipsIndexUrl, string[] conceptIds, AllowableValuesEnum allowedValues)
        {
            Stopwatch indexWatch = new Stopwatch();
            indexWatch.Start();

            // add data
            Console.WriteLine(string.Format("Will now add data to index \"{0}\"", indexName));
            foreach (var conceptId in conceptIds)
            {
                AddIndexDataFromIndex(baseUrl, indexName, entity, descriptionIndexUrl, relationshipsIndexUrl, conceptId, allowedValues);
            }

            indexWatch.Stop();
            Console.WriteLine(string.Format("Finished building index \"{0}\"", indexName));
            Console.WriteLine(string.Format("Index build time: {0}:{1}:{2}", indexWatch.Elapsed.Hours, indexWatch.Elapsed.Minutes, indexWatch.Elapsed.Seconds));
        }

        public static void CreateIndex(string indexName, string entity, string baseUrl)
        {
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
            string[] headerNames = new string[] { "conceptId", "term", "typeId" };
            ElasticSearchUtility.CreateIndex(indexName, entity, baseUrl, headerNames);
            Console.WriteLine(string.Format("Created index \"{0}\".", indexName));
        }

        public static void AddIndexDataFromIndex(string baseUrl, string indexName, string entity, string descriptionsIndexUrl, string relationshipsIndexUrl, string conceptId, AllowableValuesEnum allowedValues)
        {
            string indexUrl = baseUrl + "/" + indexName + "/" + entity;

            var parentIds = new List<string>();
            var childrenIds = new List<string>();

            int total = 0;
            int count = 1;
            int level = 1;

            Console.WriteLine("Starting index data processing: " + level.ToString());
            Stopwatch mainWatch = new Stopwatch();
            mainWatch.Start();

            if (allowedValues == AllowableValuesEnum.ThisCodeAndDescendants || allowedValues == AllowableValuesEnum.ThisCodeOnly)
            {
                var descriptions = ElasticSearchUtility.GetDescriptions(descriptionsIndexUrl, conceptId);
                foreach (var description in descriptions)
                {
                    ElasticSearchUtility.WriteToIndex(indexUrl, conceptId, description.Term, total, description.TypeId);
                    total++;
                }
            }

            if (allowedValues != AllowableValuesEnum.ThisCodeOnly)
            {
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
                        var descriptions = ElasticSearchUtility.GetDescriptions(descriptionsIndexUrl, child);
                        foreach (var description in descriptions)
                        {
                            ElasticSearchUtility.WriteToIndex(indexUrl, child, description.Term, total, description.TypeId);
                            total++;
                            count++;
                        }
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
            }

            mainWatch.Stop();

            Console.WriteLine(string.Format("Total data added: {0}", total));
            Console.WriteLine(string.Format("Completed: {0}:{1}:{2}", mainWatch.Elapsed.Hours, mainWatch.Elapsed.Minutes, mainWatch.Elapsed.Seconds));
            Console.WriteLine("\n");
            //Console.ReadLine();
        }

        public static void AddIndexDataFromFile(string filePath, string[] headerNames, string baseUrl, string indexName, string entity, char delimiter, string conceptsIndexUrl, string[] conceptIdColumnNames)
        {
            string indexUrl = baseUrl + "/" + indexName + "/" + entity;

            using (StreamReader reader = new StreamReader(filePath))
            {
                var line = string.Empty;
                int rowIndex = 1;
                int activeColumnIndex = -1;

                // get active column\
                for (var i = 0; i < headerNames.Length; i++)
                {
                    if (headerNames[i].ToUpperInvariant().Contains("ACTIVE"))
                    {
                        activeColumnIndex = i;
                        break;
                    }
                }

                while ((line = reader.ReadLine()) != null)
                {
                    if (rowIndex > 1)
                    {
                        string[] rowValues = line.Split(delimiter);

                        // if current row is not active, skip it
                        if (rowValues[activeColumnIndex] != "1")
                            continue;

                        List<string> conceptIds = new List<string>();
                        foreach (var conceptColumn in conceptIdColumnNames)
                        {
                            for (int i = 0; i < headerNames.Length; i++)
                            {
                                if (headerNames[i] == conceptColumn)
                                {
                                    conceptIds.Add(rowValues[i]);
                                    break;
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(conceptsIndexUrl))
                        {
                            bool isActiveConcept = true;

                            foreach (var conceptId in conceptIds)
                            {
                                var conceptRequest = (HttpWebRequest)WebRequest.Create(conceptsIndexUrl);
                                conceptRequest.Method = "POST";
                                string conceptDataString = "{ \"query\" : {\"match\" : { \"id\" : \"" + conceptId + "\" }}}";
                                byte[] conceptData = Encoding.UTF8.GetBytes(conceptDataString);
                                conceptRequest.ContentType = "application/x-www-form-urlencoded";
                                conceptRequest.Accept = "application/json";
                                conceptRequest.ContentLength = conceptData.Length;

                                using (var stream = conceptRequest.GetRequestStream())
                                {
                                    stream.Write(conceptData, 0, conceptData.Length);
                                }

                                try
                                {
                                    using (var response = conceptRequest.GetResponse())
                                    {
                                        using (var responseStream = new StreamReader(response.GetResponseStream()))
                                        {
                                            var responseString = responseStream.ReadToEnd();
                                            var result = JsonConvert.DeserializeObject<Result>(responseString);
                                            if (result.hits.hits.Count == 0)
                                            {
                                                isActiveConcept = false;
                                                break;
                                            }
                                        }

                                    }
                                }
                                catch (Exception ex)
                                {
                                    File.AppendAllText(Path.Combine(Environment.CurrentDirectory, "log.txt"), string.Format("Index: {0}; Error occured at line: {1}.\nError: {2}\n", indexName, rowIndex, ex.Message));
                                }
                            }

                            if (!isActiveConcept)
                            {
                                Console.WriteLine("Skipped current row due to tied concept is not active.");
                                continue;
                            }
                        }

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
                            File.AppendAllText(Path.Combine(Environment.CurrentDirectory, "log.txt"), string.Format("Index: {0}; Error occured at line: {1}.\nError: {2}\n", indexName, rowIndex, ex.Message));
                        }
                    }

                    rowIndex++;
                }
            }
        }




    }
}
