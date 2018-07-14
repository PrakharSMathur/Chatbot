using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Linq;
using System.Collections.Generic;
using BotApplicationPSM.Dialogs;
using Microsoft.Bot.Builder.FormFlow;

namespace BotApplicationPSM
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        /*internal static IDialog<object> Makeroot()
        {
            return Chain.From(() => new RootDialog());
        }*/
        internal static IDialog<BookingClass> MakeRootDialog()
        {
            return Chain.From(() => FormDialog.FromForm(BookingClass.BuildForm));
        }
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.GetActivityType() == ActivityTypes.Message)
            {
               // await Conversation.SendAsync(activity, Makeroot);
                await Conversation.SendAsync(activity, () => new RootDialog());
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            string messageType = message.GetActivityType();
            if (messageType == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (messageType == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
                IConversationUpdateActivity update = message;
                var client = new ConnectorClient(new Uri(message.ServiceUrl), new MicrosoftAppCredentials());
                if (update.MembersAdded != null && update.MembersAdded.Any())
                {
                    foreach (var newMember in update.MembersAdded)
                    {
                        if (newMember.Id != message.Recipient.Id)
                        {
                            var reply = message.CreateReply();
                            reply.Text = $"Welcome to Medeventz"/*,please press any key to continue*/;
                            
                            /*reply.Attachments = new List<Attachment>();      //define attachments to the message
                            var heroCard1 = new HeroCard                            //defining new hero card
                            {
                                // title of the card  
                                Title = "Blood donation",
                                //subtitle of the card  
                                Subtitle = "Current event",
                                // navigate to page , while tab on card  
                                //Tap = new CardAction(ActionTypes.MessageBack="messageBack", "Book Appointment", value: "http://www.devenvexe.com"),
                                //Detail Text  
                                Text = "Raqt-daan " + "\n" + " jivan daan. Each drop matters!",
                                // list of  Large Image  
                                Images = new List<CardImage> { new CardImage(@"C:\Users\demouser2\Downloads\Blood_donation.jpg") },
                                // list of buttons   
                                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Learn More", value: "https://www.organicfacts.net/health-benefits/other/blood-donation.html"), new CardAction(ActionTypes.OpenUrl, "Book now", value: "https://en.wikipedia.org/wiki/5_Gorkha_Rifles_(Frontier_Force)") }
                            };

                            Attachment plAttachment1 = heroCard1.ToAttachment();
                            reply.Attachments.Add(plAttachment1);*/


                            client.Conversations.ReplyToActivityAsync(reply);


                        }
                    }
                }
            }
            else if (messageType == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (messageType == ActivityTypes.Typing)
            {
                // Handle knowing that the user is typing
            }
            else if (messageType == ActivityTypes.Ping)
            {
                var reply = message.CreateReply();
                reply.Type = "Ping";
                return reply;
            }

            return null;
        }
    }
}