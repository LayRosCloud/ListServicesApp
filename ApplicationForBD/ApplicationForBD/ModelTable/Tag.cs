using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForBD.ApplicationDataBases
{
    internal class Tag
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public Tag(int iD, string title, string color)
        {
            ID = iD;
            Title = title;
            Color = color;
        }
    }
}
