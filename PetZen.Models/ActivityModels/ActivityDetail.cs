using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetZen.Data;

namespace PetZen.Models.ActivityModels
{
    public class ActivityDetail
    {
        public int ActivityId { get; set; }

        public Guid OwnerId { get; set; }

        public int PetId { get; set; }

        public string PetName { get; set; }

        public ActivityEnum ActType { get; set; }

       public DateTimeOffset Date { get; set; }

       public string Notes { get; set; }

    }
}
