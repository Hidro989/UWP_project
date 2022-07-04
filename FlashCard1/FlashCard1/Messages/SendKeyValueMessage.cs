using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard1.Messages
{
    public class SendKeyValueMessage : RequestMessage<IDictionary<string, int>>
    {
    }
}
