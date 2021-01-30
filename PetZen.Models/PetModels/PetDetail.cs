using PetZen.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetZen.Models.PetModels
{
    public class PetDetail
    {
        public int PetId { get; set; }
        public string Name { get; set; }
        public SpeciesEnum Species { get; set; }
        public string Breed { get; set; }
        public double? Weight { get; set; }

        [Display(Name = "Birthday")]
        public DateTime? DateOfBirth { get; set; }
        public int MealsPerDay { get; set; }

        public int MedAdminsPerDay { get; set; }
    }
}
