using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kendo_asp_mvc.Models
{
    public class PersonModel
    {
        [Required]
        public string Name
        {
            get;
            set;
        }

        public string Country
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        [Required]
        public int Id
        {
            get;
            set;
        }
    }
}