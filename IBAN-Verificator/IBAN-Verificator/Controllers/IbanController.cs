using IBAN_Verificator.Models;
using IBAN_Verificator.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace IBAN_Verificator.Controllers
{
    [ApiController]
    [Route("api")]
    public class IbanController : ControllerBase
    {
        public readonly IIbanRepository IbanRepository;
        public IbanController(IIbanRepository ibanRepository)
        {
            this.IbanRepository = ibanRepository ??
                throw new ArgumentNullException(nameof(ibanRepository));
        }

        [HttpPost("iban")]
        public ActionResult<IbanResponse> CheckIban([FromBody] string iban)
        {
            var result = this.IbanRepository.CheckIban(iban);

            return Ok(result);
        }
        
        [HttpPost("ibans")]
        public ActionResult<List<IbanResponse>> CheckIbans([FromBody] string[] ibans)
        {
            var result = this.IbanRepository.CheckIbans(ibans);

            return Ok(result);
        }
    }
}