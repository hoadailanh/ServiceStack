using Raven.Client;
using Raven.Client.Document;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.ServiceModel;

namespace UserApi.ServiceInterface
{
    public class DocumentStoreHolder
    {
        private static Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(CreateStore);

        public static IDocumentStore Store => store.Value;

        private static IDocumentStore CreateStore()
        {
            IDocumentStore store = new DocumentStore()
            {
                Url = ConfigurationManager.AppSettings["dburl"],
                DefaultDatabase = ConfigurationManager.AppSettings["dbname"]
            }.Initialize();

            using (var session = store.OpenSession())
            {
                List<User> users = session.Query<User>().Where(x => x.Email == "admin@localhost.com" && x.Password == "Admin").ToList();
                if(users.Count == 0)
                {
                    var superUser = new User { Name = "Admin", Email = "admin@localhost.com", Password = "Admin", Status = UserStatusConstants.ActiveStatus };
                    session.Store(superUser);

                    var logItem = new LogItem { User = superUser, TimeStamp = DateTime.UtcNow, Action = "Create Admin User", NewValue = superUser.ToJson() };
                    session.Store(logItem);
                    session.SaveChanges();
                }
            }
            return store;
        }
    }
}
