using Newtonsoft.Json;
using PensionDisbursementApi.Model;
using PensionDisbursementApi.Repository.IRepository;
using PensionDisbursementApi.StaticDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PensionDisbursementApi.Repository
{
    public class ProcessDisburementRepository : IProcessDisburementRepository
    {
        private readonly IHttpClientFactory clientFactory; // This IHttpClientFactory we can use due to the services.AddHttpClient()

        public ProcessDisburementRepository(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public async Task<PensionerOutput> GetPensionerDetail(ProcessPensionInput processPensionInput,string token="")
        {
                  
            var url = SD.PensionerDetailBaseAPIPath + "api/PensionerDetail/" +processPensionInput.AadhaarNumber ;
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = clientFactory.CreateClient();

            if (token != null && token.Length != 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            HttpResponseMessage response = await client.SendAsync(request); // Now this cient will pass the request with the content in the particular url mentioned in the request
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync(); // At first the response from the status code in to JSON String
                var pensionerdetailObj=JsonConvert.DeserializeObject<PensionerDetail>(jsonString); // The Json String is converted or Deserailizes into PensionerDetail
                var pensionerinputobj = new PensionerInput
                {
                    Name = pensionerdetailObj.Name,
                    DateOfBirth = pensionerdetailObj.Dateofbirth,
                    PAN = pensionerdetailObj.Pan,
                    AadhaarNumber = pensionerdetailObj.AadharNumber,
                    PensionType = pensionerdetailObj.PensionType
                };
                
                var url1 = SD.ProcessPensionBaseAPIPath + "api/ProcessPension/pensionDetail/"+token;
                var request1 = new HttpRequestMessage(HttpMethod.Get, url1);             
                request1.Content = new StringContent(JsonConvert.SerializeObject(pensionerinputobj), Encoding.UTF8, "application/json");//objectToCreate contains the object to be passed and within the request body it is passed as JSON string in post put as well as get
                var client1 = clientFactory.CreateClient();
                if (token != null && token.Length != 0)
                {
                    client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                HttpResponseMessage response1 = await client1.SendAsync(request1); // Now this cient will pass the request with the content in the particular url mentioned in the request
                if (response1.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString1 = await response1.Content.ReadAsStringAsync(); // At first the response from the status code in to JSON String
                     var obj = JsonConvert.DeserializeObject<PensionerOutput>(jsonString1);
                    return obj;
                }
                else
                    return null;

            }

            else
                return null;

        }

        public bool ProcessedValue(BankType banktype, int bankservicecharge)
        {
            int bankservicecharge1;
            if (banktype == (BankType)1)
                bankservicecharge1 = 500;
            else
                bankservicecharge1 = 550;

            if (bankservicecharge == bankservicecharge1)
                return true;
            else
                return false;
        }
        
    }
}
