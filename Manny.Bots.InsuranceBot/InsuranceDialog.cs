using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Luis.Models;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace Manny.Bots.InsuranceBot
{
    [LuisModel("xxxxxxx", "xxxxxxxx")]
    [Serializable]
    public class InsuranceDialog : LuisDialog<object>
    {
        [LuisIntent("Enquire")]
        //Intent is to enquire about Insurance
        public async Task Enquire(IDialogContext context, LuisResult result)
        {
            PromptDialog.Confirm(
                    context: context,
                  resume: HandleInsuranceOptions,
                    prompt: "Do you want to know about our insurance?",
                    retry: "Didn't get that!");


            //context.Wait(MessageReceived);
        }

        [LuisIntent("Engage")]
        public async Task Engage(IDialogContext context, LuisResult result)
        {
            PromptDialog.Confirm(
                   context: context,
                 resume: HandleRegisterInterest,
                   prompt: "Do you want to register your interest? We will be in touch.",
                   retry: "Didn't get that!");
        }

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            string message = "Hi there. Welcome to BestPrice.";
            await context.PostAsync(message);            
            context.Wait(MessageReceived);
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Sorry I didn't understand that");
            await Conversation.SendAsync(context.MakeMessage(), CreateDefaultInusuranceOptions);
            context.Wait(MessageReceived);
        }

        internal static IDialog<Insurance> CreateDefaultInusuranceOptions()
        {
            return Chain.From(() => FormDialog.FromForm(Insurance.BuildForm));
        }

        public async Task HandleInsuranceOptions(IDialogContext context, IAwaitable<bool> argument)
        {
            var confirm = await argument;
            if (confirm)
            {
                var ops = (IEnumerable<TypeOfInsuranceOptions>)Enum.GetValues(typeof(TypeOfInsuranceOptions));
                PromptDialog.Choice<TypeOfInsuranceOptions>(context, ResumeTypeOptionsAsync, ops, "Let us know what are you interested in?");
            }
            else
            {
                await context.PostAsync("Let me understand you more. Come again");
                context.Wait(MessageReceived);
            }           
        }

        public async Task HandleRegisterInterest(IDialogContext context, IAwaitable<bool> argument)
        {
            var confirm = await argument;
            if (confirm)
            {
                await context.PostAsync("Thanks we have registered your interest and will be in touch.");
                context.Wait(MessageReceived);
            }
            else
            {
                await context.PostAsync("Let me understand you more. Come again");
                context.Wait(MessageReceived);
            }
        }

        public async Task ResumeTypeOptionsAsync(IDialogContext context, IAwaitable<TypeOfInsuranceOptions> argument)
        {
            var selection = await argument;
            var insuranceData = new InsuranceProduct();

            await context.PostAsync("Below are the policies that might interest you");
            var products = insuranceData.GetInsuranceProducts(selection);
            foreach(InsuranceProduct p in products)
            {
                await context.PostAsync(p.ToString());
            }
            context.Wait(MessageReceived);
        }
    }
}