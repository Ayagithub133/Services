using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Shared.Data
{
    public class RegisterViewModel
    {
        [Key]
        public string Id { set; get; }

        [StringLength(50)]
        [Required(ErrorMessage ="ادخل الاسم الاول")]
        public string FirstName { set; get; }

        [StringLength(50)]
        [Required(ErrorMessage = "ادخل الاسم الثانى")]
        public string SecondName { set; get; }

        [StringLength(50) ]
        [Required (ErrorMessage = "ادخل الاسم الاخير")]
        public string LastName { set; get; }

        [EmailAddress]
        [Required(ErrorMessage = "البريد الالكترونى مطلوب")]
        [StringLength(256)]
        public string Email { set; get; }

        [Phone]
        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [StringLength(20,MinimumLength = 9, ErrorMessage = "لاتدخل رقم اقل من تسع ارقام")]
        public string Phone { set; get; }

        [Required(ErrorMessage = "كلمه المرور مطلوبة")]
        [MinLength(7,ErrorMessage ="لا يسمح بأقل من 7 ارقام أو رموز أو حروف")]
        public string Password { set; get; }

        [Required(ErrorMessage = "قم بتأكيد كلمة المرور")] 
        [MinLength(7,ErrorMessage ="")]
        [Compare(nameof(Password),ErrorMessage ="غير متطابق مع كلمة المرور")]
        public string ConfirmPassword { set; get; }
    }
}

