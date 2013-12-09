SnomedClinicalFindingIndexBuilder
=================================

SNOMED releases do not have groupings of concepts per attributes. Clinical Finding attributes should have valid concepts assigned to them. For the user to be able to enter correct concept, SNOMED search should return the corresponding valid concepts for the attribute.

System Requirements:
1. .NET 2.0
2. Visual Studio 2013 (you may use previous VS versions)

How to use:

Getting and installing ElasticSearch
1. You are required to install Java to be able to run ElasticSearch. Get the latest Java from https://java.com/en/download/index.jsp. Install Java.
2. Download ElasticSearch from this link. http://www.elasticsearch.org/download/. Select what suits you.
3. Extract download to a location you prefer.
4. Create a system environment variable named JAVA_HOME that points to the Java bin directory. Example (C:\Program Files (x86)\Java\jre7)
5. Run ElasticSearch but going to the ElasticSearch extract folder. Go to the bin folder and run elasticsearch.bat file. Example (C:\elasticsearch-0.90.5\bin\elasticsearch.bat)

For more information on ElasticSearch setup, proceed to http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/setup.html

Building the Project
1. Download the ZIP file from GitHub at https://github.com/jerbersoft/SnomedClinicalFindingIndexBuilder/archive/master.zip.
2. Extract the contents to your preferred location.
3. Build the project.

Getting SNOMED data
1. SNOMED data is not included in this project as it is a standard dataset that requires registration. For more information, please proceed to this website. http://www.ihtsdo.org/snomed-ct/.
2. If you have access to a SNOMED release download, please download the version you require. A SNOMED release file may look like "SnomedCT_Release_INT_20130731.zip".
3. Locate the 3 SNOMED files we need. Files are located in <folder>\SnomedCT_Release_INT_20130731\RF2Release\Snapshot\Terminology.
4. Please the following files to C:\Data (this can be set at the project config file):
    a. sct2_Concept_Snapshot_INT_20130731.txt
    b. sct2_Description_Snapshot-en_INT_20130731.txt
    c. sct2_Relationship_Snapshot_INT_20130731.txt

Running the Project.
1. We need to update ElasticSearch setting to accept a LOT of JSON data.
2. Proceed to <elasticsearch extract folder>\config\ and open elasticsearch.yml in your text editor.
3. Add this line in the file: index.query.bool.max_clause_count: "1000000"
4. Proceed to project config file (App.config) and be sure you update the following Settings to align with the files in C:\Data\ folder:
    a. ConceptsFilePath = sct2_Concept_Snapshot_INT_20130731.txt
    b. DescriptionsFilePath = sct2_Description_Snapshot-en_INT_20130731.txt
    c. RelationshipsFilePath = sct2_Relationship_Snapshot_INT_20130731.txt
5. Run the project.

Apologies if some of the steps are not clear as I have updated this in a hurry. Let me know if you have questions. Email me at herbertsabanal at gmail dot com.



