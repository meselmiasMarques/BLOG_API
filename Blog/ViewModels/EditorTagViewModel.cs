using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Blog.ViewModels
{
    public class EditorTagViewModel
    {
        [Required(ErrorMessage ="O campo Nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage ="O campo slug é obrigatório")]
        public string Slug { get; set; }
    }
}