using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetZen.Data
{
    public class Feeding
    {
        [Key]
        public int FeedingId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public DateTimeOffset FeedDateTime { get; set; }

        [Required]
        [ForeignKey(nameof(Pet))]
        public int PetId { get; set; }
        public virtual Pet Pet { get; set; }

        [Required]
        [ForeignKey(nameof(Food))]
        public int FoodId { get; set; }
        public virtual Food Food { get; set; }  

        public double AmountFed { get; set; }

        public MeasurementEnum Measurement { get; set; }

        [Required]
        public bool Default { get; set; }

        public string Notes { get; set; }
    }
}

