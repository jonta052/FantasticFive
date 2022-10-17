using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace NewsApp.Models
{
    public class EditUserViewModel
    {
        //public EditUserViewModel()
        //{
        //    Roles = new List<string>();
        //}

        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        //[Required]
        //public string LasstName { get; set; }

        //[Required]
        //public decimal PhoneNumber  { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        //public IList<string> Roles { get; set; }
    }
}
