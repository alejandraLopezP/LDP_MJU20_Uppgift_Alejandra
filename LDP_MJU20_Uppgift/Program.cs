using System;
using System.Linq;
using static System.Console;

namespace LDP_MJU20_Uppgift
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Please introduce your personal number YYYYMMDDnnnc: ");
            string personalNumber = ReadLine();

            int year, month, day;

            char[] arrayPersonNumber = personalNumber.ToCharArray();
            if (arrayPersonNumber.Length != 12)
            {
                WriteLine("Format invalid: You must introduce 12 numbers");
                ReadKey();
                return;
            }

            try
            {
                year = int.Parse(String.Join("", arrayPersonNumber.Skip(0).Take(4).ToArray()));
            }
            catch (Exception)
            {
                WriteLine("Invalid year");
                ReadKey();
                return;
            }

            try
            {
                month = int.Parse(String.Join("", arrayPersonNumber.Skip(4).Take(2).ToArray()));
            }
            catch (Exception)
            {

                WriteLine("Invalid month");
                ReadKey();
                return;
            }

            try
            {
                day = int.Parse(String.Join("", arrayPersonNumber.Skip(6).Take(2).ToArray()));
            }
            catch (Exception)
            {

                WriteLine("Invalid day");
                ReadKey();
                return;
            }

            if (!(year >= 1753 && year <= 2020))
            {
                WriteLine("Introduce a valid year");
                ReadKey();
                return;
            }

            if (!(month >= 1 && month <= 12))
            {
                WriteLine("Introduce a valid month");
                ReadKey();
                return;
            }
            int[] month30 = new int[] { 4, 6, 9, 11 };

            // Logica para saber si el año es biciesto
            bool yearBiciesto = true;

            if(!yearBiciesto && month == 2 && day > 28)
            {

            }

            if (month == 2 && day > 29)
            {

            }

            if (month30.Any(x => x == month) && day > 30)
            {

            }

            if (day > 31)
            {

            }

        }
    }
}
