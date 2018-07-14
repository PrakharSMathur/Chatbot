using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using BotApplicationPSM.Dialogs;

namespace BotApplicationPSM.LUIShandler
{
    [Serializable]
    [LuisModel("32293239-84cc-4ba8-b9e2-4009915a8dc1", "61ec1452421a490c9dec2c0962c1d2c6")]
    public class LuisHandle1 : LuisDialog<object>
    {
       
        [LuisIntent("Help")]
        private async Task Help(IDialogContext context, IAwaitable<string> result)
        {
             context.Call(new RootDialog(), luisdone);
        }

        private async Task luisdone(IDialogContext context, IAwaitable<object> result)
        {

            await context.PostAsync("luis done");
            context.Done<bool>(true);
        }
        [LuisIntent("Blood_donation")]
        private async Task Blood_donation(IDialogContext context, IAwaitable<string> result)
        {
            context.Call(new Blooddonationcnf(), luisdone);
        }

        [LuisIntent("Cancellation")]
        private async Task Cancellation(IDialogContext context, IAwaitable<string> result)
        {
            context.Call(new Cancelbooking(), luisdone);
        }


        [LuisIntent("Dental_booking")]
        private async Task Dental_booking(IDialogContext context, IAwaitable<string> result)
        {
            context.Call(new Dentalcnf(), luisdone);
        }

        [LuisIntent("FAQ")]
        private async Task FAQ(IDialogContext context, IAwaitable<string> result)
        {
            context.Call(new FAQDialog(), luisdone);
        }
        [LuisIntent("Show_events")]
        private async Task Show_events(IDialogContext context, IAwaitable<string> result)
        {
            context.Call(new Showevents(), luisdone);
        }

     


    }
}