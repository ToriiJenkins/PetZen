using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetZen.Data
{
    public class Administration
    {
        [Key]
        public int AdminId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        [ForeignKey(nameof(Pet))]
        public int PetId { get; set; }
        public virtual Pet Pet { get; set; }
        [Required]
        [ForeignKey(nameof(Medication))]
        public int MedId { get; set; }
        public virtual Medication Medication { get; set; }
        [Required]
        public DateTimeOffset AdminDateTime { get; set; }
        [Required]
        public double Dosage { get; set; }
        [Required]
        public MeasurementEnum DoseMeasure { get; set; }
        [Required]
        public bool? Defalut { get; set; }
        public string Notes { get; set; }
    }
}
