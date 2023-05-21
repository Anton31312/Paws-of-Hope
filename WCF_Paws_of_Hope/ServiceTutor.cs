using Paws_of_Hope.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCF_Paws_of_Hope;

namespace wcf_chat
{
  
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceTutor
    {
        List<ServerUser> users = new List<ServerUser>();

        public int Connect(string login, string password)
        {
            var userAuth = AppDate.Context.Tutor.ToList().
                Where(i => i.Login == login && i.Password == password).FirstOrDefault();
            ServerUser user = new ServerUser() {
                ID = userAuth.IDTutor,
                Login = userAuth.Login,
                Password = userAuth.Password,
                operationContext = OperationContext.Current
            };

            users.Add(user);
            return user.ID;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);
            if (user!=null)
            {
                users.Remove(user);
            }
        }

    }
}
