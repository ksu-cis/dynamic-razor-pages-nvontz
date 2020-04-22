using System;
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

        [BindProperty]
        public string SearchTerms { get; set; }
        /// <summary>
        /// The Filtered MPAA Ratings
        /// </summary>
        [BindProperty]
        public string[] MPAARatings { get; set; }
        [BindProperty]
        public string[] Genres { get; set; }
        
        public double? IMDBMin { get; set; }

        public double? IMDBMax { get; set; }

        public double? RTMin { get; set; }

        public double? RTMax { get; set; }



        public void OnGet(double? IMDBMin, double? IMDBMax, double? RTMin, double? RTMax)
        {
            this.IMDBMin = IMDBMin;
            this.IMDBMax = IMDBMax;
            Movies = MovieDatabase.Search(SearchTerms);
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);
            Movies = MovieDatabase.FilterByGenre(Movies, Genres);
            Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
            Movies = MovieDatabase.FilterByRTRating(Movies, RTMin, RTMax);
        }

    }
}
