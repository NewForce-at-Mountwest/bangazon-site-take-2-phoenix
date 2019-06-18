using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.OrderViewModels
{
    public class OrderPaymentViewModel
    {
        public Order FinalOrder { get; set; }


        public SelectList ThePaymentTypes{ get; set;  }








    }
}
