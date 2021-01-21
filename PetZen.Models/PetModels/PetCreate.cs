using PetZen.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetZen.Models.PetModels
{
    public class PetCreate
    {
        [Required]
        public string Name { get; set; }

        public SpeciesEnum Species { get; set; }

        public string Breed { get; set; }

        public double? Weight { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Required]
        [Range(1, 4, ErrorMessage = "Enter a value between 1 and 4.")]
        public int MealsPerDay { get; set; }
    }
}
