using System;
using System.Collections.Generic;
using System.Text;

namespace IndexBuilder.Dtos
{
    public class Shards
    {
        public int total { get; set; }
        public int successful { get; set; }
        public int failed { get; set; }
    }

    public class Source
    {
        public string id { get; set; }
        public string effectiveTime { get; set; }
        public string active { get; set; }
        public string moduleId { get; set; }
        public string sourceId { get; set; }
        public string destinationId { get; set; }
        public string relationshipGroup { get; set; }
        public string typeId { get; set; }
        public string characteristicTypeId { get; set; }
        public string modifierId { get; set; }
        public string term { get; set; }
        public string conceptId { get; set; }
    }

    public class Hit
    {
        public string _index { get; set; }
        public string _type { get; set; }
        public string _id { get; set; }
        public double _score { get; set; }
        public Source _source { get; set; }
    }

    public class Hits
    {
        public int total { get; set; }
        public double? max_score { get; set; }
        public List<Hit> hits { get; set; }
    }

    public class Result
    {
        public int took { get; set; }
        public bool timed_out { get; set; }
        public Shards _shards { get; set; }
        public Hits hits { get; set; }
    }

    public class Relationship
    {
        public string ParentID { get; set; }
        public string ChildID { get; set; }
        public int Level { get; set; }
    }
}
