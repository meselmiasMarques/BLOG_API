using Blog.Models;
using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class EditorUserViewModel
    {

        [Required(ErrorMessage = "O Campo Nome é requerido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O Campo Email é requerido")]

        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Image { get; set; }

        [Required(ErrorMessage = "O Campo Slug é requerido")]
        public string Slug { get; set; }
        public string Bio { get; set; }

    }
}
