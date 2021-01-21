using PetZen.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetZen.Models.MedicationModels
{
    public class MedicationDetail
    {
        public int MedId { get; set; }
        public string Name { get; set; }
        //public int PetId { get; set; }
        public string PetName { get; set; }
        public double Dosage { get; set; }
        public int TimesPerDay { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string RefillLink { get; set; }
    }
}

