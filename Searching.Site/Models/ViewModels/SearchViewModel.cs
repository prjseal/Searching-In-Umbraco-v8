using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Searching.Site.Models.ViewModels
{
    public class SearchViewModel
    {
        [Display(Name ="Search Term")]
        [Required(ErrorMessage ="You must enter a search term")]
        public string Query { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "Page Number")]
        public string Page { get; set; }
    }
}