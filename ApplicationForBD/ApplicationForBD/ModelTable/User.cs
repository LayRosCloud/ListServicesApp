using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForBD.ApplicationDataBases
{
    internal class User
    {
        private int iduser;
        private string loginuser;
        private string passworduser;
        private string emailuser;
        private int roleid;

        public int id
        {
            get { return iduser; }
            set { iduser = value; }
        }
        public string login_user
        {
            get { return loginuser; }
            set { loginuser = value; }
        }
        public string password_user
        {
            get { return passworduser; }
            set { passworduser = value; }
        }
        public string email_user
        {
            get { return emailuser; }
            set { emailuser = value; }
        }
        public int role_id
        {
            get { return roleid; }
            set { roleid = value; }
        }

        public User()
        {
        }
        public User(int id, string login, string password, string email):this(id, login, password, email, 2)
        {
        }
        public User(int id, string login, string password, string email, int idRole)
        {
            this.id = id;
            login_user = login;
            password_user = password;
            email_user = email;
            role_id = idRole;
        }
        public override string ToString()
            => $"{id} | {login_user} | {password_user} | {email_user} | {role_id}";
    }
}
