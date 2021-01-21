using PetZen.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetZen.Models.FeedingModels
{
    public class FeedingEdit
    {
        public int FeedingId { get; set; }
        public DateTimeOffset FeedDateTime { get; set; }
        public int PetId { get; set; }
        public string PetName { get; set; }
        public int FoodId { get; set; }
        public String FoodName { get; set; }
        public double AmountFed { get; set; }
        public MeasurementEnum Measurement{get; set; }
        public bool Default { get; set; }
        public string Notes { get; set; }
    }
}
