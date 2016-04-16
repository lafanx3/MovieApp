using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudApp.Domain
{
    public class Movie
    {
        //Movie id
        public int Id { get; set; }
        //Name of Movie
        public string Title { get; set; }
        //Rating? make nullable
        public int Rating { get; set; }
        //Movie Image
        public string Poster_path { get; set; }
    }
}