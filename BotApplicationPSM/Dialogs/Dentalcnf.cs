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
    public class Dentalcnf : IDialog<object>
    {
        public IEnumerable<string> Optionslist1 = new[] { "Yes", "No" };
        public async Task StartAsync(IDialogContext context)
        {
            context.PostAsync("Welcome to dental centre");
            context.PrivateConversationData.SetValue("event1", "Dental");
            PromptDialog.Number(context, select1, "Enter your age", "", 3);
            //return Task.CompletedTask;
        }
        private async Task select1(IDialogContext context, IAwaitable<long> result)
        {
            var choice1 = await result;
            
            context.PrivateConversationData.SetValue("p_age",  choice1);
            if ((choice1>=10 )&&(choice1<=70))
            {
                await letsbook(context);                                    
        }
            else
            {
                await context.PostAsync("ummm...It seems you are not eligible for DENTAL check up, please look for some other activity");
                context.Call(new Showevents(), done);
            }
        }
        private async Task letsbook(IDialogContext context)
        {
            var myform = new FormDialog<BookingClass>(new BookingClass(), BookingClass.BuildForm, FormOptions.PromptInStart, null);
            //context.PostAsync("Congratulations");
            context.Call(myform, final);

        }

        private async Task final(IDialogContext context, IAwaitable<BookingClass> result)
        {
            //context.PostAsync("Re-directing you");
            //int temp;
            string name;
            string bgp;
            string pno;
            string newdate;
            //context.PrivateConversationData.SetValue("Name", "Test1");
            // context.PrivateConversationData.SetValue("Time", res.ToString());
            int choice1=0;
            string choice2 = "Dental";
            context.PrivateConversationData.TryGetValue("pname", out name);
            context.PrivateConversationData.TryGetValue("bgroup", out bgp);
            context.PrivateConversationData.TryGetValue("pphone", out pno);
            context.PrivateConversationData.TryGetValue("Appdate", out newdate);
            context.PrivateConversationData.TryGetValue("p_age",out choice1);
            //context.PrivateConversationData.TryGetValue("event1",out choice2  );
            context.PrivateConversationData.SetValue("event1", "Dental");
            Class1 obj1 = new Class1();
            obj1.pname = name;
            obj1.p_age = choice1;// temp;
            obj1.bgroup = bgp;
            obj1.pphone = pno;
            obj1.Appdate = newdate;
            obj1.event1 = choice2;
            DBconnection obj = new DBconnection();
            obj.BookingAppt(obj1);

            await context.PostAsync("Your appointment has been booked.");
            context.Call(new ShowOptions(), done);
        }

        private async Task done(IDialogContext context, IAwaitable<object> result)
        {
            
            context.Done<bool>(true);                   //flow stops here 
        }
        

    }
}