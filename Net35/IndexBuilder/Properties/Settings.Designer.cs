﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IndexBuilder.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost:9200")]
        public string BaseIndexUrl {
            get {
                return ((string)(this["BaseIndexUrl"]));
            }
            set {
                this["BaseIndexUrl"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("descriptions")]
        public string DescriptionIndexName {
            get {
                return ((string)(this["DescriptionIndexName"]));
            }
            set {
                this["DescriptionIndexName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("relationships")]
        public string RelationshipIndexName {
            get {
                return ((string)(this["RelationshipIndexName"]));
            }
            set {
                this["RelationshipIndexName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("clinicalfinding")]
        public string ClinicalFindingIndexName {
            get {
                return ((string)(this["ClinicalFindingIndexName"]));
            }
            set {
                this["ClinicalFindingIndexName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("findingsite")]
        public string FindingSiteIndexName {
            get {
                return ((string)(this["FindingSiteIndexName"]));
            }
            set {
                this["FindingSiteIndexName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("concept")]
        public string Entity {
            get {
                return ((string)(this["Entity"]));
            }
            set {
                this["Entity"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("c:\\data\\descriptions.txt")]
        public string DescriptionsFilePath {
            get {
                return ((string)(this["DescriptionsFilePath"]));
            }
            set {
                this["DescriptionsFilePath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("c:\\data\\relationships.txt")]
        public string RelationshipsFilePath {
            get {
                return ((string)(this["RelationshipsFilePath"]));
            }
            set {
                this["RelationshipsFilePath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("9")]
        public int DelimiterCode {
            get {
                return ((int)(this["DelimiterCode"]));
            }
            set {
                this["DelimiterCode"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("associatedmorphology")]
        public string AssociatedMorphologyIndexName {
            get {
                return ((string)(this["AssociatedMorphologyIndexName"]));
            }
            set {
                this["AssociatedMorphologyIndexName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("associatedwith")]
        public string AssociatedWithIndexName {
            get {
                return ((string)(this["AssociatedWithIndexName"]));
            }
            set {
                this["AssociatedWithIndexName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("causativeagent")]
        public string CausativeAgentIndexName {
            get {
                return ((string)(this["CausativeAgentIndexName"]));
            }
            set {
                this["CausativeAgentIndexName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("dueto")]
        public string DueToIndexName {
            get {
                return ((string)(this["DueToIndexName"]));
            }
            set {
                this["DueToIndexName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("after")]
        public string AfterIndexName {
            get {
                return ((string)(this["AfterIndexName"]));
            }
            set {
                this["AfterIndexName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("severity")]
        public string SeverityIndexName {
            get {
                return ((string)(this["SeverityIndexName"]));
            }
            set {
                this["SeverityIndexName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("clinicalcourse")]
        public string ClinicalCourseIndexName {
            get {
                return ((string)(this["ClinicalCourseIndexName"]));
            }
            set {
                this["ClinicalCourseIndexName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("episodicity")]
        public string EpisodicityIndexName {
            get {
                return ((string)(this["EpisodicityIndexName"]));
            }
            set {
                this["EpisodicityIndexName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("interprets")]
        public string InterpretsIndexName {
            get {
                return ((string)(this["InterpretsIndexName"]));
            }
            set {
                this["InterpretsIndexName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("hasinterpretation")]
        public string HasInterpritationIndexName {
            get {
                return ((string)(this["HasInterpritationIndexName"]));
            }
            set {
                this["HasInterpritationIndexName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("pathologicalprocess")]
        public string PathologicalProcessIndexName {
            get {
                return ((string)(this["PathologicalProcessIndexName"]));
            }
            set {
                this["PathologicalProcessIndexName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("occurence")]
        public string OccurenceIndexName {
            get {
                return ((string)(this["OccurenceIndexName"]));
            }
            set {
                this["OccurenceIndexName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("findingmethod")]
        public string FindingMethodIndexName {
            get {
                return ((string)(this["FindingMethodIndexName"]));
            }
            set {
                this["FindingMethodIndexName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("findinginformer")]
        public string FindingInformerIndexName {
            get {
                return ((string)(this["FindingInformerIndexName"]));
            }
            set {
                this["FindingInformerIndexName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("hasdefinitionalmanifestation")]
        public string HasDefinitionalManifestationIndexName {
            get {
                return ((string)(this["HasDefinitionalManifestationIndexName"]));
            }
            set {
                this["HasDefinitionalManifestationIndexName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool BuildDescriptions {
            get {
                return ((bool)(this["BuildDescriptions"]));
            }
            set {
                this["BuildDescriptions"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool BuildRelationships {
            get {
                return ((bool)(this["BuildRelationships"]));
            }
            set {
                this["BuildRelationships"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool BuildClinicalFindings {
            get {
                return ((bool)(this["BuildClinicalFindings"]));
            }
            set {
                this["BuildClinicalFindings"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool BuildFindingSites {
            get {
                return ((bool)(this["BuildFindingSites"]));
            }
            set {
                this["BuildFindingSites"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool BuildAssociatedMorphologies {
            get {
                return ((bool)(this["BuildAssociatedMorphologies"]));
            }
            set {
                this["BuildAssociatedMorphologies"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool BuildAssociatedWith {
            get {
                return ((bool)(this["BuildAssociatedWith"]));
            }
            set {
                this["BuildAssociatedWith"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool BuildCausativeAgents {
            get {
                return ((bool)(this["BuildCausativeAgents"]));
            }
            set {
                this["BuildCausativeAgents"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool BuildDueTos {
            get {
                return ((bool)(this["BuildDueTos"]));
            }
            set {
                this["BuildDueTos"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool BuildAfters {
            get {
                return ((bool)(this["BuildAfters"]));
            }
            set {
                this["BuildAfters"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool BuildSeverities {
            get {
                return ((bool)(this["BuildSeverities"]));
            }
            set {
                this["BuildSeverities"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool BuildClinicalCourses {
            get {
                return ((bool)(this["BuildClinicalCourses"]));
            }
            set {
                this["BuildClinicalCourses"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool BuildEpisodicities {
            get {
                return ((bool)(this["BuildEpisodicities"]));
            }
            set {
                this["BuildEpisodicities"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool BuildInterprets {
            get {
                return ((bool)(this["BuildInterprets"]));
            }
            set {
                this["BuildInterprets"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool BuildHasInterpretations {
            get {
                return ((bool)(this["BuildHasInterpretations"]));
            }
            set {
                this["BuildHasInterpretations"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool BuildPathologicalProcesses {
            get {
                return ((bool)(this["BuildPathologicalProcesses"]));
            }
            set {
                this["BuildPathologicalProcesses"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool BuildOccurences {
            get {
                return ((bool)(this["BuildOccurences"]));
            }
            set {
                this["BuildOccurences"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool BuildFindingMethods {
            get {
                return ((bool)(this["BuildFindingMethods"]));
            }
            set {
                this["BuildFindingMethods"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool BuildFindingInformers {
            get {
                return ((bool)(this["BuildFindingInformers"]));
            }
            set {
                this["BuildFindingInformers"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool BuildHasDefinitionalManifestations {
            get {
                return ((bool)(this["BuildHasDefinitionalManifestations"]));
            }
            set {
                this["BuildHasDefinitionalManifestations"] = value;
            }
        }
    }
}
