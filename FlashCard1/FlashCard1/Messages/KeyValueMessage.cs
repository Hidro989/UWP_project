using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard1.Messages
{
    public class KeyValueMessage 
    {
        public IDictionary<string, int> Choice { get; set; }
        public KeyValueMessage(IDictionary<string, int> choice)
        {
            Choice = choice;
        }
    }
}
