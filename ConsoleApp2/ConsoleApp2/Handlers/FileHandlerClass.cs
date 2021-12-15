using System;
using System.IO;

namespace ConsoleApp2
{
    /// <summary>
    /// Hanterar diskaktivitet som att läsa och spara listan till en textfil i mappen (systemvariabeln) APPDATA
    /// </summary>
    ///
    internal class FileHandlerClass : MenuClass
    {
        public static string fileLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PizzaLine.txt";  //  <<<=============   Här ligger fil/sökvägen
 
        /// <summary>
        /// Läser in (förutbestämd) lista från (förutbestämd) fil
        /// </summary>
        ///
        public static void readListFromFile()
        {
            try
            {
                using (StreamReader sr = new StreamReader(fileLocation))
                {
                    string line = "";

                    while ((line = sr.ReadLine()) != null)
                    {
                        MenuClass orderEntry = new MenuClass();
                        string[] values = line.Split("\t");
                        orderEntry.Consignee = values[0];
                        orderEntry.Phone = values[1];
                        orderEntry.Street = values[2];
                        orderEntry.City = values[3];
                        orderEntry.Food = values[4];
                        orderEntry.Queued = true;
                        listOfOrders.Add(orderEntry);
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

        /// <summary>
        /// Läser in (förutbestämd) fil till (förutbestämd) lista
        /// </summary>
        public static void saveListToFile()
        {
            using (StreamWriter sw = new StreamWriter(fileLocation))
            {

                Console.WriteLine("Sparar nu ner köade kunder till fil ...\n\n");
                foreach (MenuClass orderEntry in listOfOrders)
                {
                    if (orderEntry.Queued) sw.WriteLine(orderEntry.Consignee + "\t" + orderEntry.Phone + "\t" + orderEntry.Street + "\t" + orderEntry.City + "\t" + orderEntry.Food);
                }
                Console.WriteLine("Klart: (Spara data till fil)");
                listOfOrders.Clear();
                Console.ReadKey();
            }
        }
    }
}
