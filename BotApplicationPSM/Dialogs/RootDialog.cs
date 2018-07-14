using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using BotApplicationPSM.LUIShandler;
using Microsoft.Bot.Builder.FormFlow;

namespace BotApplicationPSM.Dialogs
{
    [LuisModel("3229323984cc4ba8b9e24009915a8dc1", "61ec1452421a490c9dec2c0962c1d2c6")]
    [Serializable]
    
    public class RootDialog : LuisDialog<object>
    {
        private async Task luisdone(IDialogContext context, IAwaitable<object> result) //method to end all other methods
        {
            context.Done<bool>(true);

        }
        [LuisIntent("Help")]
        private async Task Help(IDialogContext context, LuisResult result)
        {
              context.Call(new ShowOptions(), luisdone);                //calls the ShowOptions dialog
        }
         [LuisIntent("Blood_donation")]
        private async Task Blood_donation(IDialogContext context, LuisResult result)
        {
              context.Call(new Blooddonationcnf(), luisdone);           //calls blood donation dialog
        }

        [LuisIntent("Cancellation")]
        private async Task Cancellation(IDialogContext context, LuisResult result)
        {
            DBconnection obj = new DBconnection();
            var empl = obj.GetBooking();

            context.PostAsync("Name: " + empl.pname.ToUpper());
            context.PostAsync("Age: " + empl.p_age);
            context.PostAsync("Blood group: " + empl.bgroup);
            context.PostAsync("Phone no: " + empl.pphone);
            context.PostAsync("Date: " + empl.Appdate);
            context.PostAsync("This appointment was cancelled.");
            DBconnection obj1 = new DBconnection();
            obj1.deleteBooking(empl.BookingId);
            context.Done<object>(null);
            // context.Call(new Cancelbooking(), luisdone);                //calls cancel booking dialog
        }

        [LuisIntent("Dental_booking")]
        private async Task Dental_booking(IDialogContext context, LuisResult result)
        {
            context.Call(new Dentalcnf(), luisdone);                    //calls dental booking dialog
        }

        [LuisIntent("FAQ")]
        private async Task FAQ(IDialogContext context, LuisResult result)
        {
            context.Call(new FAQDialog(), luisdone);                    //redirects to FAQ
        }
        [LuisIntent("Showevents")]
        private async Task Showevents(IDialogContext context, LuisResult result)
        {
            context.Call(new Showevents(), luisdone);                    //displays all events  in caraousel format
        }
        [LuisIntent("Eyecare")]
        private async Task Eyecare(IDialogContext context, LuisResult result)
        {
            context.Call(new Eyecnf(), luisdone);                   //calls eye care booking dialog
        }
        //[LuisIntent("Show_booking")]
        //private async Task Show_booking(IDialogContext context, LuisResult result)
        //{
        //    //context.Call(new (), luisdone);                    //to add show booking
        //}
        [LuisIntent("Show_booking")]
        private async Task Show_booking(IDialogContext context, LuisResult result)
        {
            var myform = new FormDialog<BookingClass>(new BookingClass(), BookingClass.BuildForm, FormOptions.PromptInStart, null);

            context.Call<BookingClass>(myform, final);

        }

        private async Task final(IDialogContext context, IAwaitable<BookingClass> result)
        {
            //var res = await result;

            int temp;
            string name;
            string bgp;
            string pno;
            string newdate;
            //context.PrivateConversationData.SetValue("Name", "Test1");
            // context.PrivateConversationData.SetValue("Time", res.ToString());

            context.PrivateConversationData.TryGetValue("pname", out name);
            context.PrivateConversationData.TryGetValue("p_age", out temp);
            context.PrivateConversationData.TryGetValue("bgroup", out bgp);
            context.PrivateConversationData.TryGetValue("pphone", out pno);
            context.PrivateConversationData.TryGetValue("Appdate", out newdate);

            Class1 obj1 = new Class1();
            obj1.pname = name;
            obj1.p_age = temp;
            obj1.bgroup = bgp;
            obj1.pphone = pno;
            obj1.Appdate = newdate;
            DBconnection obj = new DBconnection();
            obj.BookingAppt(obj1);

            await context.PostAsync("Your appointment has been booked.");
            context.Done<bool>(true);
        }
        private async Task Howto_eyes(IDialogContext context, LuisResult result)
        {
            context.PostAsync("Eat well. Good eye health starts with the food on your plate. "
           +"Quit smoking."+"\n"
         
           +" Use safety Eyewear."+"\n"+
            " Look away from the Computer Screen."+"\n"+
            " Visityour EyeDoctor regularly.");
        }
        private async Task Howto_teeth(IDialogContext context, LuisResult result)
        {
            context.PostAsync("Brush your teeth 2 times a day with fluoride (“FLOOR-ide”) toothpaste."+"\n"+
        "Floss between your teeth every day."+"\n"+
        "Visit a dentist regularly for a checkup and cleaning."+"\n"+
                "If you drink alcohol, drink only in moderation.");
        }
        private async Task Dental_help(IDialogContext context, LuisResult result)
        {
            context.PostAsync("You can meet renowned dentists and get a check up of your teeth.  Services offered include dental check-up, tooth extraction, scaling, fillings to treat cavities, x-ray, laser treatment etc.  ");

        }


        private async Task Blood_group(IDialogContext context, LuisResult result)
        {
            context.PostAsync("Blood transfusions can only happen between people with compatible blood groups. If an incompatible blood group's blood is transfused to a patient, the results can be fatal too. Hence there is a strict necessity for all blood donors to know there blood groups." + "\n" +
            "   For more information, please visit th following links: -" + "\n" +
            "    http://www.ncbb.org/general-donation-facts " + "\n" +
            "    http://www.carterbloodcare.org/blood-facts/blood-types/");
        }
        private async Task Fee(IDialogContext context, LuisResult result)
        {
            context.PostAsync("All camps are free.");
        }
        private async Task Can_donate(IDialogContext context, LuisResult result)
        {
            context.PostAsync("The donor must be fit and healthy, and should not be suffering from transmittable diseases."+"\n"+
            "He / She must be 18–65 years old and should weigh a minimum of 50kg"+"\n"+
            "   For more information, please visit th following links: -" + "\n"+
            "https://www.organicfacts.net/health-benefits/other/blood-donation.html"
           );
        }
        private async Task Cannot_donate(IDialogContext context, LuisResult result)
        {
            context.PostAsync("People who meet the following conditions are forbidden to donate blood:-" + "\n" +

"     Anyone younger than 18 or elder than 65 years old." + "\n" +

"    Individuals suffering from ailments like  blood pressure, cancer,  kidney ailments and diabetes." + "\n" +
"Women who are pregnant or breastfeeding." 
);
        }
        [LuisIntent("Blood_health_effects")]
        private async Task Blood_health_effects(IDialogContext context, LuisResult result)
        {
            context.PostAsync("Over the years, various studies have suggested that donating blood can actually be good for a person's long-term health because blood donations keep the body's iron levels in check, regular donors may lower their risk of strokes and heart disease, or (more contentiously) cancer");
        }
        [LuisIntent("side_effects_blood")]
        private async Task side_effects_blood(IDialogContext context, LuisResult result)
        {
            context.PostAsync("Sometimes people who donate blood notice a few minor side effects like nausea,lightheadedness, dizziness, or fainting, but these symptoms usually go away quickly. The donor's body usually replaces the liquid part of blood (plasma) within 72 hours after giving blood.    	");
        }
        [LuisIntent("Thanks")]
        private async Task Thanks(IDialogContext context, LuisResult result)
        {
            context.PostAsync("It's been a pleasure to help you. Have a great day!");
            context.Call(new RootDialog(),luisdone);                    //displays all events  in caraousel format
        }
    }

}
