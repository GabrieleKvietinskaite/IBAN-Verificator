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

        [HttpGet("iban")]
        public ActionResult<string> CheckIban()
        {
            var result = this.IbanRepository.CheckIban("LT517180077788877777");

            return Ok(result);
        }
        
        [HttpGet("ibans")]
        public ActionResult<List<IbanResponse>> CheckIbans()
        {
            var result = this.IbanRepository.CheckIbans("AA051245445454552117989:LT647044001231465456:LT517044077788877777");

            return Ok(result);
        }
    }
}