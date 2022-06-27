using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForBD.ModelTable
{
    internal class TablesLang
    {
 
        public string NameTable { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime RedactorTime { get; set; }

        public TablesLang(string name, DateTime create, DateTime redactor)
        {
            NameTable = name;
            CreateTime = create;
            RedactorTime = redactor;
        }
        public override string ToString()
        {
            return $"{NameTable}|{CreateTime}|{RedactorTime}";
        }
    }
}
