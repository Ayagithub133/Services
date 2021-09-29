using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Shared.Data
{
    public class AppUser
    {
        
        public string Id { set; get; }
        [StringLength(50)]
        [Required]
        public string FirstName { set; get; }

        [StringLength(50)]
        [Required]
        public string SecondName { set; get; }
        [StringLength(50)]
        [Required]
        public string LastName { set; get; }

        [StringLength(50)]

        public string City { set; get; }

        public string UserImage { set; get; }

        public DateTime CreateAt { set; get; }

        public bool isActive { set; get; }
        public string Email { set; get; }
        public virtual ICollection<Messages> ChatMessagesFromUsers { set; get; }
        public virtual ICollection<Messages> ChatMessagesToUsers { set; get; }
    }
}
