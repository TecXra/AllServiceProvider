using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AllServiceProvider.Models
{
    public class City
    {
        public int Id { set; get; }

        [DisplayName("City")]
        public string CityName { set; get; }
    }
}