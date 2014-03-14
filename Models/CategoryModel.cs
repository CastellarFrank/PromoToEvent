using System.ComponentModel.DataAnnotations;
using System.Web;


namespace PromoToEvents.Models
{
   

    public class DisplayCategoriaModel
    {
        public int IdCategoria { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Category Image")]
        public string ImgPath { get; set; }

        [Display(Name = "Active Category")]
        public bool Status { get; set; }

        public string LabelStatus { get; set; }

    }

    public class EditCategoriaModel
    {
        public int IdCategoria { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The number of characters {0} must be at least {2}.", MinimumLength = 4)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Category Image")]
        public string ImgPath { get; set; }
        
        public HttpPostedFileBase PictureFile {get; set; }

        [Display(Name = "Active Category")]
        public string Active { get; set; }
    }

    public class RegisterCategoriaModel
    {
        [Required]
        [StringLength(200, ErrorMessage = "The number of characters {0} must be at least {2}.", MinimumLength = 4)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Category Image")]
        public HttpPostedFileBase PictureFile { get; set; }

        [Required]
        [Display(Name = "Active Category")]
        public string Active { get; set; }
    }

    public class ApiCategoryInfo
    {
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
    }


}