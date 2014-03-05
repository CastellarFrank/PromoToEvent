using System.Linq;
using System.Web;
using System.Web.Security;
using PromoToEvents.Logic.DataBase;


namespace PromoToEvents.Logic.Session
{
    public class SessionLayer: ISessionManagement
    {
        private static SessionLayer _instance;
        private static readonly AfiliadoRepository UserRepo = AfiliadoRepository.GetInstance;
        private readonly string _userEmailIdentifier;
        private readonly string _userRoleIdentifier;
        private readonly string _userDisplayNameIdentifier;


        private SessionLayer()
        {
            _userEmailIdentifier = "loggedUserEmail";
            _userRoleIdentifier = "loggedUserRole";
            _userDisplayNameIdentifier = "loggedUserName";

        }

        public static SessionLayer Instance
        {
            get { return _instance ?? (_instance = new SessionLayer()); }
        }

        public bool LogIn(string userName, string password, bool remember = false)
        {
            if (!ValidateUser(userName, password)) return false;

            HttpContext.Current.Session[_userEmailIdentifier] = userName;
            HttpContext.Current.Session[_userRoleIdentifier] = GetUserRole(userName);
            HttpContext.Current.Session[_userDisplayNameIdentifier] = GetUserName(userName);

            FormsAuthentication.RedirectFromLoginPage(userName, remember);

            return true;
        }

        public void LogOut(bool redirect = false)
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Remove(_userEmailIdentifier);
            HttpContext.Current.Session.Remove(_userRoleIdentifier);
            HttpContext.Current.Session.Remove(_userDisplayNameIdentifier);

            if(redirect) FormsAuthentication.RedirectToLoginPage();

        }

        public string GetUserLoggedEmail()
        {
            var userName = HttpContext.Current.Session[_userEmailIdentifier];
            return userName != null ? userName.ToString() : "";
        }

        public string GetUserLoggedRole()
        {
            var userRole = HttpContext.Current.Session[_userRoleIdentifier];
            return userRole != null ? userRole.ToString() : "";
        }

        public string GetUserLoggedName()
        {
            var userDisplay = HttpContext.Current.Session[_userDisplayNameIdentifier];
            return userDisplay != null ? userDisplay.ToString() : "";
        }

        private static bool ValidateUser(string userName, string password)
        {    
            var myUsers = UserRepo.Filter(x => x.emailAfiliado.Equals(userName) && 
                x.passwordAfiliado.Equals(password) && x.statusAfiliado);
            return myUsers != null && myUsers.Count() == 1; 
        }

        private static string GetUserRole(string userName)
        {
            var users = UserRepo.Filter(x => x.emailAfiliado.Equals(userName));

            if (users != null && users.Count() != 0)
                return UserRepo.UserTypeLabel(users.First().raizVal);
            return "";
        }

        private static string GetUserName(string userEmail)
        {
            var myUsers = UserRepo.Filter(x => x.emailAfiliado.Equals(userEmail));
            return (myUsers != null && myUsers.Count() == 1) ? myUsers.First().nombreAfiliado : ""; 
        }




    }
}
