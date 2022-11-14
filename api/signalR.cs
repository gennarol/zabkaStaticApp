using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace SignalR
{

    public class Transaction
    {
        
        public string ConnectionId { get; set; }
        public String ServiceName { get; set; }
        public String Quantity { get; set; }
        public String Amount { get; set; }

    }
    public static class signalR
    {
        [FunctionName("negotiate")]
        public static SignalRConnectionInfo GetSignalRInfo(
          [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
          [SignalRConnectionInfo(HubName = "posTransaction")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }


        [FunctionName("messages")]
        public static async Task<string> SendMessage(
              [HttpTrigger(AuthorizationLevel.Anonymous, "post")] Transaction message,
              [SignalR(HubName = "posTransaction")] IAsyncCollector<SignalRMessage> signalRMessages)
        {            
            await signalRMessages.AddAsync(
                new SignalRMessage
                {   Target = "posTransaction",                    
                    Arguments = new[] { message }                    
                }) ;

            return "Ok";

        }
    }
}
