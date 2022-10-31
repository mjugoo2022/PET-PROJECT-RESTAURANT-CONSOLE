
using PET_PROJECT___RESTAURANT_CONSOLE.Helpsrs;
using PET_PROJECT___RESTAURANT_CONSOLE.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace PET_PROJECT___RESTAURANT_CONSOLE
{
    class Program
    {



        static void Main(string[] args)
        {


            Connection connect = new Connection();
            //Console.WriteLine("Press Z to view order report");
            //ConsoleKey reportkey;
            //reportkey = Console.ReadKey(true).Key;

            //if (reportkey == ConsoleKey.Z)
            //{
            //    Console.WriteLine("Order Report");
            //    Console.WriteLine();
            //    connect.DisplayReport();
            //    Console.WriteLine();
             

            //}

            Console.WriteLine();

            //instantiate object
            MenuHelper menuhelper = new MenuHelper();

            Console.WriteLine("WELCOME TO MO-RESTAURANT!");
            Console.WriteLine();
            Console.WriteLine("--------------------MENU-------------------");
            Console.WriteLine();

            string menuJsonData = File.ReadAllText("C:\\Users\\mjugoo\\source\\repos\\PET PROJECT - RESTAURANT CONSOLE\\PET PROJECT - RESTAURANT CONSOLE\\menu.json");
            //deserialize json into list
            var menuList = JsonSerializer.Deserialize<List<Menu_list>>(menuJsonData);



            //check if menu list is null 
            if (menuList != null)
            {

                foreach (var menu in menuList)
                {

                    //String interpolation - Inject object into string
                    Console.WriteLine($"{menu.mealNo}\t{menu.mealName}\t \t{menu.price}");

                    //Result: 1 Chicken Pad Thai 1 325
                }



            }


            Console.WriteLine();

            Console.WriteLine(" Admin: press Z to view order report2, User: Press enter to continue");
            ConsoleKey reportkey;
            reportkey = Console.ReadKey(true).Key;

            if (reportkey == ConsoleKey.Z)
            {
                Console.WriteLine("Order Report");
                Console.WriteLine();
                connect.DisplayReport();
                Console.WriteLine();


            }

            Console.WriteLine();
            string fname;
            Console.WriteLine("Please enter your first name");
            fname = Console.ReadLine();
            string lname;
            Console.WriteLine("Please enter your last name");
            lname = Console.ReadLine();

            //-----------------------------------------------------------------------




            Console.WriteLine("Please select the meal number to order:");

            // Create a string variable and get user input from the keyboard and store it in the variable
            string meal = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Details:");
            List<SelectedItem> ItemList = new List<SelectedItem>();


            while (true)
            {
                //convert menu id to integer
                int menuIdInt;

                if (Int32.TryParse(meal, out menuIdInt))
                {
                    menuIdInt = Int32.Parse(meal);
                }

                //FirstOrDefault() - default value if there is no element 
                var selectedMeal = menuList.Where(x => x.mealNo.Equals(menuIdInt)).FirstOrDefault();

                if (selectedMeal != null)
                {
                    var selectedItem = new SelectedItem();
                    selectedItem.MealName = selectedMeal.mealName;
                    selectedItem.MenuId = selectedMeal.mealNo;

                    displayMenu(selectedMeal.mealName);

                    Console.WriteLine("Please enter quantity or press -1 to cancel");
                    int quantityInt = 0;
                    string quantity = Console.ReadLine();


                    //convert to int
                    if (Int32.TryParse(quantity, out quantityInt))
                    {
                        quantityInt = Int32.Parse(quantity);
                    }

                    //check if value is -1, do not add to list

                    if (quantityInt != -1)
                    {

                        selectedItem.Quantity = quantityInt;
                        ItemList.Add(selectedItem);

                        //fetch price
                    }
                    //Console.Read();
                    Console.WriteLine("Press X to exit or order another menu item");

                    ConsoleKey key;
                    key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.X)
                    {
                        Console.Clear();
                        Console.WriteLine("THANK YOU FOR YOUR VISIT!");
                        Console.WriteLine();
                        Console.Beep();
                        break;
                    }
                    else if (key == ConsoleKey.C)
                    {
                        Console.WriteLine("Please select the meal number to order:");
                    }
                    meal = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Meal not found ");
                }
            }

            Console.WriteLine("Here is your purchase!");
            Console.WriteLine();
            Console.WriteLine("Customer name:" + " " + string.Concat(fname, " ", lname));

            var total = 0.0;
            ConsoleDataFormatter.PrintLine();

            foreach (var userinput in ItemList)
            {
                var x = menuhelper.TotalPricePerMenu(menuList, userinput.MenuId, userinput.Quantity);
                total = total + x;

                //call save method with (firstname, lastname, menu list selected, total price)
             Console.WriteLine(userinput.MenuId + " " + userinput.MealName + " " + userinput.Quantity + " " + x);

            }

            //serialize item list 
            var menulistObj = Newtonsoft.Json.JsonConvert.SerializeObject(ItemList);


            connect.InsertOrderDetails(((int) total), fname, lname, menulistObj);

            Console.WriteLine(menulistObj);
            Console.WriteLine("Total amount to be paid:" + total);

            Console.WriteLine();
            Console.WriteLine("purchase completed!");

            Console.WriteLine("Added successfully!");
        }

        private static void displayMenu(string menuName)
        {
            Console.WriteLine("Your meal " + menuName);
        }

        //Display info in Console Table



    }
}


