using System.IO;
using Funq;
using UserApp.ServiceInterface;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.Auth;
using ServiceStack.Caching;

namespace UserApp
{
  public class AppHost : AppHostBase
  {
    /// <summary>
    /// Base constructor requires a Name and Assembly where web service implementation is located
    /// </summary>
    public AppHost()
        : base("UserApp", typeof(MyServices).Assembly) { }

    /// <summary>
    /// Application specific configuration
    /// This method should initialize any Plugins or IOC dependencies used by your web services
    /// </summary>
    public override void Configure(Container container)
    {
      SetConfig(new HostConfig
      {
        DebugMode = AppSettings.Get("DebugMode", false),
        WebHostPhysicalPath = MapProjectPath("~/wwwroot"),
        AddRedirectParamsToQueryString = true,
        UseCamelCase = true,
      });

      Plugins.Add(new TemplatePagesFeature());
      this.Plugins.Add(new PostmanFeature());
      this.Plugins.Add(new CorsFeature());
      Plugins.Add(new AuthFeature(() => new AuthUserSession(),
      new IAuthProvider[] {
        //new BasicAuthProvider(), //Sign-in with HTTP Basic Auth
        new CustomCredentialsAuthProvider(), //HTML Form post of UserName/Password credentials
      }));

      Plugins.Add(new RegistrationFeature());

      container.Register<ICacheClient>(new MemoryCacheClient());
      var userRep = new InMemoryAuthRepository();
      container.Register<IAuthRepository>(userRep);

    }
  }
}
