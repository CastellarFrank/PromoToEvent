using System.ComponentModel.DataAnnotations;
using System.Web;


namespace PromoToEvents.Models
{
    
    public class DisplayEventoModel
    {
        public int IdEvento { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Owner")]
        public string IdAfiliado { get; set; }

        [Display (Name = "Category Name")]
        public string Category { get; set; }
        
        [Display (Name = "Start date")]
        public string StartDate { get; set; }

        [Display (Name = "End Date")]
        public string FinishDate { get; set; }

        [Display (Name = "Event Status")]
        public bool Active { get; set; }

        public string ActiveLabel { get; set; }

        [Display (Name = "Event Picture")]
        public string ImgPath { get; set; }

        [Display (Name = "Address")]
        public string Address { get; set; }

        [Display (Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Score Average")]
        public decimal ScoreAverage { get; set; }     

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

    }

    public class EditEventoModel
    {
        public int IdEvento { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The number of characters {0} must be at least {2}.", MinimumLength = 4)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int Category { get; set; }

        [Required]
        [Display (Name = "Start Date")]
        public string StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public string EndDate { get; set; }

        [Required]
        [Display(Name = "Active")]
        public string Active { get; set; }

        [Display(Name = "Profile Picture")]
        public HttpPostedFileBase PictureFile {get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "The number of characters {0} must be at least {2}.", MinimumLength = 10)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "The number of characters {0} must be at least {2}.", MinimumLength = 10)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Country")]
        public int Country { get; set; }

        [Required]
        [Display(Name = "State")]
        public int State { get; set; }

        [Required]
        [Display(Name = "City")]
        public int City { get; set; }
    }

    public class RegisterEventoModel
    {
        [Required]
        [StringLength(200, ErrorMessage = "The number of characters {0} must be at least {2}.", MinimumLength = 4)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int Category { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public string StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public string EndDate { get; set; }

        [Required]
        [Display(Name = "Active")]
        public string Active { get; set; }

        [Required]
        [Display(Name = "Profile Picture")]
        public HttpPostedFileBase PictureFile { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "The number of characters {0} must be at least {2}.", MinimumLength = 10)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "The number of characters {0} must be at least {2}.", MinimumLength = 10)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Country")]
        public int Country { get; set; }

        [Required]
        [Display(Name = "State")]
        public int State { get; set; }

        [Required]
        [Display(Name = "City")]
        public int City { get; set; }

    }


}