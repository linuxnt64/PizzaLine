using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2

{
    internal class Order
    {
        private static List<Order> orders = new List<Order>();

        public string Consignee { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Food { get; set; }
        public bool Queued { get; set; }

        public static string fileLocation = @"C:\temp\PizzaLine.txt";

        public static void QueueNewOrder()
        {
            Order order = new Order();
            string _city = "";

            Console.WriteLine("\n\nMata in Ny order:\n\n");
            Console.WriteLine("Namn");
            order.Consignee = Console.ReadLine();
            Console.WriteLine("Leveransadress");
            order.Street = Console.ReadLine();
            Console.WriteLine("Stad");
            _city = Console.ReadLine();
            if (_city == "")
            {
                order.City = "Skoga";
            }
            else
            {
                order.City = _city;
            };
            Console.WriteLine("Telefon");
            order.Phone = cleanNumStr(Console.ReadLine());  // Lämpligen mobil# - Fungerar som 'unik' nyckel 
            Console.WriteLine("Beställning");
            order.Food = Console.ReadLine();
            order.Queued = true;

            orders.Add(order);
            Console.ReadKey();
            Console.Clear();
        }

        private static string cleanNumStr(string _dirty)
        {
            string res = new string(_dirty.Where(char.IsDigit).ToArray());
            if (res != "") return res;
            return "0";
        }

        public static void ListOrders()
        {
            Console.WriteLine();
            foreach (Order order in orders)
            {
                Console.WriteLine();
                Console.WriteLine($"{order.Consignee}\t{order.Phone}\t{order.Street} {order.City}\tI kö?: {order.Queued}\nBeställt: {order.Food}");
            }
            Console.ReadKey();
            Console.Clear();
        }

        public static void StartOrder()
        {
            //Här ska bagarn markera (genom order.Queued -> false) att beställningen nu är pågående och alltså inte kan ångras
            foreach (Order order in orders)
            {
                if (order.Queued == true)
                {
                    Console.WriteLine("#####################################################################\n\n" +
                                      $"Nästa beställning att behandlas:\n{order.Consignee}\t{order.Phone}\t{order.Street} {order.City}\n\nBeställt: {order.Food}\n\n" +
                                      "#####################################################################");
                    order.Queued = false;
                    break;
                }
            }
            Console.ReadKey();
            Console.Clear();
        }

        public static void RemoveOrder()
        {
            Console.WriteLine("Telefonnummer beställningen är registrerad på? ");
            string killPhone = cleanNumStr(Console.ReadLine());
            foreach (Order order in orders)
            {
                if (order.Phone == killPhone && order.Queued == true)
                {
                    Console.WriteLine($"{order.Consignee}\t{order.Phone}\t{order.Street} {order.City}\n **** Annullerad beställning: {order.Food}");
                    orders = orders.Where(order => order.Phone != killPhone).ToList();
                    break;
                }
                else if (order.Phone == killPhone)
                {
                    Console.WriteLine("#####################################################################\n\n" +
                                        "Tyvärr!!  Denna beställning är redan på väg och kan därför inte avbeställas\n\n" +
                                      "#####################################################################");
                    break;
                }
                else
                {
                    Console.WriteLine("\n\nKunde inte hitta den beställningen?!\n\n");
                }
            }
            // Console.Clear behövs inte här, då ListOrders() körs direkt efter i menyvalet
        }

        public static void makeOfferCode()
        {
            Console.WriteLine("En rabattkod: " + Guid.NewGuid());
            // Såklart ska den kopplas till kund och menyval för 'redeem' läggas till
            Console.ReadKey();
            Console.Clear();
        }

        public static void saveListToFile()
        {
            StreamWriter sw = new StreamWriter(fileLocation);


            Console.WriteLine("Sparar nu ner data till fil ...\n\n");
            foreach(Order order in orders)
            {
                sw.WriteLine(order.Consignee + "\t" + order.Phone + "\t" + order.Street + "\t" + order.City + "\t"  + order.Food);
            }
            //sw.WriteLine("From the StreamWriter class");
            sw.Close();

            Console.WriteLine("Klart: Spara data till fil ...");
            Console.ReadKey();
            Console.Clear();

        }

        public static void Menu()
        {
            char menuSwitch;

            do
            {
                Console.WriteLine("\n\n**************** Välkommen till registret för OnlinePizzabeställning **************** \n\n");
                Console.WriteLine("Le Menü:\n");
                Console.WriteLine("1. Ny beställning");
                Console.WriteLine("2. Starta");
                Console.WriteLine("3. Lista");
                Console.WriteLine("4. Avbeställning ");
                Console.WriteLine();
                Console.WriteLine("6. Fixa rabattkod");
                Console.WriteLine();
                Console.WriteLine("8. Spara listan");
                Console.WriteLine();
                Console.WriteLine("9. Avsluta programmet");

                menuSwitch = Console.ReadKey().KeyChar;
                switch (menuSwitch)
                {
                    case '1':
                        QueueNewOrder();
                        break;
                    case '2':
                        StartOrder();
                        break;
                    case '3':
                        ListOrders();
                        break;
                    case '4':
                        RemoveOrder();
                        ListOrders();
                        break;
                    case '6':
                        makeOfferCode();
                        break;
                    case '8':
                        saveListToFile();
                        break;
                    /*                    case 7:
                                            ListOrders();
                                            Console.WriteLine("\n\nVilken order (telefonnummer) vill du flytta fram i kön?");
                                            // metod för att flytta i kön
                                            Console.ReadKey();
                                            break;
                    */
                    default:
                        break;
                }
                Console.Clear();
            } while (menuSwitch != '9');
        }
    }
}


