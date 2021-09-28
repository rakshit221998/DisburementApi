using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionDisbursementApi.Model
{
    public class PensionerOutput
    {
        public string Name { get; set; }
        public DateTime DateOfbirth { get; set; }
        public string Pan { get; set; }
        public string AadharNumber { get; set; }
        public PensionType PensionType { get; set; }
        public BankType BankType { get; set; }
        public double PensionerAmount { get; set; }
        //public int BankServiceCharge { get; set; }
    }
    public enum BankType
    {
        Public = 1,
        Private = 2
    }
    public enum PensionType
    {
        Self = 1,
        Family = 2
    }
}
