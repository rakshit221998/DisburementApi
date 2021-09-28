using PensionDisbursementApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionDisbursementApi.Repository.IRepository
{
    public interface IProcessDisburementRepository
    {
        Task<PensionerOutput> GetPensionerDetail(ProcessPensionInput processPensionInput,string token);
        bool ProcessedValue(BankType banktype, int bankservicecharge);
    }
}
