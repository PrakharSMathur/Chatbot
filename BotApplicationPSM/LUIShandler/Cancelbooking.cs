using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace BotApplicationPSM.Dialogs
{
    internal class Cancelbooking :IDialog<object> 
    {
        public async Task StartAsync(IDialogContext context)
        {
           DBconnection obj = new DBconnection();
           var empl = obj.GetBooking();

           await context.PostAsync("Name:" + empl.pname.ToUpper()+ " whose age is " + empl.p_age+ " and blood group is " + empl.bgroup+ " and phone number : " + empl.pphone+ " has appointment on : " + empl.Appdate);
           await context.PostAsync("This appointment was cancelled.");
           DBconnection obj1 = new DBconnection();
           obj1.deleteBooking(empl.BookingId);
           context.Done<object>(null);
        }
    }

    
}