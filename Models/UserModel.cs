using System.ComponentModel.DataAnnotations;
using System.Web;


namespace PromoToEvents.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Email")]
        public string UserEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "¿Keep me signed in?")]
        public bool RememberMe { get; set; }

    }

    public class DisplayAfiliadoModel
    {
        public int IdAfiliado { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display (Name = "Profile Picture")]
        public string ImgPath { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Active Account")]
        public bool Active{ get; set; }

        public string ActiveLabel { get; set; }

        [Display(Name = "Created Date")]
        public string CreatedDate { get; set; }

        [Display(Name = "Modified Date")]
        public string ModifyDate { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "User Type")]
        public string UserType { get; set; }
    }

    public class EditAfiliadoModel
    {
        public int IdAfiliado { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The number of characters {0} must be at least {2}.", MinimumLength = 4)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "The number of characters {0} must be at least {2}.", MinimumLength = 10)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Profile Picture")]
        public HttpPostedFileBase PictureFile {get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "The number of characters {0} must be at least {2}.", MinimumLength = 5)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The number of characters {0} must be at least {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm")]
        [Compare("Password", ErrorMessage = "The confirm password and password must match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Active Account")]
        public string Active { get; set; }

        [Required]
        [Display(Name = "Country")]
        public int Country { get; set; }

        [Required]
        [Display(Name = "State")]
        public int State { get; set; }

        [Required]
        [Display(Name = "City")]
        public int City { get; set; }

        [Required]
        [Display(Name = "User Type")]
        public string UserType { get; set; }
    }

    public class RegisterAfiliadoModel
    {
        [Required]
        [StringLength(200, ErrorMessage = "The number of characters {0} must be at least {2}.", MinimumLength = 4)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "The number of characters {0} must be at least {2}.", MinimumLength = 10)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Profile Picture")]
        public HttpPostedFileBase PictureFile { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "The number of characters {0} must be at least {2}.", MinimumLength = 5)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The number of characters {0} must be at least {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm")]
        [Compare("Password", ErrorMessage = "The confirm password and password must match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Active Account")]
        public string Active { get; set; }

        [Required]
        [Display(Name = "Country")]
        public int Country { get; set; }

        [Required]
        [Display(Name = "State")]
        public int State { get; set; }

        [Required]
        [Display(Name = "City")]
        public int City { get; set; }

        [Required]
        [Display(Name = "User Type")]
        public string UserType { get; set; }
    }


}