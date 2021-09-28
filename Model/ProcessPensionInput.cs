using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionDisbursementApi.Model
{
        public class ProcessPensionInput
        {
            public string AadhaarNumber { get; set; }
            public double PensionAmount { get; set; }
            public int BankCharge { get; set; }

        }
    
}
