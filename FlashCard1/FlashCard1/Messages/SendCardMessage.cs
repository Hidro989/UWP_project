using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwp.Model.Model;

namespace FlashCard1.Messages
{
    public class SendCardMessage: RequestMessage<Card>
    {
    }
}
