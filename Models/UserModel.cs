using System.ComponentModel.DataAnnotations;


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
}