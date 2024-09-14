using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;

namespace API_Padaria_Mouts.Utilities
{
    public class Utility
    {
        public static bool validDocument(string document)
        {
            return validCPF(document) || validCNPJ(document);
        }

        public static bool validCPF(string cpf)
        {
            string aux = Regex.Replace(cpf, "[^0-9]", "");

            if (aux.Length != 11)
            {
                return false;
            }

            if (aux.Distinct().Count() == 1)
            {
                return false;
            }
                

            char cd; //check digit
            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += ((aux[i] - 48) * (10 - i));
            }

            int reminder = 11 - (sum % 11);
            if (reminder >= 10)
            {
                cd = '0';
            }
            else
            {
                cd = (char)(reminder + 48);
            }

            if (aux[9] != cd)
            {
                return false;
            }

            sum = 0;

            for (int i = 0; i < 10; i++)
            {
                sum += ((aux[i] - 48) * (11 - i));
            }

            reminder = 11 - (sum % 11);

            if (reminder >= 10)
            {
                cd = '0';
            }
            else
            {
                cd = (char)(reminder + 48);
            }

            if (aux[10] != cd)
            {
                return false;
            }

            return true;

        }

        public static bool validCNPJ(string cnpj)
        {
            int[] multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string aux = Regex.Replace(cnpj, "[^0-9]", "");

            if (aux.Length != 14)
                return false;

            if (aux.Distinct().Count() == 1)
                return false;

            // Calculate the first check digit
            int sum = 0;
            for (int i = 0; i < 12; i++)
                sum += int.Parse(aux[i].ToString()) * (11 - i);
            int remainder = sum % 11;
            int firstCheckDigit = remainder < 2 ? 0 : 11 - remainder;
            if (firstCheckDigit != int.Parse(aux[12].ToString()))
                return false;

            // Calculate the second check digit
            sum = 0;
            for (int i = 0; i < 13; i++)
                sum += int.Parse(aux[i].ToString()) * (12 - i);
            remainder = sum % 11;
            int secondCheckDigit = remainder < 2 ? 0 : 11 - remainder;
            if (secondCheckDigit != int.Parse(aux[13].ToString()))
                return false;

            return true;
        }

    }
}
