The prerequisites are listed below.
•	Create a new Dynamics 365 application in Azure. 
•	Make that the newly created Azure Function builds appropriately. 
•	To establish a connection with the Dataverse, use this sample of code.
Create a new Dynamics 365 application in Azure:

We need to register the Dynamics 365 Web API on Azure and use it with an application user. This is particularly useful for those working with custom APIs in CRM.

To achieve this, you will need the following components.
•	Azure portal access
•	Application user
•	CRM Admin user

Create Azure Function builds appropriately:
I am writing below Azure function to create a contact record through the Service client.
The subsequent action involves deploying the Azure function and then conducting a test using Postman or a web browser. 
Conducting a test of the Azure function using Postman.
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
                string _clientId = "xxxxx";
                string _clientSecret = "xxxxxxx";
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
Copy the local host URl and send post request from postman as shown in below screen.
 
 

Below records is created in dynamic in contact entity.
 

Thank you...!
