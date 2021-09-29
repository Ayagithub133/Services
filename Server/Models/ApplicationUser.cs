using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Server.Models
{
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser()
        {
            ChatMessagesFromUsers = new HashSet<ChatMessage>();
            ChatMessagesToUsers = new HashSet<ChatMessage>();
        }

       
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

        public virtual ICollection<ChatMessage> ChatMessagesFromUsers { set; get; }
        public virtual ICollection<ChatMessage> ChatMessagesToUsers { set; get; }
    }
}
