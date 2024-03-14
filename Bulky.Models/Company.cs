﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }    


        public string StreetAddress { get; set; }

        public String City {  get; set; }

        public String State { get; set; }

        public String PostalCode { get; set; } 

        public String PhoneNumber { get; set; }

    }
}
