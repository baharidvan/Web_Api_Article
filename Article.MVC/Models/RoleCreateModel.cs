using System.ComponentModel.DataAnnotations;

namespace Article.MVC.Models
{
    public class RoleCreateModel
    {
        [Required(ErrorMessage ="Name is required!")]
        public string Name { get; set; }
    }
}
