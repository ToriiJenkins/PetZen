using PetZen.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetZen.Models.FoodModels
{
    public class FoodDetail
    {
        public int FoodId { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public SpeciesEnum Species { get; set; }
        //public double ServingSize { get; set; }

        public string PurchaseLink { get; set; }
    }
}
