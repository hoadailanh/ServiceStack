using Raven.Client;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Web;
using System.Collections.Generic;
using System.Linq;
using UserApi.ServiceInterface;
using UserApp.ServiceModel;

public class CustomCredentialsAuthProvider : CredentialsAuthProvider
{
    public override bool TryAuthenticate(IServiceBase authService,
        string userName, string password)
    {
        IDocumentStore store = DocumentStoreHolder.Store;
        using (var session = store.OpenSession())
        {
            List<User> users = session.Query<User>().Where(x => x.Email == userName && x.Password == password).ToList();
            if(users.Count == 1 && users[0].Status == UserStatusConstants.ActiveStatus)
            {
                authService.GetSession().UserAuthId = users.ElementAt(0).Id;
                return true;
            }
            return false;
        }
    }
    public override IHttpResult OnAuthenticated(IServiceBase authService,
        IAuthSession session, IAuthTokens tokens,
        Dictionary<string, string> authInfo)
    {
        //Fill IAuthSession with data you want to retrieve in the app eg:
        //session.FirstName = session.FirstName;
        //...

        //Call base method to Save Session and fire Auth/Session callbacks:
        return base.OnAuthenticated(authService, session, tokens, authInfo);

        //Alternatively avoid built-in behavior and explicitly save session with
        //authService.SaveSession(session, SessionExpiry);
        //return null;
    }
}