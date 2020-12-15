using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Mütləqdir")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Mütləqdir")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Mütləqdir")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Düzgün olmayan email formatı")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mütləqdir")]
        [RegularExpression("[(][0-9]{3}[)][0-9]{3}-[0-9]{2}-[0-9]{2}", ErrorMessage = "Nümunə:050-000-00-00")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Mütləqdir")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage ="Minimum uzunluq 6")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifrə eyni deyil")]
        [Required(ErrorMessage = "Mütləqdir")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Mütləqdir")]
        
        public string SeriaNumber { get; set; }
        [Required(ErrorMessage = "Mütləqdir")]
        public string CitizenShip { get; set; }
        [Required(ErrorMessage = "Mütləqdir")]
        public string DayOfBirth { get; set; }
        [Required(ErrorMessage = "Mütləqdir")]
        public string MonthOfBirth { get; set; }
        [Required(ErrorMessage = "Mütləqdir")]
        public string YearOfBirth { get; set; }
        [Required(ErrorMessage = "Mütləqdir")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Mütləqdir")]
        public string IdType { get; set; }
        [Required(ErrorMessage = "Mütləqdir")]
        [MinLength(7,ErrorMessage ="Minimum uzunluq 7")]
        [MaxLength(7, ErrorMessage = "Maksimum uzunluq 7")]
        public string FinCode { get; set; }
        [Required(ErrorMessage = "Mütləqdir")]
        public string IdAddress { get; set; }
        [Required]
        public bool isAgree { get; set; }
    }
}
