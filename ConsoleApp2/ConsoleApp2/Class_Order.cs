using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


namespace ConsoleApp2

{
    internal class Order
    {
        public static string fileLocation = @"C:\temp\PizzaLine.txt";   //  <<<=============   Här ligger fil/sökvägen
        private static List<Order> orders = new List<Order>();

        public string Consignee { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Food { get; set; }
        public bool Queued { get; set; }


        public static void QueueNewOrder()
        {
            Order order = new Order();
            string city;

            Console.WriteLine("\n\nMata in Ny order:\n\n");
            Console.WriteLine("Namn");
            order.Consignee = Console.ReadLine();
            Console.WriteLine("Telefon");
            order.Phone = cleanNumStr(Console.ReadLine());  // Lämpligen mobil# - Fungerar som 'unik' nyckel 
            Console.WriteLine("Leveransadress");
            order.Street = Console.ReadLine();
            Console.WriteLine("Stad");
            city = Console.ReadLine();
            if (city == "")
            {
                order.City = "Skoga";
            }
            else
            {
                order.City = city;
            };
            Console.WriteLine("Beställning");
            order.Food = Console.ReadLine();
            order.Queued = true;

            orders.Add(order);
            Console.ReadKey();
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
        }

        public static void StartOrder()
        {
            //Med detta kan bagarn läsa nästa bestaällning i kön och markera (genom order.Queued -> false) att den nu är pågående och alltså inte kan ångras
            foreach (Order order in orders)
            {
                if (order.Queued == true)
                {
                    Console.WriteLine(
                        "\t\t---------------------------------------------------------------------------------------------------\n\n" +
                       $"\t\t\tNästa beställning att behandlas:\n\t\t\t{order.Consignee}\t{order.Phone}\t{order.Street} {order.City}\n\n\t\t\tBeställt: {order.Food}\n\n" +
                        "\t\t---------------------------------------------------------------------------------------------------\n\n");
                    order.Queued = false;
                    break;
                }
            }
            Console.ReadKey();
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
                    /* Jämför med:
                    var index = participantList. IndexOf(participant) ;
                    participantList . RemoveAt(index) ;
                    */
                    break;
                }
                else if (order.Phone == killPhone)
                {
                    Console.WriteLine("\t\t###############################################################################\n\n" +
                                      "\t\t# Tyvärr!!  Denna beställning är redan på väg och kan därför inte avbeställas #\n\n" +
                                      "\t\t###############################################################################");
                    break;
                }
                else
                {
                    Console.WriteLine("\n\nKunde inte hitta den beställningen?!\n\n");
                }
                System.Threading.Tasks.Task.Delay(2000).Wait();
            }
        }

        public static void makeOfferCode()
        {
            Console.WriteLine("En rabattkod: " + Guid.NewGuid());
            // Såklart ska den kopplas till kund och menyval för 'redeem' läggas till
            Console.ReadKey();
        }

        public static void saveListToFile()
        {
            using (StreamWriter sw = new StreamWriter(fileLocation))
            {

                Console.WriteLine("Sparar nu ner köade kunder till fil ...\n\n");
                foreach (Order order in orders)
                {
                    if (order.Queued) sw.WriteLine(order.Consignee + "\t" + order.Phone + "\t" + order.Street + "\t" + order.City + "\t" + order.Food);
                }
                Console.WriteLine("Klart: (Spara data till fil)");
                orders.Clear();
                Console.ReadKey();
            }
        }

        public static void readListFromFile()
        {
            try
            {
                using (StreamReader sr = new StreamReader(fileLocation))
                {
                    string line = "";

                    while ((line = sr.ReadLine()) != null)
                    {
                        Order order = new Order();
                        string[] values = line.Split("\t");
                        order.Consignee = values[0];
                        order.Phone = values[1];
                        order.Street = values[2];
                        order.City = values[3];
                        order.Food = values[4];
                        order.Queued = true;
                        orders.Add(order);
                    }

                }
                Console.WriteLine("Klart: (Har läst data från fil)");
            }
            catch
            {
                Console.WriteLine("\t¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤\n" +
                                  "\t¤¤¤¤¤¤  FEL:  Det gick inte att läsa in från fil. Filen kanske saknas?  ¤¤¤¤¤¤\n" +
                                  "\t¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤");
                
            }
        Console.ReadKey();
        }
        public static void GracefulEndPrg()
        {
            if (orders.Count > 0)
            {
                Console.WriteLine("Du har obearbetade beställningar i kö - spara dem först!");
                Console.ReadKey();
                Menu();
            }
        }

        public static void Menu()
        {
            char menuSwitch;
            do
            {
                Console.Clear();
                Console.WriteLine("\tLe Menü:\n");
                Console.WriteLine("\t1. Ny beställning");
                Console.WriteLine("\t2. Starta");
                Console.WriteLine("\t3. Lista");
                Console.WriteLine("\t4. Avbeställning ");
                Console.WriteLine();
                Console.WriteLine("\t6. Fixa rabattkod");
                Console.WriteLine();
                Console.WriteLine("\t7. Spara kön till fil");
                Console.WriteLine("\t8. Läs in kön från fil");
                Console.WriteLine();
                Console.WriteLine("\t9. Avsluta programmet");

                menuSwitch = Console.ReadKey().KeyChar;
                Console.Clear();
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
                        //ListOrders();
                        break;
                    case '6':
                        makeOfferCode();
                        break;
                    case '7':
                        saveListToFile();
                        break;
                    case '8':
                        readListFromFile();
                        break;
                    default:
                        break;
                }
            } while (menuSwitch != '9');
            GracefulEndPrg();
        }
    }
}


