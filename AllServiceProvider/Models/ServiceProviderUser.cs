using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AllServiceProvider.Models
{
    public class ServiceProviderUser
    {

        public int Id { set; get; }

        [Required(ErrorMessage = "Please enter the name")]
        [Display(Name = "Name")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Please enter the Phone Number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { set; get; }
        public string AppUserId { set; get; }



        [Required(ErrorMessage = "Please select the Skill")]
        [Display(Name = "Skill")]
        public int SkillId { set; get; }



        [Display(Name = "Skill")]
        [ForeignKey("SkillId")]
        public virtual Skill Skill { get; set; }

        [Required(ErrorMessage = "Please select the City")]
        [Display(Name = "City")]
        public int CityId { set; get; }


        [Display(Name = "City")]
        [ForeignKey("CityId")]
        public virtual City City { get; set; }

        public virtual ICollection<SiteUser> SiteUser { set; get; }


        public virtual ICollection<Review> Reviews { set; get; }


    }
}