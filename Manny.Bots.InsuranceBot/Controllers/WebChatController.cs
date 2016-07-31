using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Configuration;

namespace Manny.Bots.InsuranceBot
{
      public class WebChatController : ApiController
    {
       public async Task<string> Get()
        {
            string webChatSecret = ConfigurationManager.AppSettings["WebChatSecret"];
            return $"<iframe src='https://webchat.botframework.com/embed/AssistantBot04?s=LIhbmQ1vomI.cwA.jFY.2TDZm29WjaNCcfOT2H6ppKbs9O7AvsIaEgcxUUmEOk8' style='height: 502px; max-height: 502px;'></iframe>";
        }
    }
}