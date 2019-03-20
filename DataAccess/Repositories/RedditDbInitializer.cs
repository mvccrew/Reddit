using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class RedditDbInitializer : DropCreateDatabaseIfModelChanges<RedditDb>
    {
        protected override void Seed(RedditDb context)
        {
            IList<User> defaultUsers = new List<User>();

            defaultUsers.Add(new User() { Username = "antonio", Password = "antoniopass", FirstName = "Antonio", LastName = "Boyadzhiev", IsAdmin = true });
            defaultUsers.Add(new User() { Username = "daniel", Password = "danielpass", FirstName = "Daniel", LastName = "Ivanov", IsAdmin = true });
            defaultUsers.Add(new User() { Username = "angel", Password = "angelpass", FirstName = "Angel", LastName = "Krastev", IsAdmin = true });
            defaultUsers.Add(new User() { Username = "ivelin", Password = "ivelinpass", FirstName = "Ivelin", LastName = "Jilov", IsAdmin = true });

            context.Users.AddRange(defaultUsers);

            base.Seed(context);
        }
    }
}
