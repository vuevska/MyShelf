using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShelf.ViewModels
{
    public class BookItemViewModel
    {
        [Key]
        public int ItemId { get; set; }

        //Foreign Key
        public int BookId { get; set; }

        public virtual Models.Book Book { get; set;}

        public int ShoppingCartId { get; set; }

        public virtual Models.ShoppingCart ShoppingCart { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

    }
}