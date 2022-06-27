using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForBD.ApplicationDataBases
{
    internal class role
    {
        public int id_role { get; set; }
        public string role_description { get; set; }
        public role(int id_role, string role_description)
        {
            this.id_role = id_role;
            this.role_description = role_description;
        }
    }
}
