using MyShelf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShelf.ViewModels
{
    public class AuthorBooksViewModel
    {
        public Author Author { get; set; }
        public List<Book> Books { get; set; }
    }
}