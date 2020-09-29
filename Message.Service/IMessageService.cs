using System.Collections.Generic;

namespace Message.Service
{
    public interface IMessageService
    {
        void Send(string message);
        List<string> GetList();
    }
}
