﻿using MyStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyStore.WebUI.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { set; get; }
        public string returnUrl { set; get; } 
    }
}