using System.ServiceModel;

namespace wcf_chat
{
    [ServiceContract()]
    public interface IServiceTutor
    {
        [OperationContract]
        int Connect(string login, string password);

        [OperationContract]
        void Disconnect(int id);
    }

}
