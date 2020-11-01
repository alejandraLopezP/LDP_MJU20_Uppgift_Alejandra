using System;
using System.Linq;
using static System.Console;

namespace LDP_MJU20_Uppgift
{
    class Program
    {
        static void Main(string[] args)
        {
            ValidationPersonalNumber();
        }
    
        private static void ValidationPersonalNumber()
        {
            WriteLine("Please introduce your personal number in the following formats:\n 1. YYYYMMDDnnnc: 2. YYYYMMDD+nnnc: 3.YYYYMMDD-nnnc: 4.YYMMDD-nnnc: 5.YYMMDD+nnnc: 6.YYMMDDnnnc");
            string personalNumber = ReadLine();
            char[] arrayPersonNumber = personalNumber.ToCharArray();

            //Removing char: - and + for to do the rest of the calculations
            if(
                (arrayPersonNumber.Any(c => c.ToString() == "-") 
                    || arrayPersonNumber.Any(c => c.ToString() == "+")) 
                && !(arrayPersonNumber[arrayPersonNumber.Length - 5].ToString() == "-"
                    || arrayPersonNumber[arrayPersonNumber.Length -5].ToString() == "+"))
            {
                WriteLine("Character (-/+) in a wrong position, try again!");
                ValidationPersonalNumber();
                return;
            }

            personalNumber = personalNumber.Replace("+", "");
            personalNumber = personalNumber.Replace("-", "");

            if (personalNumber.Length == 10)
                personalNumber = "19" + personalNumber;

            int year = 0;
            int month = 0;
            int day = 0;
            int nnnBirthNumber = 0;
            int cLastNumber = 0;
            bool isWoman = false;
            bool isMan = false;

            arrayPersonNumber = personalNumber.ToCharArray();
            if (arrayPersonNumber.Length != 12)
            {
                WriteLine("Format invalid: You must introduce 10 or 12 NUMBERS(NOT chararcter)");
                ValidationPersonalNumber();
                return;
            }

            try
            {
                year = int.Parse(String.Join("", arrayPersonNumber.Skip(0).Take(4).ToArray()));
            }
            catch (Exception)
            {
                WriteLine("Invalid year");
                ValidationPersonalNumber();
                return;
            }

            try
            {
                month = int.Parse(String.Join("", arrayPersonNumber.Skip(4).Take(2).ToArray()));
            }
            catch (Exception)
            {

                WriteLine("Invalid month");
                ValidationPersonalNumber();
                return;
            }

            try
            {
                day = int.Parse(String.Join("", arrayPersonNumber.Skip(6).Take(2).ToArray()));
            }
            catch (Exception)
            {

                WriteLine("Invalid day");
                ValidationPersonalNumber();
                return;
            }

            try
            {
                nnnBirthNumber = int.Parse(String.Join("", arrayPersonNumber.Skip(8).Take(3).ToArray()));
                
                
            }
            catch (Exception)
            {

                WriteLine("Invalid birth number");
                ValidationPersonalNumber();
                return;
            }

            
            try
            {
                cLastNumber = int.Parse(String.Join("", arrayPersonNumber.Skip(11).Take(1).ToArray()));
                
            }
            catch (Exception)
            {

                WriteLine("Invalid character");
                ValidationPersonalNumber();
                return;
            }

            if (((nnnBirthNumber % 2) == 0) || nnnBirthNumber == 0)
            {
                isWoman = true;

            }
            else if ((nnnBirthNumber % 2) == 1)
            {
                isMan = true;
            }
            else
            {
                WriteLine("Introduce a valid personal number");
            }




            if (!(year >= 1753 && year <= 2021))
            {
                WriteLine("Introduce a valid year");
                ValidationPersonalNumber();
                return;
            }

            if (!(month >= 1 && month <= 12))
            {
                WriteLine("Introduce a valid month");
                ValidationPersonalNumber();
                return;
            }

            int[] month30 = new int[] { 4, 6, 9, 11 };
            if (month30.Any(x => x == month) && day > 30)
            {
                WriteLine("Introduce a valid day for this month");
                ValidationPersonalNumber();
                return;
            }

            // Logic for Leap Year calculation
            bool leapYear = true;
            if (year % 4 != 0 || (year % 100 == 0 && year % 400 != 0))
            {
                leapYear = false;
            }

            if (!leapYear && month == 2 && day > 28)
            {
                WriteLine("This year is not a Leap year, you can´t introduce more than 28 days in february");
                ValidationPersonalNumber();
                return;
            }

            if (month == 2 && day > 29)
            {
                WriteLine("Introduce a valid day for this month");
                ValidationPersonalNumber();
                return;
            }

            if (day > 31)
            {
                WriteLine("Introduce a valid day for this month");
                ValidationPersonalNumber();
                return;
            }

            //Birthday number nnn validation

            if (!(nnnBirthNumber >= 0 && nnnBirthNumber <= 999) )
            {
                WriteLine("Introduce a valid birthday number!");
                ValidationPersonalNumber();
                return;
            }
            //Evaluating the year 
            string date = "" + year + "-" + month + "-" + day; 
            if(DateTime.Parse("2021-01-01") < DateTime.Parse(date))
            {
                WriteLine("Introduce a valid date!");
                ValidationPersonalNumber();
                return;
            }

            if (!luhn(personalNumber))
            {
                WriteLine("Control digit by Luhn algorithm is NOT valid, try again!");
                ValidationPersonalNumber();
                return;
            }

            WriteLine("This personal number by Luhn algorithm is RIGHT");
            WriteLine("This personal number is valid");
            if (isWoman)
            {
                Write("This personal number belongs to a Woman \n");
            }
            else if (isMan)
            {
                Write("This personal number belongs to a Man \n");
            }
            else
            {
                WriteLine("Introduce a right gender");
            }

            WriteLine("Do you want test another Personal number? Press: S ");
            string continueProgram = ReadLine();
            if (continueProgram.ToLower() == "s")
                ValidationPersonalNumber();
            return;
        }

        static bool luhn(string personalNumber)
        {
            // Split char for to do the Array
            char[] perNumArray = personalNumber.ToCharArray();
            // Loop
            int sum = 0;
            int multiplicator = 2;
            int initLoop = perNumArray.Length == 12 ? 2 : 0;
            for (int i = initLoop; i < personalNumber.Length -1; i++)
            {
                int digit = int.Parse(perNumArray[i].ToString());
                int res = digit * multiplicator;

                multiplicator = multiplicator == 2 ? multiplicator - 1 : multiplicator + 1; 
                if(res > 9)
                {
                    char[] resArray = res.ToString().ToCharArray();
                    res = int.Parse(resArray[0].ToString()) + int.Parse(resArray[1].ToString());
                }
                sum += res;
            }

            int controlLuhn = (10 - (sum % 10)) % 10;
            return controlLuhn.ToString() == perNumArray[personalNumber.Length - 1].ToString();
        }

    }
}
