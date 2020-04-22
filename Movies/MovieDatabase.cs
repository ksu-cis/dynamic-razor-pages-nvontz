using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public static class MovieDatabase
    {
        private static List<Movie> movies = new List<Movie>();

        /// <summary>
        /// Loads the movie database from the JSON file
        /// </summary>
        static MovieDatabase() {

            using (StreamReader file = System.IO.File.OpenText("movies.json"))
            {
                string json = file.ReadToEnd();
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            }

            HashSet<string> genreSet = new HashSet<string>();
            foreach (Movie movie in movies)
            {
                if (movie.MajorGenre != null)
                {
                    genreSet.Add(movie.MajorGenre);
                }
            }
            genres = genreSet.ToArray();
        }

        /// <summary>
        /// Gets all the movies in the database
        /// </summary>
        public static IEnumerable<Movie> All { get { return movies; } }

        /// <summary>
        /// Searches the database for matching movies
        /// </summary>
        /// <param name="terms">The terms to search for</param>
        /// <returns>A collection of movies</returns>
        public static IEnumerable<Movie> Search(string terms)
        {
            List<Movie> results = new List<Movie>();

            if (terms == null) return All;

            foreach(Movie movie in All)
            {
                if(movie.Title != null && movie.Title.Contains(terms, StringComparison.InvariantCultureIgnoreCase))
                {
                    results.Add(movie);
                }
            }

            return results;
        }

        public static string[] MPAARatings
        {
            get => new string[]
            {
                "G",
                "PG",
                "PG-13",
                "R",
                "NC-17"
            };
        }
        /// <summary>
        /// Filters the provided collection of movies to filter
        /// </summary>
        /// <param name="movies"></param>
        /// <param name="ratings"></param>
        /// <returns></returns>
        public static IEnumerable<Movie> FilterByMPAARating(IEnumerable<Movie> movies, IEnumerable<string> ratings)
        {
            if (ratings == null || ratings.Count() == 0) return movies;

            List<Movie> results = new List<Movie>();
            foreach(Movie movie in movies)
            {
                if (movie.MPAARating != null && ratings.Contains(movie.MPAARating))
                {
                    results.Add(movie);
                }
            }
            return results;
        }
        /// <summary>
        /// The genres represented in the database
        /// </summary>
        private static string[] genres;
        /// <summary>
        /// Gets the movie genres represented in the database
        /// </summary>
        public static string[] Genres => genres;

        /// <summary>
        /// Filters the collection by Genre
        /// </summary>
        /// <param name="movies"></param>
        /// <param name="ratings"></param>
        /// <returns></returns>
        public static IEnumerable<Movie> FilterByGenre(IEnumerable<Movie> movies, IEnumerable<string> genre)
        {
            if (genre == null || genre.Count() == 0) return movies;

            List<Movie> results = new List<Movie>();
            foreach (Movie movie in movies)
            {
                if (movie.MajorGenre != null && genre.Contains(movie.MajorGenre))
                {
                    results.Add(movie);
                }
            }
            return results;
        }

        public static IEnumerable<Movie> FilterByIMDBRating(IEnumerable<Movie> movies, double? min, double? max)
        {
            if (min == null && max == null) return movies;
           
            var results = new List<Movie>();

            if(min == null)
            {
                foreach(Movie movie in movies)
                {
                    if (movie.IMDBRating <= max) results.Add(movie);
                }
                return results;
            }

            if (max == null)
            {
                foreach (Movie movie in movies)
                {
                    if (movie.IMDBRating >= min) results.Add(movie);
                }
                return results;
            }
            foreach(Movie movie in movies)
            {
                if(movie.IMDBRating >= min && movie.IMDBRating <= max)
                {
                    results.Add(movie);
                }
            }

            return results;
        }

        public static IEnumerable<Movie> FilterByRTRating(IEnumerable<Movie> movies, double? min, double? max)
        {
            if (min == null && max == null) return movies;

            var results = new List<Movie>();

            if (min == null)
            {
                foreach (Movie movie in movies)
                {
                    if (movie.RottenTomatoesRating <= max) results.Add(movie);
                }
                return results;
            }

            if (max == null)
            {
                foreach (Movie movie in movies)
                {
                    if (movie.RottenTomatoesRating >= min) results.Add(movie);
                }
                return results;
            }
            foreach (Movie movie in movies)
            {
                if (movie.RottenTomatoesRating >= min && movie.RottenTomatoesRating <= max)
                {
                    results.Add(movie);
                }
            }

            return results;
        }
    }
}
