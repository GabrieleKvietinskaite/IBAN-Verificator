using IBAN_Verificator.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IBAN_Verificator.Repositories
{
    public class IbanRepository : IIbanRepository
    {
        private readonly Dictionary<string, string> Banks = new Dictionary<string, string>(){
            {"SEB", "70440"},
            {"Swedbank", "73000"},
            {"Luminor", "40100"}
        };
        private readonly string LithuanianCountryCodeInDigits = "2129";

        public IbanResponse CheckIban(string iban)
        {
            if (iban.Length != 20 || !IsCountryCodeValid(iban)) 
            {
                return new IbanResponse
                {
                    Iban = iban,
                    Bank = String.Empty,
                    IsValid = false
                };
            }

            string bankCode = iban.Substring(4, 5);
            string controlNumbers = iban.Substring(2, 2);
            string bban = iban.Substring(9, 11);

            return new IbanResponse
            {
                Iban = iban,
                Bank = DetermineBank(bankCode),
                IsValid = ValidateIban(bankCode, bban, controlNumbers)
            };
        } 

        public List<IbanResponse> CheckIbans(string[] ibans)
        {
            var results = new List<IbanResponse>();

            foreach (string iban in ibans)
            {
                results.Add(CheckIban(iban));
            }
            return results;
        }

        private bool IsCountryCodeValid(string iban)
        {
            string countryCode = iban.Substring(0, 2);

            return countryCode == "LT";
        }

        private string DetermineBank(string bankCode)
        {
            if (this.Banks.ContainsValue(bankCode))
            {
                return this.Banks.FirstOrDefault(x => x.Value == bankCode).Key;
            }
            else
            {
                return string.Empty;
            }
        }

        private bool ValidateIban(string bankCode, string bban, string controlNumbers)
        {
            string number = bankCode + bban + this.LithuanianCountryCodeInDigits + controlNumbers;
            int remainder;

            while(number.Length >= 9)
            {
                remainder = Int32.Parse(number.Substring(0, 9)) % 97;
                number = number.Replace(number.Substring(0, 9), remainder.ToString());           
            }
            remainder = Int32.Parse(number.Substring(0)) % 97;

            return remainder == 1;
        }
    }
}