using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace WCF_ServiceAuth
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceAuth : IServiceAuth
    {
        List<ServerUser> users = new List<ServerUser>();
        public int Connect(int ID, string login, string password)
        {
            ServerUser user = new ServerUser()
            {
                ID = ID,
                Login = login,
                Password = password,
                operationContext = OperationContext.Current
            };

            users.Add(user);
            return user.ID;
        }

        public void Disconnect(int id)
        {
           var user = users.FirstOrDefault(i => i.ID == id);
           if (user != null)
           {
                users.Remove(user);
           }
        }

    }
}
