using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_c.Models
{
    //deze class geeft ieder product een unieke instantie
    public class CartItem
    {
        //de Game die toegevoegd wordt aan de ShoppingCart
        public Game Product { get; set; }

        //Constructor
        public CartItem(Game product)
        {
            Product = product;
        }
    }
}