using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IBAN_Verificator.Models
{
    public class IbanResponse
    {
        public string Iban { get; set; }
        public string Bank { get; set; }
        public bool IsValid { get; set; }
    }
}