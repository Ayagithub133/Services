using Services.Shared.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Server.Models
{
    public class Hall : BaseEntity
    {

      [Required]
      [StringLength(100,MinimumLength =20)]
      public string ServiceTitle { set; get; }
      
      [Required]
      [DataType(DataType.Date)]
      public DateTime BookDate { set; get; }

     
      public BookType BookType { set; get; }

    
      public Menue Menue { set; get; }

     
      public GuestGender GuestGender { set; get; }

 
      public HallSize HallSize { set; get; }

      [Required]
      [StringLength(1000,MinimumLength =200)]
      public string    HallDescription { set; get; }

    
     public int GuestNumber { set; get; }

     [Required]
     public double Budget { set; get; }

     [Required]
     [StringLength(400)]
     public string  Location { set; get; }

     [Required]
     public Int16 SuperVisorFNumber { set; get; }

    [Required]
    public string UserContact { set; get; }

    [Required]
    public string UserCity { set; get; }

    [Required]
     public Int16  SuperVisorMNumber { set; get; }

    public bool isReviwed { set; get; }


    public DateTime TimeAdd { set; get; }

     public string Pic1 { set; get; }
     public string Pic2 { set; get; } 
     public string Pic3 { set; get; }
     public string Pic4 { set; get; }
     public string Pic5 { set; get; }

     [ForeignKey("User")]
     public string UserId { set; get; }
     public ApplicationUser User { set; get; }
    }
}
