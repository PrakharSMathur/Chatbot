
using System;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Dialogs;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
//using System.Windows.Forms;



namespace BotApplicationPSM.Dialogs
{
    [Serializable]
    public class BookingClass
    {
        public enum bloodgp
        {
            //    /*[Terms("A+", "B+", "O+","AB+","A-","B-","AB-","O-")]*/
           None, A_Positive, B_Positive,O_Positive, AB_Positive, A_Negative, B_Negative, O_Negative, AB_Negative
            //a, b, c, d, f, e, g, h,
            //    Enum
        };
        [Prompt(" Please enter your name{||}")]
        public string Name;
       // [Prompt(" Please re-enter your age{||}")]
        //[Numeric(10, 75)]
       // public int age;
        [Prompt(" Please enter your blood group{||}")]
        //public IEnumerable<string> cardiacsurgeons = new[] { "Dr.Sam", "Dr.Pam" };
        //public IEnumerable<string> bloodgp = new[] { "Enum" };
        public bloodgp BloodGroup;
        //public List<bloodgp> Bloodgroup;
        [Prompt(" Please enter your phone number{||}")]
        [Numeric(1000000000, 9999999999)]
        public double Phone_no;
        [Prompt("Please select a day {||}")]
        public DayOfWeek Appointment_Day;

        //public async  Task StartAsync(IDialogContext context)
        //{
        //    PromptDialog.Choice(context, done, bloodgp, "Please select your date", "wrong input", 1, PromptStyle.Auto);
        //}
        public static IForm<BookingClass> BuildForm()
        {
            {
                return new FormBuilder<BookingClass>()
                    .Message("Welcome to the reservation ")
                     .OnCompletion(async (context, profileForm) =>
                     {
                         context.PrivateConversationData.SetValue<bool>(
                   "ProfileComplete", true);
                         context.PrivateConversationData.SetValue<string>(
                             "pname", profileForm.Name);
                        // context.PrivateConversationData.SetValue<int>(
                          //   "p_age", profileForm.age);
                         context.PrivateConversationData.SetValue<string>(
                             "bgroup",profileForm.BloodGroup.ToString());
                         context.PrivateConversationData.SetValue<string>(
                             "pphone", profileForm.Phone_no.ToString());
                         context.PrivateConversationData.SetValue<string>(
                                                      "Appdate", profileForm.Appointment_Day.ToString());

                         //var pname = context.PrivateConversationData.GetValueOrDefault("Name", string.Empty);
                         //int p_age1 = context.PrivateConversationData.GetValueOrDefault("age", 0);
                         //var bgroup = context.PrivateConversationData.GetValueOrDefault("Bloodgroup", string.Empty);
                         //var pphone = context.PrivateConversationData.GetValueOrDefault("Phone", string.Empty);
                         //var Appdate = context.PrivateConversationData.GetValueOrDefault("Monday", string.Empty);
                         // Tell the user that the form is complete  
                         await context.PostAsync("Thanks for booking.");
                         
                     })
                    .Build();
            }
        }

        //private async Task done(IDialogContext context, IAwaitable<object> result)
        //{
        //    var res = await result;
           
        //    int temp;
        //    string name;
        //    string bgp;
        //    string pno;
        //    string newdate;
        //    //context.PrivateConversationData.SetValue("Name", "Test1");
        //   // context.PrivateConversationData.SetValue("Time", res.ToString());

        //    context.PrivateConversationData.TryGetValue("pname",out name);
        //    context.PrivateConversationData.TryGetValue("p_age", out temp);
        //    context.PrivateConversationData.TryGetValue("bgroup",out bgp);
        //    context.PrivateConversationData.TryGetValue("pno", out pno);
        //    context.PrivateConversationData.TryGetValue("Appdate", out newdate);
           
        //    Class1 obj1 = new Class1();
        //    obj1.pname = name;
        //    obj1.p_age = temp;
        //    obj1.bgroup = bgp;
        //    obj1.pphone = pno;
        //    obj1.Appdate = newdate;
        //    DBconnection obj = new DBconnection();
        //    obj.BookingAppt(obj1);

        //    await context.PostAsync("Your appointment has been booked.");
        //    context.Done<object>(null);
        //}

        ////public async Task StartAsync(IDialogContext context)
        //{
        //    throw new NotImplementedException();
        //}
        //public void AddEmployee(Employee employee)
        //{
        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("spAddEmployee", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@Name", employee.Name);
        //        cmd.Parameters.AddWithValue("@Gender", employee.Gender);
        //        cmd.Parameters.AddWithValue("@Department", employee.Department);
        //        cmd.Parameters.AddWithValue("@City", employee.City);
        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //}
    }
        
    }
    

