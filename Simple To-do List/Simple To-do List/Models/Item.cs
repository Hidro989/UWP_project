using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_To_do_List.Models
{
    public class Item
    {
        public int Id_Item { get; set; }
        public string Title_Item { get; set; }
        public string Code_List { get; set; }

        public string Description_Item { get; set; }

        public bool Is_Finished { get; set; }

    }

    public class Item_Extened : Item
    {
        public string Priority_Item { get; set; }

        public string Date_Time_Item { get; set; }
        public string Time_Item { get; set; }
    }
}
