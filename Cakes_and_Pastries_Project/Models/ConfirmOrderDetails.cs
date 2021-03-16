using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cakes_and_Pastries_Project.Models
{
    public class ConfirmOrderDetails
    {
        public int Order_ID { get; set; }
        public string Cake_Id { get; set; }
        public string Cake_Name { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> Price { get; set; }
        public string Status_of_Order { get; set; }
        public string Email_ID { get; set; }
    }
}