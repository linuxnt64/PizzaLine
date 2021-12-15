using System;

namespace ConsoleApp2
{
    internal class MiscClass : MenuClass
    {
 
        
        /// <summary>
        /// Tanken är att man inte ska missa att kön inte är tömd - ej impl. i denna ver
        /// </summary>
        //
        public static bool GracefulEndPrg()
        {
            if (listOfOrders.Count > 0 && true)
            {
                Console.WriteLine("Du har obearbetade beställningar i kö - spara dem först!");
                Console.ReadKey();
                return  true;
            } else return false;
        }


        /// <summary>
        /// Ett inte förfinat försök att uppfylla kravet på rabattkod. Förmodligen förbättrad i senare version
        /// </summary>
        //
        public static void makeOfferCode()
        {
            Console.WriteLine("En rabattkod: " + Guid.NewGuid());
            // Såklart ska den kopplas till kund och menyval för 'redeem' läggas till
            Console.ReadKey();
        }
    }
}