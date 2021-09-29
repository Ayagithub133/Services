using Services.Shared.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Services.Shared.Validations;

namespace Services.Shared.Data
{
    public class HallViewModel
    {
        
        [Required(ErrorMessage ="ادخل اسم الخدمة")]
        [StringLength(100, MinimumLength = 20 , ErrorMessage = "لا تدخل اقل من 20 حرفا او اكثر من 100 حرف")]
        public string ServiceTitle { set; get; }

        [Required(ErrorMessage = "حدد تاريخ الحجز")]
        [DataType(DataType.Date)]
        [ValidateDate(ErrorMessage ="أدخل تاريخ مناسب")]
        public DateTime BookDate { set; get; }

        [Required(ErrorMessage = "نوع الحجز") ]
        public BookType BookType { set; get; }

        [Required(ErrorMessage ="قم بتحديد قائمة الطعام")]
        public Menue Menue { set; get; }

        [Required(ErrorMessage = "ادخل نوع الحضور")]
        public GuestGender GuestGender { set; get; }

        [Required(ErrorMessage ="اختر حجم القاعة")]
        public HallSize HallSize { set; get; }

        [Required]
        [StringLength(1000, MinimumLength = 200, ErrorMessage = "غير مسموح باقل من 200 حرف او اكثر من 1000 حرف")]
        public string HallDescription { set; get; }

        [Required(ErrorMessage = "ادخل عدد الضيوف")]
        public int GuestNumber { set; get; }

        [Required(ErrorMessage = "ضع سعر تقريبى للقاعة")]
        public float Budget { set; get; }

        [Required(ErrorMessage = "ادخل عنوان الحجز المطلوب")]
        [StringLength(400)]
        public string Location { set; get; }

        [Required(ErrorMessage = "عدد المباشرات")]
        public Int16 SuperVisorFNumber { set; get; }


        [Required(ErrorMessage = "عدد المباشرين")]
        public Int16 SuperVisorMNumber { set; get; }

        [Required(ErrorMessage ="ادخل رقم للتواصل")]
        public string ContactNumber { set; get; }

        [Required(ErrorMessage ="أدخل مدينتك")]
        public string UserCity { set; get; }

       

    }
}
