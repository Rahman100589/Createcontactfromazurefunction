using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;

namespace CreateContactfromAzurefunction
{
    public static class Function1
    {
        [FunctionName("Createcontactrecord")]
        public static async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
    ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var _contactid = new Guid();
            try
            {
                string _clientId = "e69c80b6-615d-4c4f-97c5-88c9306e1aae";
                string _clientSecret = "sPZ8Q~152D_aVvp4TO_Fxem7iTLSP0B4Ab1OOaqI";
                string _environment = "org18828102.crm5";
                var _connectionString = @$"Url=https://{_environment}.dynamics.com;AuthType=ClientSecret;ClientId={_clientId}
                ;ClientSecret={_clientSecret};RequireNewInstance=true";


                var service = new ServiceClient(_connectionString);
                if (service.IsReady)
                {
                    _contactid = await GetContacts(service);
                }


            }
            catch (Exception ex)
            {
              return  new  OkObjectResult(ex.Message);
                throw new(ex.Message);

            }
            OkObjectResult testrecord = new OkObjectResult("Contact Record created with ID " + Convert.ToString(_contactid));
            return testrecord;
        }

        private static async Task<Guid> GetContacts(ServiceClient service)
        {
            Guid _contactid;
            // Create a contact 
            
            Entity contact = new Entity("contact")
            {
                ["firstname"] = "Rahman",
                ["lastname"] = "Dynamics CRM"
            };
            _contactid = service.Create(contact);
            return  _contactid;
        }

        
    }
}
