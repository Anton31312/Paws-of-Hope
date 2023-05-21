using Paws_of_Hope.EF;
using System.ServiceModel;

namespace WCF_Paws_of_Hope
{
    public class ServerUser
    {
        public int ID { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public OperationContext operationContext { get; set; }
    }
}
