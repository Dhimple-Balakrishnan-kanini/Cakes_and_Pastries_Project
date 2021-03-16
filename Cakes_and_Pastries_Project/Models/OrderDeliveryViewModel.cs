﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cakes_and_Pastries_Project.Models
{
    public class OrderDeliveryViewModel
    {
        public string Delivery_Employee_ID { get; set; }
        public Nullable<int> Order_ID { get; set; }
        public string Customer_Name { get; set; }
        public string Mobile_Number { get; set; }
        public string Delivery_Address { get; set; }
        public Nullable<System.DateTime> Date_Of_Delivery { get; set; }
        public Nullable<System.TimeSpan> Time_Of_Delivery { get; set; }
        public string Email_ID { get; set; }

    }
}