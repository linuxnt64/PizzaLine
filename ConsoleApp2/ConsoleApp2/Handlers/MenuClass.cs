using System;
using System.Collections.Generic;

namespace ConsoleApp2

{
    internal class MenuClass : OrderModelClass
    {
        public static List<OrderModelClass> listOfOrders = new List<OrderModelClass>();
        public static void Menu()
        {
            char menuSwitch;
            do
            {
                printMenu();
                menuSwitch = Console.ReadKey().KeyChar;
                Console.Clear();
                switch (menuSwitch)
                {
                    case '1':
                        ListHandlerClass.QueueNewOrder();
                        break;
                    case '2':
                        ListHandlerClass.StartOrder();
                        break;
                    case '3':
                        ListHandlerClass.ListOrders();
                        break;
                    case '4':
                        ListHandlerClass.RemoveOrder();
                        break;
                    case '6':
                        MiscClass.makeOfferCode();
                        break;
                    case '7':
                        FileHandlerClass.saveListToFile();
                        break;
                    case '8':
                        FileHandlerClass.readListFromFile();
                        break;
                    default:
                        break;
                }
            } while (menuSwitch != '9');
            ;
        }

        private static void printMenu()
        {
            Console.Clear();
            Console.WriteLine("\tLe Menü:\n");
            Console.WriteLine("\t1. Ny beställning");
            Console.WriteLine("\t2. Starta");
            Console.WriteLine("\t3. Lista");
            Console.WriteLine("\t4. Avbeställning ");
            Console.WriteLine();
            Console.WriteLine("\t6. Rabattkod");
            Console.WriteLine();
            Console.WriteLine("\t7. Spara kön till fil");
            Console.WriteLine("\t8. Läs in kön från fil");
            Console.WriteLine();
            Console.WriteLine("\t9. Avsluta programmet");
        }
    }
}


