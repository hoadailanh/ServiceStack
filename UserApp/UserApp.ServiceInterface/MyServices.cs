using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack;
using ServiceStack.Templates;
using ServiceStack.DataAnnotations;
using UserApp.ServiceModel;
using Raven.Client;
using UserApi.ServiceInterface;

namespace UserApp.ServiceInterface
{
    [Exclude(Feature.Metadata)]
    [FallbackRoute("/{PathInfo*}", Matches = "AcceptsHtml")]
    public class FallbackForClientRoutes
    {
        public string PathInfo { get; set; }
    }

    public class MyServices : Service
    {
        //Return index.html for unmatched requests so routing is handled on client
        public object Any(FallbackForClientRoutes request)
        {
            IDocumentStore store = DocumentStoreHolder.Store;
            return new PageResult(Request.GetPage("/"));
        }
        [Authenticate]
        public object Get(GetUsersRequest request)
        {
            List<User> users = new List<User>();
            var session = this.GetSession();
            IDocumentStore store = DocumentStoreHolder.Store;
            using (var storeSession = store.OpenSession())
            {
                var parentUser = storeSession.Load<User>(session.UserAuthId);
                if (parentUser.UserIds != null)
                {
                    foreach (string userId in parentUser.UserIds)
                    {
                        users.Add(storeSession.Load<User>(userId));
                    }
                }
                var logItem = new LogItem { User = parentUser, TimeStamp = DateTime.UtcNow,
                    Url = base.Request.AbsoluteUri, IpAddress = base.Request.RemoteIp, Action = "Get User List" };
                storeSession.Store(logItem);
                storeSession.SaveChanges();

                return new UserListResponse { Result = users };
            }
        }

        // Return a single user given their userID
        [Authenticate]
        public object Get(GetUserRequest request)
        {
            IDocumentStore store = DocumentStoreHolder.Store;
            using (var storeSession = store.OpenSession())
            {
                return new UserResponse { Result = storeSession.Load<User>(request.UserId) };
            }
        }

        [Authenticate]
        public void Delete(DeleteUserRequest request)
        {
            IDocumentStore store = DocumentStoreHolder.Store;
            using (var storeSession = store.OpenSession())
            {
                var session = this.GetSession();
                storeSession.Delete(request.UserId);

                var parentUser = storeSession.Load<User>(session.UserAuthId);
                if (parentUser.UserIds != null)
                    parentUser.UserIds.Remove(request.UserId);

                var logItem = new LogItem
                {
                    User = parentUser,
                    OldValue = request.ToJson(),
                    TimeStamp = DateTime.UtcNow,
                    Url = base.Request.AbsoluteUri,
                    IpAddress = base.Request.RemoteIp,
                    Action = "Delete User"
                };
                storeSession.Store(logItem);
                storeSession.SaveChanges();
            }
        }

        // Creates a new user
        [Authenticate]
        public object Post(User request)
        {
            var session = this.GetSession();
            IDocumentStore store = DocumentStoreHolder.Store;
            using (var storeSession = store.OpenSession())
            {
                storeSession.Store(request);
                var parentUser = storeSession.Load<User>(session.UserAuthId);
                if (parentUser.UserIds == null)
                    parentUser.UserIds = new List<string>();
                parentUser.UserIds.Add(request.Id);

                var logItem = new LogItem
                {
                    User = parentUser,
                    NewValue = request.ToJson(),
                    TimeStamp = DateTime.UtcNow,
                    Url = base.Request.AbsoluteUri,
                    IpAddress = base.Request.RemoteIp,
                    Action = "Create User"
                };
                storeSession.Store(logItem);
                storeSession.SaveChanges();

                return new UserResponse { Result = request };
            }
        }

        // Updates an existing user
        [Authenticate]
        public object Put(User request)
        {
            var session = this.GetSession();
            IDocumentStore store = DocumentStoreHolder.Store;
            using (var storeSession = store.OpenSession())
            {
                storeSession.Store(request);
               
                var parentUser = storeSession.Load<User>(session.UserAuthId);
                var logItem = new LogItem
                {
                    User = parentUser,
                    NewValue = request.ToJson(),
                    TimeStamp = DateTime.UtcNow,
                    Url = base.Request.AbsoluteUri,
                    IpAddress = base.Request.RemoteIp,
                    Action = "Update User"
                };
                storeSession.Store(logItem);
                storeSession.SaveChanges();
                return new UserResponse { Result = request };
            }
        }

    }
}
