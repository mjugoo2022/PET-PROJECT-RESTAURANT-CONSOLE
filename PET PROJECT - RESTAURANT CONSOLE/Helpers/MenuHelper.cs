using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PET_PROJECT___RESTAURANT_CONSOLE.Helpsrs
{
    public class MenuHelper
    {

        internal double TotalPricePerMenu(List<Menu_list> menuList, int mealNo, int quantity)
        {
            //take menu id and loop in menu list to get menu details
            //int price = 0;
            var price = 0.0;
            foreach (var mList in menuList)
            {
                if (mList.mealNo == mealNo)
                {
                    price = mList.price * quantity;
                    break;
                }
            }
            return price;
        }
    }
}
