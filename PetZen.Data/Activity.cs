 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetZen.Data
{
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public ActivityEnum ActType { get; set; }

        [Required]
        [ForeignKey(nameof(Pet))]
        public int PetId { get; set; }
        public virtual Pet Pet { get; set; }

        [Required]
        public bool Default { get; set; }

        [Required]
        public DateTimeOffset Date { get; set; }

        public string Notes { get; set; }
    }
}
