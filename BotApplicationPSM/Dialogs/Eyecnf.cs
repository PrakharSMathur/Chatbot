using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.FormFlow;
using BotApplicationPSM.LUIShandler;

namespace BotApplicationPSM.Dialogs
{
    [Serializable]
    public class Eyecnf : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.PostAsync("Welcome to the eye-care booking");
            PromptDialog.Number(context, decide1, "Enter you age");         //prompts user for age
        }

        private async Task decide1(IDialogContext context, IAwaitable<long> result)
        {
            long num = await result;
            context.PrivateConversationData.SetValue("p_age",num );
            if ((num>=10)  && (num <= 75))                                  //compare if entered age is in set limits
            {
                PromptDialog.Confirm(context, decide2, "Do you wear spectacles/lenses?"); //another eligibility check
            }
            else
            {
                context.PostAsync("Eye test not needed");
                context.Done<bool>(true);
            }
        }

        private async Task decide2(IDialogContext context, IAwaitable<bool> result)
        {
            var res = await result;
            
            if (res)
            {
                await context.PostAsync("You are eligible for booking!");
                context.PrivateConversationData.SetValue("event1", "EyeCare");
                letsbook(context);
                
            }
            else
            {
                context.PostAsync("Sorry! We don't have specialists for your age group");
                context.Done<bool>(true);
            }
        }
        private async Task letsbook(IDialogContext context)
        {
            var myform = new FormDialog<BookingClass>(new BookingClass(), BookingClass.BuildForm, FormOptions.PromptInStart, null);

            context.Call<BookingClass>(myform, final);

        }

        private async Task final(IDialogContext context, IAwaitable<BookingClass> result)
        {
            //context.PostAsync("Re-directing you");
            int num=0;
            string name;
            string bgp;
            string pno;
            string newdate;
            //context.PrivateConversationData.SetValue("Name", "Test1");
            // context.PrivateConversationData.SetValue("Time", res.ToString());
            string choice = "EyeCamp";
            context.PrivateConversationData.TryGetValue("pname", out name);
           context.PrivateConversationData.TryGetValue("p_age", out num);
            context.PrivateConversationData.TryGetValue("bgroup", out bgp);
            context.PrivateConversationData.TryGetValue("pphone", out pno);
            context.PrivateConversationData.TryGetValue("Appdate", out newdate);
            context.PrivateConversationData.SetValue("event1", "EyeCamp");
            Class1 obj1 = new Class1();
            obj1.pname = name;
            obj1.p_age = num;
            obj1.bgroup = bgp;
            obj1.pphone = pno;
            obj1.Appdate = newdate;
            obj1.event1 = choice;
            DBconnection obj = new DBconnection();
            obj.BookingAppt(obj1);

            await context.PostAsync("Your appointment has been booked.");

            context.Call(new ShowOptions(), wel);
            //context.Done<bool>(true);
        }

        private async Task wel(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<bool>(true);
        }

    }
}