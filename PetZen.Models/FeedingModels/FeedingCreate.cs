using PetZen.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PetZen.Models.FeedingModels
{
    public class FeedingCreate
    {
        [Required]
        public DateTimeOffset FeedDateTime { get; set; }

        //Dropdown for Pets
        public IEnumerable<SelectListItem> Pets { get; set; }
        //Selected Pet
        public int PetId { get; set; }

        //Dropdown for Foods
        public IEnumerable<SelectListItem> Foods { get; set; }
        //Selected Food
        public int FoodId { get; set; }

        public double AmountFed { get; set; }

        public MeasurementEnum Measurement { get; set; }

        [Required]
        public bool Default { get; set; }

        public string Notes { get; set; }
    }
}
