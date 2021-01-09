using IBAN_Verificator.Models;
using System.Collections.Generic;

namespace IBAN_Verificator.Repositories
{
    public interface IIbanRepository
    {
        IbanResponse CheckIban(string iban);
        List<IbanResponse> CheckIbans(string ibans);
    }
}
