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

    public partial class Ordered_Cake_Details
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ordered_Cake_Details()
        {
            this.Order_delivery_Details = new HashSet<Order_delivery_Details>();
        }

        [DisplayName("Order ID")]
        public int Order_ID { get; set; }
        [DisplayName("Cake ID")]
        public string Cake_Id { get; set; }
        [DisplayName("Cake Name")]
        public string Cake_Name { get; set; }
        [DisplayName("Quantity")]
        public Nullable<int> Quantity { get; set; }
        [DisplayName("Price")]
        public Nullable<int> Price { get; set; }
        [DisplayName("Status of Order")]
        public string Status_of_Order { get; set; }
        [DisplayName("Email ID")]
        public string Email_ID { get; set; }
    
        public virtual Cakes_and_Pastries_Details Cakes_and_Pastries_Details { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_delivery_Details> Order_delivery_Details { get; set; }
        public virtual Customer_Details Customer_Details { get; set; }
    }
}
