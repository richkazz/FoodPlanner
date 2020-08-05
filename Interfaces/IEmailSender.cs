using FoodPlanner.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Interfaces
{
    public interface IEmailSender
    {
       public void SendEmail(Message message);
    }
}
