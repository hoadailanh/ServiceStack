using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users
{
    class Program
    {
        static IDocumentStore ravenDb = new DocumentStore
        {
            Urls = new string[] { "http://127.0.0.1:8080" },
            Database = "Users"
        }.Initialize();
        static void Main(string[] args)
        {
            using (var session = ravenDb.OpenSession())
            {
                

                //var user1 = new User { Name = "Amanda" };
                //session.Store(user1);
                //var user2 = new User { Name = "Hayden" };
                //session.Store(user2);

                //var userIds = new List<String>();
                //userIds.Add(user1.Id);
                //userIds.Add(user2.Id);

                //var superUser = new User { Name = "Super Hoa", UserIds = userIds };
                //session.Store(superUser);

                //session.SaveChanges();

                var user3 = new User { Name = "Huong" };
                session.Store(user3);

                var superDB = session.Load<User>("users/195-A");
                superDB.UserIds.Add(user3.Id);

                session.SaveChanges();
                
                foreach(string userId in superDB.UserIds)
                {
                    User u = session.Load<User>(userId);
                }
            }
        }
    }
}
