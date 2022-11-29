﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniProject_InsuranceManagementSystem.Models
{
    public class CustomerRequest
    {
         public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime PurchasedDate { get; set; }  

        public string MobileNumber { get; set; }

        public int CustomerId { get; set; }

        public string InsuranceType { get; set; }

        public string SubType { get; set; }
    }
}