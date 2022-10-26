using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PET_PROJECT___RESTAURANT_CONSOLE.Models
{
    public class SelectedItem
    {
        public int MenuId { get; set; }
        public string MealName { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

    }
}
