﻿using VianaSoft.BuildingBlocks.Core.Pagination;

namespace VianaSoft.Partner.App.Filters
{
    public class FilterIViewModel : Pager
    {
        public string? Name { get; set; }
        public string? Document { get; set; }
        public bool? Enable { get; set; }
    }
}
