using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cakes_and_Pastries_Project.Models
{
    public class ViewCartItems
    {
        
        public string Cake_ID { get; set; }
        public string Cake_and_Pastries_Type { get; set; }
        public string Cake_Name { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> Price { get; set; }
      
        
    }
}