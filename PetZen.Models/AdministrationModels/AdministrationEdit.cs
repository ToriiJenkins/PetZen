﻿using PetZen.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetZen.Models.AdministrationModels
{
    public class AdministrationEdit
    {
        public int AdminId { get; set; }
        public int PetId { get; set; }
        public string PetName { get; set; }
        public int MedId { get; set; }
        public string MedName { get; set; }
        public DateTimeOffset AdminDateTime { get; set; }
        public double Dosage { get; set; }
        public MeasurementEnum DoseMeasure { get; set; }
        public bool? Default { get; set; }
        public string Notes { get; set; }
    }
}