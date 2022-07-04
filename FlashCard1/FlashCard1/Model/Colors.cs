using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard1.Model
{
    public class Colors
    {
        public ObservableCollection<Color> ListColor { get; set; }
        public Colors()
        {
            ListColor = new ObservableCollection<Color>
            {
                new Color
                {
                    Name = "Red",
                    Code = "#E1432E"
                },
                new Color
                {
                    Name = "Fresh Purple",
                    Code = "#D260E1"
                },
                new Color
                {
                    Name = "Baby Blue",
                    Code = "#89CFF0"
                },
                new Color
                {
                    Name = "Cyan",
                    Code = "#0EE3D8"
                },
                new Color
                {
                    Name = "Green Lantern",
                    Code = "#49A84C"
                },
            };
        }
    }

    public class Color
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
