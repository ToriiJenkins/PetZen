using PetZen.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetZen.Models.FoodModels
{
    public class FoodEdit
    {
        public int FoodId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public SpeciesEnum Species { get; set; }

        //[Required]
        //public double ServingSize { get; set; }

        public string PurchaseLink { get; set; }
    }
}
