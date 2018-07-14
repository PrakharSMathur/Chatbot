using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;

namespace BotApplicationPSM.Dialogs
{
    [Serializable]
    public class Showevents : IDialog<object>
    {

        //public IEnumerable<string> Myoptions = new[] { "Events", "Cancel", "FAQ", "test1_ blood donation", "test2_eye checkup", "test3" };
        public async Task StartAsync(IDialogContext context)
        {

            await DisplayHeroCard(context);                         //display hero cards with list of events
        }


        public async Task DisplayHeroCard(IDialogContext context)
        {
            var replyMessage = context.MakeMessage();               //make a new message 
            replyMessage.Attachments = new List<Attachment>();      //define attachments to the message
            replyMessage.AttachmentLayout = "carousel";             //define attachment layout 

            /*here we define the 6 hero cards for the blood donation camp, dental camp, eye camp,
            diabetes camp, pulse polio camp and cancer checkup camp.*/

            var heroCard1 = new HeroCard                            //defining new hero card
            {
                // title of the card  
                Title = "Blood donation",
                //subtitle of the card  
                Subtitle = "Current event",
                //Detail Text  
                Text = "Raqt-daan ," + "\n"
                + Environment.NewLine + " jivan daan. Each drop matters!",
                // list of  Large Image  
                Images = new List<CardImage> { new CardImage("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSMaMccaqPcMTt6Gy3HsY1eirdt2yPaD-kxtEQgcH6zu3bG4-U5") },
                // list of buttons   
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Learn More", value: "https://www.organicfacts.net/health-benefits/other/blood-donation.html"), new CardAction(ActionTypes.PostBack, "Book now", value: "Blood_donation") }
            };

            Attachment plAttachment1 = heroCard1.ToAttachment();
            replyMessage.Attachments.Add(plAttachment1);
            var heroCard2 = new HeroCard
            {
                // title of the card  
                Title = "Dental camp",
                //subtitle of the card  
                Subtitle = "Current event",
                //Detail Text  
                Text = "Renowned dentists from the whole town come together to protect poeple's smiles!",
                // list of  Large Image  
                Images = new List<CardImage> { new CardImage("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ1BnK6zWwzY7eLzZTwMfZptoadAYiXV89gJ688MHjuK4bZbg9JrQ") },
                // list of buttons   
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Learn More", value: "https://www.dentalcare.com/en-us/patient-education/patient-materials/why-are-regular-dental-visits-important"), new CardAction(ActionTypes.PostBack, "Book now", value: "Dental_booking") }
            };
            Attachment plattachment2 = heroCard2.ToAttachment();
            replyMessage.Attachments.Add(plattachment2);

            var heroCard3 = new HeroCard            //defining new hero card for bone-density checkup camp
            {
                // title of the card  
                Title = "Eye camp",
                //subtitle of the card  
                Subtitle = "Current event",
                //Detail Text  
                Text = "All your eye related problems solved at one place, for free!",
                // list of  Large Image  
                Images = new List<CardImage> { new CardImage("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRpuuLPbsHwsWo6EtWmPkUcaOVgjAk8WcUnH8Kv-IDjFgWjezEn") },
                // list of buttons https://thinkaboutyoureyes.com/articles/eye-exams-and-health/why-are-eye-exams-important  
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Learn More", value: "https://www.dentalcare.com/en-us/patient-education/patient-materials/why-are-regular-dental-visits-important"), new CardAction(ActionTypes.PostBack, "Book now", value: "eyecare") }
            };

            Attachment plattachment3 = heroCard3.ToAttachment(); //define herocard to be attachment
            replyMessage.Attachments.Add(plattachment3);         //attach the attachment to message

            var heroCard4 = new HeroCard            //defining new hero card 
            {
                // title of the card  
                Title = "Diabetes checkup and awareness camp",
                //subtitle of the card  
                Subtitle = "Past event",
                //Detail Text  
                Text = "Don't let diabetes stop you from enjoying your life, come to learn how !",
                // list of  Large Image  
                Images = new List<CardImage> { new CardImage("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcROtDsUzfpYhk87QFqyaPUn7Df6aHd6Lb28LX-2XRMsZxnuHYQnyg") },
                // list of buttons   
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Learn More", value: "https://www.mercola.com/diabetes.aspx") }
            };

            Attachment plattachment4 = heroCard4.ToAttachment(); //define herocard to be attachment
            replyMessage.Attachments.Add(plattachment4);         //attach the attachment to message

            var heroCard5 = new HeroCard            //defining new hero card 
            {
                // title of the card  
                Title = "Pulse Polio camp",
                //subtitle of the card  
                Subtitle = "Past event",
                // navigate to page , while tab on card  
                //Tap = new CardAction(ActionTypes.MessageBack="messageBack", "Book Appointment", value: "http://www.devenvexe.com"),
                //Detail Text  
                Text = "Let's make our country polio-free!",
                // list of  Large Image  
                Images = new List<CardImage> { new CardImage("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSVHzIL7h6Obj6bo03LBfIW41G5eyhsCI0nBuIbOQBYSCGmXTwuaQ") },
                // list of buttons   
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Learn More", value: "https://www.nhp.gov.in/pulse-polio-programme_pg") }
            };

            Attachment plattachment5 = heroCard5.ToAttachment(); //define herocard to be attachment
            replyMessage.Attachments.Add(plattachment5);         //attach the attachment to message

            //var heroCard6 = new HeroCard            //defining new hero card 
            //{
            //    // title of the card  
            //    Title = "Cancer checkup camp",
            //    //subtitle of the card  
            //    Subtitle = "Past event",
            //    //Detail Text  
            //    Text = "Beat cancer, before it beats you!",
            //    // list of  Large Image  
            //    Images = new List<CardImage> { new CardImage("") },
            //    // list of buttons   
            //    Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Learn More", value: "https://www.cancer.gov/about-cancer/understanding/what-is-cancer") }
            //};

            //Attachment plattachment6 = heroCard6.ToAttachment(); //define herocard to be attachment
            //replyMessage.Attachments.Add(plattachment6);         //attach the attachment to message


            await context.PostAsync(replyMessage);               //post message (with hero cards as attachments) to user   
            context.Done<bool>(true);
        }

    }
}