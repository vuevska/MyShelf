using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShelf.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; }

        public int AuthorId { get; set; }

        [Required]
        public virtual Author Author { get; set; }
        
        public int GenreId { get; set; }

        [Required]
        public virtual Genre Genre { get; set; }

        [Required, Display(Name = "Cover")]
        public string CoverUrl { get; set; }

        [Required, Display(Name = "Number of pages")]
        public int NumPages { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "Publication Date")]
        public string PublicationDate { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required, Display(Name = "ISBN")]
        public string ISBN13 { get; set; }

        [Required, Display(Name = "Average Rating")]
        public decimal AverageRating { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}