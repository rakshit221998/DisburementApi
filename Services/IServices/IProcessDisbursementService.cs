using PensionDisbursementApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionDisbursementApi.Services.IServices
{
    public interface IProcessDisbursementService
    {
        Task<PensionerOutput> GetCodeService(ProcessPensionInput processPensionInput,string token);
        bool ProcessedValueService(BankType banktype, int bankservicecharge);
    }
}
