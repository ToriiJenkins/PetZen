using PetZen.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PetZen.Models.ActivityModels
{
    public class ActivityEdit
    {
        public int ActivityId { get; set; }
        
        [Required]
        public ActivityEnum ActType { get; set; }

        //Dropdown for Pets
        public IEnumerable<SelectListItem> Pets { get; set; }
        //Selected Pet
        public int PetId { get; set; }

        [Required]
        public DateTimeOffset Date { get; set; }

        public string Notes { get; set; }
    }
}
