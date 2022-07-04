using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard1.Messages
{
    public class SubChangeMessage
    {
        public bool check { get; set; }
        public SubChangeMessage(bool check)
        {
            this.check = check;
        }
    }
}
