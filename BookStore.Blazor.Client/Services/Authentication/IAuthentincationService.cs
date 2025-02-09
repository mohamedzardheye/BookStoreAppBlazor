using BookStore.Blazor.Client.Services.Base;

namespace BookStore.Blazor.Client.Services.Authentication
{



    public interface IAuthenticationService
    {
      Task<bool> AuthenticateAsync(LoginUserDto loginModel);
  
        Task Logout();
    }


  
}
