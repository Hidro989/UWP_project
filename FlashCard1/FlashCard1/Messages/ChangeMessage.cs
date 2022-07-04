using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard1.Messages
{
    public class ChangeMessage
    {
        public bool check { get; set; }

        public ChangeMessage(bool check)
        {
            this.check = check;
        }
    }
}
