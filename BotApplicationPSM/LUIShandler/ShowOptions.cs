using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using BotApplicationPSM.Dialogs;
using System.Collections.Generic;

namespace BotApplicationPSM.LUIShandler
{
    [Serializable]
    public class ShowOptions : IDialog<object>
    {
        public IEnumerable<string> Myoptions = new[] { "Events", "Cancel", "FAQ"/*, "Blood_donation", "dental", "eye_checkup"*/ };
        public async Task StartAsync(IDialogContext context)
        {
            PromptDialog.Choice(context, select, Myoptions, "Please select one of the following options: - ", "Please try again", 3);
            //context.Wait(ListOfoptions);
        }

        private async Task select(IDialogContext context, IAwaitable<string> result)
        {
            string act1;
            act1 = await result;                                        //storing result obtained in previous PromptDialog.Choice as string 
            act1 = act1.ToLower();                                      //for invariant comparisons
            if (act1 == "events")                                       //case 1    
            {
                //await DisplayHeroCard(context);                         //display hero cards with list of events
                context.Call(new Showevents(), adone);
            }
            else if (act1.ToLowerInvariant() == "cancel")                    //case 2
            {
                //var re = context.MakeMessage();                         //make new message
                //re.Text = "Cancel working";                             //add text to message
                //await context.PostAsync(re);                            //display message text
                context.Call(new Cancelbooking(), adone);

            }
            else if (act1.ToLowerInvariant() == "faq")
            {
                //var re = context.MakeMessage();
                //re.Text = "FAQ working";
                //await context.PostAsync(re);
                context.Call(new FAQDialog(), adone);
            }
            //else if (act1.ToLowerInvariant() == "blood_donation")
            //{
            //    context.Call(new Blooddonationcnf(), adone);
            //}
            //else if (act1.ToLowerInvariant() == "dental")
            //{
            //    context.Call(new Dentalcnf(), adone);
                
            //}
            //else if (act1.ToLowerInvariant() == "eye_checkup")
            //{
            //    context.Call(new Eyecnf(), adone);
            //}

            else
            {
                var re = context.MakeMessage();
                re.Text = "Try something from the options";
                await context.PostAsync(re);
            }
            //context.Done<bool>(true);
        }

        private async Task adone(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<bool>(true);
        }


    }
}