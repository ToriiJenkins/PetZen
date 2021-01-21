using PetZen.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetZen.Models.ActivityModels
{
    public class ActivityListItem
    {
        public int ActivityId { get; set; }
        public string PetName { get; set; }
        public ActivityEnum ActType { get; set; }
        public DateTimeOffset Date {get; set;}
        public string Notes { get; set; }

    }
}
