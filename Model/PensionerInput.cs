using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionDisbursementApi.Model
{
    public class PensionerInput
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PAN { get; set; }
        public string AadhaarNumber { get; set; }
        public PensionType PensionType { get; set; }
    }
   
}
