using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;
using Microsoft.Bot.Builder.FormFlow;
using BotApplicationPSM.LUIShandler;

namespace BotApplicationPSM.Dialogs
{
    [Serializable]
    public class Blooddonationcnf : IDialog<object>
    {
        public IEnumerable<string> Optionslist1 = new[] { "Yes", "No" };
        public async Task StartAsync(IDialogContext context)
        {
            //context.PostAsync("Welcome to blood donation registration");
            PromptDialog.Number(context, select1, "Please enter your age", "", 3);

        }


        private async Task select1(IDialogContext context, IAwaitable<long> result)
        {
            long  choice4 = await result;
            int choice1 = (int)choice4;
            if ((choice1 >= 16) && (choice1 <= 60))
            {
                context.PrivateConversationData.SetValue("p_age", choice1);
                PromptDialog.Confirm(context, select2, "Are you diabetic?", "", 2, PromptStyle.Auto);
            }
            else
            {
                await context.PostAsync("ummm...It seems you are not eligible to donate blood, please look for some other activity");
                context.Call(new Showevents(), wel);
            }
        }

        private async Task select2(IDialogContext context, IAwaitable<bool> result)
        {
            var choice2 = await result;

            if (!choice2)
            {
                string event1;
                context.PrivateConversationData.SetValue("event1", "BloodDonation");
                await letsbook(context);
                
            }
            else
            {
                await context.PostAsync("ummm...It seems you are not eligible to donate blood, please look for some other activity");
                context.Call(new Showevents(), wel);
            }

        }

        private async Task letsbook(IDialogContext context)
        {
            context.PostAsync("Congratulations! You are eligible to donate blood!");
            var myform = new FormDialog<BookingClass>(new BookingClass(), BookingClass.BuildForm, FormOptions.PromptInStart, null);

            context.Call<BookingClass>(myform, final);

        }

        private async Task final(IDialogContext context, IAwaitable<BookingClass> result)
        {
            //context.PostAsync("Re-directing you");
           int choice1;
            string name;
            string bgp;
            string pno;
            string newdate;
            //context.PrivateConversationData.SetValue("Name", "Test1");
            // context.PrivateConversationData.SetValue("Time", res.ToString());
            string choice3 = "BloodDonation";
            context.PrivateConversationData.TryGetValue("pname", out name);
            context.PrivateConversationData.TryGetValue("p_age", out choice1);
            context.PrivateConversationData.TryGetValue("bgroup", out bgp);
            context.PrivateConversationData.TryGetValue("pphone", out pno);
            context.PrivateConversationData.TryGetValue("Appdate", out newdate);
            context.PrivateConversationData.SetValue("event1", "BloodDonation");
            Class1 obj1 = new Class1();
            obj1.pname = name;
            obj1.p_age = choice1;
            obj1.bgroup = bgp;
            obj1.pphone = pno;
            obj1.Appdate = newdate;
            obj1.event1 = choice3;
            DBconnection obj = new DBconnection();
            obj.BookingAppt(obj1);

            await context.PostAsync("Your appointment has been booked.");

            context.Call(new ShowOptions(), wel);
        }

        private async Task wel(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<bool>(true);
        }
    }
}