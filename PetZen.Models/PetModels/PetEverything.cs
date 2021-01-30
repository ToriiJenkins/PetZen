using PetZen.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetZen.Models.PetModels
{
    public class PetEverything
    {
        public int PetId { get; set; }
        public string PetName { get; set; }
        public IEnumerable<FeedingListitem> PetFeedings { get; set; }
        public IEnumerable<Administration> PetAdministrations { get; set; }
        public IEnumerable<Activity> PetActivities { get; set; }
    }
}
