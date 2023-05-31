using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_ServiceAuth
{
    // ПРИМЕЧАНИЕ. Можно использовать команду "Переименовать" в меню "Рефакторинг", чтобы изменить имя интерфейса "IServiceAuth" в коде и файле конфигурации.
    [ServiceContract]
    public interface IServiceAuth
    {
        [OperationContract]
        int Connect(int ID, string login, string password);

        [OperationContract]
        void Disconnect(int id);
    }
}
