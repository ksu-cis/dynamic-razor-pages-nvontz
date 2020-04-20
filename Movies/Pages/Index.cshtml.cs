﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Movie> Movies { get; protected set; }
        public string SearchTerms { get; set; }

        public void OnGet()
        {
            String terms = Request.Query["SearchTerms"];
            Movies = MovieDatabase.Search(SearchTerms);
        }

    }
}
