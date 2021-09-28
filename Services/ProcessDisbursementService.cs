using PensionDisbursementApi.Model;
using PensionDisbursementApi.Repository.IRepository;
using PensionDisbursementApi.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionDisbursementApi.Services
{
    public class ProcessDisbursementService : IProcessDisbursementService
    {
        private IProcessDisburementRepository processDisburementRepository;


        public ProcessDisbursementService(IProcessDisburementRepository processDisburementRepository)
        {
            this.processDisburementRepository = processDisburementRepository;

        }

        public async Task<PensionerOutput> GetCodeService(ProcessPensionInput processPensionInput,string token="")
        {
            var obj = await processDisburementRepository.GetPensionerDetail(processPensionInput,token);
            return obj;
        }

        public bool ProcessedValueService(BankType banktype, int bankservicecharge)
        {
            var obj = processDisburementRepository.ProcessedValue(banktype, bankservicecharge);
            return obj;
        }
    }
}
