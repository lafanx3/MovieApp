using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudApp.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public bool MoviePresent { get; set; }
    }
}