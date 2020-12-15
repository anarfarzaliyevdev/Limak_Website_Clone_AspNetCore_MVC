using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Mütləqdir")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Düzgün olmayan email formatı.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mütləqdir")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }

    }
}
