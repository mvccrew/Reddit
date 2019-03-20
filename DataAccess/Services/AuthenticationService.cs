using DataAccess.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class AuthenticationService
    {
        public User LoggedUser { get; set; }

        public void AuthenticateUser(string aUsername, string aPassword)
        {
            UsersRepository repo = new UsersRepository();

            LoggedUser = repo.GetAll(u => u.Username == aUsername && u.Password == aPassword).FirstOrDefault();
        }
    }
}
