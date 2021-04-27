﻿using System;
using System.Collections.Generic;
using System.Text;
namespace AuthorQuerier
{
    /// <summary>
    /// Creates a model modelling the root object of the api resource
    /// </summary>
    public class PageModel
    {
        public string page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public AuthorModel[] data { get; set; }
    }
}
