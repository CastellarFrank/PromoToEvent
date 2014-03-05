
namespace PromoToEvents.Logic.Session
{
    public interface ISessionManagement
    {
        bool LogIn(string userName, string password, bool remember = false);

        void LogOut(bool redirect = false);

        string GetUserLoggedName();

        string GetUserLoggedRole();

    }
}