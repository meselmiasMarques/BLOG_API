using Blog.Models;
using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class EditorCategoryViewModel
    {
        [Required(ErrorMessage ="O Campo {0} é requerido")]
        [StringLength(40, MinimumLength =3, ErrorMessage = "Este campo deve conter entre 3 e 40 Caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O Campo {0} é requerido")]
        public string Slug { get; set; }
    }
}
