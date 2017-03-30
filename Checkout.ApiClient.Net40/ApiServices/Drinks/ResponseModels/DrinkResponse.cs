using Checkout.ApiServices.Drinks.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.ApiServices.Drinks.ResponseModels
{
    public class DrinkResponse
    {
        public string Result { get; set; }

        public string Exception { get; set; }

        public Drink Drink { get; set; }

        public List<Drink> DrinkList { get; set; }
    }
}
