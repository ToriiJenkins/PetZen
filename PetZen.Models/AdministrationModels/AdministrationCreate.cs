using PetZen.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PetZen.Models.AdministrationModels
{
    public class AdministrationCreate
    {

        //Dropdown for Pets
        public IEnumerable<SelectListItem> Pets { get; set; }
        //Selected Pet
        public int PetId { get; set; }

        //Dropdown for Medications
        public IEnumerable<SelectListItem> Medications { get; set; }
        //Selected Medication
        public int MedId { get; set; }

        [Required]
        public DateTimeOffset AdminDateTime { get; set; }

        [Required]
        public double Dosage { get; set; }
        [Required]
        public MeasurementEnum DoseMeasure { get; set; }
        [Required]
        public bool Defalut { get; set; }
        public string Notes { get; set; }
    }
}
