using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UsersRepository : BaseRepository<User>
    {
        public void ChangeKarma(int userId, int value)
        {
            UsersRepository repo = new UsersRepository();
            User user = repo.GetById(userId);

            user.Karma += value;

            repo.Save(user);
        }
    }
}
