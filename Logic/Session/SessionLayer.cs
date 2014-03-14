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
        private readonly string _userIdIdentifier;


        private SessionLayer()
        {
            _userEmailIdentifier = "loggedUserEmail";
            _userRoleIdentifier = "loggedUserRole";
            _userDisplayNameIdentifier = "loggedUserName";
            _userIdIdentifier = "loggedId";

        }

        public static SessionLayer Instance
        {
            get { return _instance ?? (_instance = new SessionLayer()); }
        }

        public bool LogIn(string userName, string password, bool remember = false)
        {
            var afiliado = ValidateUser(userName, password);
            if (afiliado == null) return false;

            UpdateSessionFromUser(afiliado);

            FormsAuthentication.RedirectFromLoginPage(userName, remember);

            return true;
        }

        public void LogOut(bool redirect = false)
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Remove(_userEmailIdentifier);
            HttpContext.Current.Session.Remove(_userRoleIdentifier);
            HttpContext.Current.Session.Remove(_userDisplayNameIdentifier);
            HttpContext.Current.Session.Remove(_userIdIdentifier);

            if(redirect) FormsAuthentication.RedirectToLoginPage();

        }

        public void CheckSession()
        {
            if(!HttpContext.Current.User.Identity.IsAuthenticated)
                FormsAuthentication.RedirectToLoginPage();

            if (HttpContext.Current.Session[_userIdIdentifier] != null) return;

            var id = int.Parse(HttpContext.Current.User.Identity.Name);
            var user = UserRepo.GetById(id);
            UpdateSessionFromUser(user);
        }

        public void UpdateSessionFromUser(Afiliado user)
        {
            HttpContext.Current.Session[_userEmailIdentifier] = user.emailAfiliado;
            HttpContext.Current.Session[_userRoleIdentifier] = UserRepo.UserTypeLabel(user.raizVal);
            HttpContext.Current.Session[_userDisplayNameIdentifier] = user.nombreAfiliado;
            HttpContext.Current.Session[_userIdIdentifier] = user.idAfiliado;
            
        }

        public string GetUserLoggedEmail()
        {
            CheckSession();
            var userName = HttpContext.Current.Session[_userEmailIdentifier];
            return userName != null ? userName.ToString() : "";
        }

        public string GetUserLoggedRole()
        {
            CheckSession();
            var userRole = HttpContext.Current.Session[_userRoleIdentifier];
            return userRole != null ? userRole.ToString() : "";
        }

        public string GetUserLoggedName()
        {
            CheckSession();
            var userDisplay = HttpContext.Current.Session[_userDisplayNameIdentifier];
            return userDisplay != null ? userDisplay.ToString() : "";
        }

        public int GetUserLoggedId()
        {
            CheckSession();
            var userId = HttpContext.Current.Session[_userIdIdentifier];
            return userId != null ? int.Parse(userId.ToString()) : 0;
        }

        private Afiliado ValidateUser(string userName, string password)
        {    
            var myUsers = UserRepo.Filter(x => x.emailAfiliado.Equals(userName) && 
                x.passwordAfiliado.Equals(password) && x.statusAfiliado);
            if (myUsers != null && myUsers.Count() == 1)
                return myUsers.First();
            return null;
        }
    }
}
