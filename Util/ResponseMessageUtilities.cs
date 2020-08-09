using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Util
{
    public class ResponseMessageUtilities
    {
        //Account Messages
        public const string ACCOUNT_LOCKED_OUT = "Your account is locked out. Kindly wait for 10 minutes and try again";
        public const string INVALID_LOGIN = "Login Failed: Invalid Email or password";
        public const string Invalid_Login_Attempt = "Invalid Login Attempt";
        public const string INVALID_EMAIL = "Check your Email and try again";
        public const string FORGOT_PASSWORD_CONFIRMTION = "The email has been sent. Please check your email to reset your password.";

        //Creating error massage
        public const string ITEM_EXIST = "The item already exist ";
        public const string CREATED_SUCESSFUL = "The item as been created sucessfully ";
        public const string DELETED_SUCESSFUL = "The item as been deleted sucessfully ";
        
        public const string UPDATE_SUCESSFUL = "The item as been updated sucessfully ";



    }
}
