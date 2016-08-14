using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AllServiceProvider.Models
{
    public class Skill
    {

        public int Id { set; get; }

        [Required(ErrorMessage = "Please enter the Skill")]
        [Display(Name = "Skill")]
        public string SkillName { set; get; }
    }
}