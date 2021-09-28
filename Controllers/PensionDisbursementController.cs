using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PensionDisbursementApi.Model;
using PensionDisbursementApi.Repository.IRepository;
using PensionDisbursementApi.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionDisbursementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PensionDisbursementController : ControllerBase
    {
        private static Dictionary<int, string> dictstatuscode = new Dictionary<int, string>()
        {
            { 10,"Success" },
            { 21,"Pension Amount Calculated is Wrong. Please redo the Calculation" }

        };

        private IProcessDisbursementService processDisbursementService;
        public PensionDisbursementController(IProcessDisbursementService processDisbursementService)
        {
            this.processDisbursementService = processDisbursementService;
        }
        

        [HttpPost("getresponse/{token}")]
        public async Task<IActionResult> GetCode([FromBody]ProcessPensionInput processPensionInput,string token)
        {
            var obj = await processDisbursementService.GetCodeService(processPensionInput,token);

            if (obj == null)
                return BadRequest("No Valid User found fot this Adhaar No");

            var banktype = obj.BankType;

            var bankservicecharge = processPensionInput.BankCharge;

            ProcessPensionResponse processPensionResponse=null;
            int bankCharges = 0;
            if (obj.BankType == (BankType)1)
                bankCharges = 500;
            else
               bankCharges = 550;


            if (processPensionInput.PensionAmount == (obj.PensionerAmount+bankCharges) && processDisbursementService.ProcessedValueService(banktype, bankservicecharge))
            {
                processPensionResponse = new ProcessPensionResponse()
                {
                    ProcessPensionStatusCode = dictstatuscode[10]
                };
                return Ok(processPensionResponse);
            }
            else
            {
                processPensionResponse = new ProcessPensionResponse()
                {
                    ProcessPensionStatusCode = dictstatuscode[21]
                };
                return BadRequest(processPensionResponse);
            }
            
        }
        
        
        


    }
}
