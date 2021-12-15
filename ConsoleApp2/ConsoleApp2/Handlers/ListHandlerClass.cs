using System;
using System.Linq;

namespace ConsoleApp2
{
    internal class ListHandlerClass : MenuClass
    {
     
        /// <summary>
        /// Prompt'ar användaren att mata in uppgifter för att skapa en ny beställning
        /// </summary>
        public static void QueueNewOrder()
        {
            MenuClass orderEntry = new MenuClass();
            string city;

            Console.WriteLine("\n\nNy beställning:\n\n");
            Console.WriteLine("Namn");
            orderEntry.Consignee = Console.ReadLine();
            Console.WriteLine("Telefon");
            orderEntry.Phone = cleanNumStr(Console.ReadLine());  // Lämpligen mobil# - Fungerar som 'unik' nyckel 
            Console.WriteLine("Leveransadress");
            orderEntry.Street = Console.ReadLine();
            Console.WriteLine("Stad");
            city = Console.ReadLine();
            if (city == "")
            {
                orderEntry.City = "Skoga";
            }
            else
            {
                orderEntry.City = city;
            };
            Console.WriteLine("Beställning");
            orderEntry.Food = Console.ReadLine();
            orderEntry.Queued = true;

            listOfOrders.Add(orderEntry);
            Console.ReadKey();
        }

        /// <summary>
        /// Skriver ut listan på konsollen
        /// </summary>
        public static void ListOrders()
        {
            Console.WriteLine();
            foreach (MenuClass orderEntry in listOfOrders)
            {
                Console.WriteLine();
                Console.WriteLine($"{orderEntry.Consignee}\t{orderEntry.Phone}\t{orderEntry.Street} {orderEntry.City}\tI kö?: {orderEntry.Queued}\nBeställt: {orderEntry.Food}");
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Tar bort beställning om den fortfarande har status 'i kö' dvs inte påbörjats i beredning
        /// </summary>
        public static void RemoveOrder()
        {
            Console.WriteLine("Telefonnummer beställningen är registrerad på? ");
            string killPhone = cleanNumStr(Console.ReadLine());
            bool foundOrder = false;
            foreach (MenuClass orderEntry in listOfOrders)
            {
                if (orderEntry.Phone == killPhone && orderEntry.Queued == true)
                {
                    foundOrder = true;
                    Console.WriteLine($"{orderEntry.Consignee}\t{orderEntry.Phone}\t{orderEntry.Street} {orderEntry.City}\n **** Annullerad beställning: {orderEntry.Food}");
                    listOfOrders = listOfOrders.Where(orderEntry => orderEntry.Phone != killPhone).ToList();
                    /* Jämför med:
                    var index = participantList. IndexOf(participant) ;
                    participantList . RemoveAt(index) ;
                    */
                    break;
                }
                else if (orderEntry.Phone == killPhone)
                {
                    Console.WriteLine("\t\t###############################################################################\n\n" +
                                      "\t\t# Tyvärr!!  Denna beställning är redan på väg och kan därför inte avbeställas #\n\n" +
                                      "\t\t###############################################################################");
                    break;
                }
            }
            if (!foundOrder)
            {
                Console.WriteLine("\tKunde inte hitta den beställningen. Telefonnummer rätt?");
            }
            System.Threading.Tasks.Task.Delay(2000).Wait();
        }

        /// <summary>
        /// Skriver ut nästa order i kön för beredning och flaggar den som pågående (ej längre i kön)
        /// </summary>
        public static void StartOrder()
        {
            //Med detta kan bagarn läsa nästa beställning i kön och markera (genom orderEntry.Queued -> false) att den nu är pågående och alltså inte kan ångras
            foreach (MenuClass orderEntry in listOfOrders)
            {
                if (orderEntry.Queued == true)
                {
                    Console.WriteLine(
                        "\t\t---------------------------------------------------------------------------------------------------\n\n" +
                       $"\t\t\tNästa beställning att behandlas:\n\t\t\t{orderEntry.Consignee}\t{orderEntry.Phone}\t{orderEntry.Street} {orderEntry.City}\n\n\t\t\tBeställt: {orderEntry.Food}\n\n" +
                        "\t\t---------------------------------------------------------------------------------------------------\n\n");
                    orderEntry.Queued = false;
                    break;
                }
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Städar ur en sträng som borde innehålla bara ett heltal
        /// </summary>
        /// <param name="_dirty"></param>
        /// <returns></returns>
        private static string cleanNumStr(string _dirty)
        {
            string res = new string(_dirty.Where(char.IsDigit).ToArray());
            if (res != "") return res;
            return "0";
        }
    }
}