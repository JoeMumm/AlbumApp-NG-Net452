using AlbumApp.Business.Entities;
using AlbumApp.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;

namespace AlbumApp.Web
{
  public partial class Startup
  {
      // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
    public void ConfigureAuth(IAppBuilder app)
    {
        // Configure the db context, user manager and signin manager to use a single instance per request
        app.CreatePerOwinContext(AlbumContext.Create);
        app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
        app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

        // Enable the application to use a cookie to store information for the signed in user
        // and to use a cookie to temporarily store information about a user logging in with a third party login provider
        // Configure the sign in cookie
        app.UseCookieAuthentication(new CookieAuthenticationOptions
        {
          AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
          LoginPath = new PathString("/Account/Login"),
          Provider = new CookieAuthenticationProvider
          {
            // Enables the application to validate the security stamp when the user logs in.
            // This is a security feature which is used when you change a password or add an external login to your account.  
            OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                validateInterval: TimeSpan.FromMinutes(30),
                regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager)),

            OnApplyRedirect = ctx => {
              var response = ctx.Response;
              if (!IsApiResponse(ctx.Response))
              { response.Redirect(ctx.RedirectUri); } }
          }
        });
    }

    private static bool IsApiResponse(IOwinResponse response) {
      var responseHeader = response.Headers;
      if (responseHeader == null) return false;
      return (responseHeader["Suppress-Redirect"] == "True"); }

  }
}

