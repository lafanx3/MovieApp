using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudApp.Models.Requests
{
    public class MovieUpdateRequest
    {
        public int Id { get; set; }
        public int Rating { get; set; }
    }
}