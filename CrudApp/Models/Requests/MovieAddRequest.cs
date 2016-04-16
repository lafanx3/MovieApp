using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudApp.Models.Requests
{
    public class MovieAddRequest
    {
        //Movie id
        public int Id { get; set; }
        //Name of Movie
        public string Title { get; set; }
        //Rating? make nullable
        public int? Rating { get; set; }
        //Image url
        public string Poster_path { get; set; }
    }
}