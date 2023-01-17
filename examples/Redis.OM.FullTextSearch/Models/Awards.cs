﻿

using Redis.OM.Modeling;

namespace Redis.OM.FullTextSearch.Models
{
    [Document(IndexName = "awards-idx", StorageType = StorageType.Json)]
    public class Awards
    {
        [Indexed(Sortable = true)]
        public int Wins { get; set; }
        [Indexed(Sortable = true)]
        public int Nominations { get; set; }
        [Searchable]
        public string? Text { get; set; }
    }
}
