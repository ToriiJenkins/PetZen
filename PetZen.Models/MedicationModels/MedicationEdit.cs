using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PetZen.Models.MedicationModels
{
    public class MedicationEdit
    {
        public int MedId { get; set; }
        
        [Required]
        public string Name { get; set; }

        ////Dropdown for Pets
        //public IEnumerable<SelectListItem> Pets { get; set; }
        ////Selected Pet
        //public int PetId { get; set; }

        //public int PetName { get; set; }

        //[Required]
        //public double Dosage { get; set; }

        [Required]
        public int TimesPerDay { get; set; }

        [Required]
        public DateTime BeginDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string RefillLink { get; set; }
    }
}
