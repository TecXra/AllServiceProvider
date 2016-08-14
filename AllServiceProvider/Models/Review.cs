using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
namespace AllServiceProvider.Models
{
    public class Review
    {
        public int Id { set; get; }

        [Required(ErrorMessage = "Please enter the User Id")]
        [Display(Name = "User Id")]
        public int UserId { set; get; }

        [Required(ErrorMessage = "Please enter the FeedBack")]
        [Display(Name = "FeedBack")]
        public string FeedBack { set; get; }

        [Required(ErrorMessage = "Please enter the Writer Id")]
        [Display(Name = "Writer Id")]
        public int WriterId { get; set; }

        [ForeignKey("UserId")]
        public virtual ServiceProviderUser ServiceProviderUser { get; set; }



        [ForeignKey("WriterId")]
        public virtual SiteUser SiteUser { get; set; }



    }
}