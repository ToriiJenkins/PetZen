using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetZen.Data
{
    public class Food
    {
        [Key]
        public int FoodId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public SpeciesEnum Species { get; set; }

        public string PurchaseLink { get; set; }
    }
}
