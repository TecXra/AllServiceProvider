using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AllServiceProvider.Models
{
    public class SiteUser
    {

        public int Id { set; get; }


        [Required(ErrorMessage = "Please enter the name")]
        [Display(Name = "Name")]
        public string Name { set; get; }


        [Required(ErrorMessage = "Please enter the Phone Number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { set; get; }

        public string AppUserId { set; get; }
    //    public virtual Skill Skill { get; set; }
        //public int CityId { set; get; }
        //[ForeignKey("CityId")]
        //public virtual City City { get; set; }



        public virtual ICollection<ServiceProviderUser> ServiceProviderUser { set; get; }

        public virtual ICollection<Review> Reviews { set; get; }


    }
}