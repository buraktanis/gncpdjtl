using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Core.EntitiesBase;

namespace Entities.DTOs
{
    public class UserForLoginDto:IDto
    {
        [Required(ErrorMessage = "required")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "emailinvalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "required")]
        public string Password { get; set; }
        public bool IsPersistent { get; set; }
    }
}
