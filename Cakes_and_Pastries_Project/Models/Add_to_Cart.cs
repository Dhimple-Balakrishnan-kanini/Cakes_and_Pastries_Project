//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cakes_and_Pastries_Project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class Add_to_Cart
    {
        [DisplayName("Email-ID")]
        public string Email_ID { get; set; }
        [DisplayName("Cake-ID")]
        public string Cake_ID { get; set; }
        [DisplayName("Cake and Pastries Type")]
        public string Cake_and_Pastries_Type { get; set; }
        [DisplayName("Cake Name")]
        public string Cake_Name { get; set; }
        [DisplayName("Quantity")]
        public Nullable<int> Quantity { get; set; }
        [DisplayName("Price")]
        public Nullable<int> Price { get; set; }
    
        public virtual Cakes_and_Pastries_Details Cakes_and_Pastries_Details { get; set; }
        public virtual Customer_Details Customer_Details { get; set; }
    }
}
