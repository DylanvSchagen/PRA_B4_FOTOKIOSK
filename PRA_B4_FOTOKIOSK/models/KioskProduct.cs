﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRA_B4_FOTOKIOSK.models
{
    public class KioskProduct
    {

        public string Name { get; set; }
        public float Price { get; set; }

        public string Description { get; set; }

    }
    public class OrderProduct
    { 
        public int? FotoId { get; set; }
        public string ProductName { get; set; }
        public int? Amount {  get; set; }
        
        public float? PriceTotal {  get; set; }

    }
        
}
