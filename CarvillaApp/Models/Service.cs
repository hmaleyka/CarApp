﻿using CarvillaApp.Models.Base;

namespace CarvillaApp.Models
{
    public class Service : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

    }
}