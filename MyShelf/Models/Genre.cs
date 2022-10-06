using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShelf.Models
{
    public class Genre
    {
        public int GenreId { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public virtual List<Book> Books { get; set; }
    }
}