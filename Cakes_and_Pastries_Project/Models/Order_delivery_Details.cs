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

    public partial class Order_delivery_Details
    {
        [DisplayName("Delivery Employee ID")]
        public string Delivery_Employee_ID { get; set; }
        [DisplayName("Order ID")]
        public Nullable<int> Order_ID { get; set; }
        [DisplayName("Customer Name")]
        public string Customer_Name { get; set; }
        [DisplayName("Mobile Name")]
        public string Mobile_Number { get; set; }
        [DisplayName("Delivery  Addres")]
        public string Delivery_Address { get; set; }
        [DisplayName("Date of Delivery")]
        public Nullable<System.DateTime> Date_Of_Delivery { get; set; }
        [DisplayName("Time of Delivery")]
        public Nullable<System.TimeSpan> Time_Of_Delivery { get; set; }
        public string Email_ID { get; set; }
    
        public virtual Delivery_Employee_Details Delivery_Employee_Details { get; set; }
        public virtual Ordered_Cake_Details Ordered_Cake_Details { get; set; }
        public virtual Delivery_Details Delivery_Details { get; set; }
    }
}
