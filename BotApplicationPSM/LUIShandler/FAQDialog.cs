using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;

namespace BotApplicationPSM.Dialogs
{
    [Serializable]
    public class FAQDialog : IDialog<object>
    {

        public async  Task StartAsync(IDialogContext context)
        {
            //context.Done<bool>(true);
            context.PostAsync("What is your question?");
            context.Call(new RootDialog(), done);
            
        }

        private async Task done(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<bool>(true);
        }
    }
}