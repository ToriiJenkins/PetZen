using PetZen.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetZen.Models.PetModels
{
    public class DailyPet
    {
        public int PetId { get; set; }
        public string Name { get; set; }

        public int MealsPerDay {get; set;}

        public int MedicationsPerDay { get; set; }
    }
}
