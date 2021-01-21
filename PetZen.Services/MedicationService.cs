using PetZen.Data;
using PetZen.Models;
using PetZen.Models.MedicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetZen.Services
{
    public class MedicationService
    {
        private readonly Guid _userId;

        public MedicationService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateMedication(MedicationCreate model)
        {
            var entity =
                new Medication()
                {
                    OwnerId = _userId,
                    Name = model.Name,
                    //PetId = model.PetId,
                    //Dosage = model.Dosage,
                    TimesPerDay = model.TimesPerDay,
                    BeginDate = model.BeginDate,
                    EndDate = model.EndDate,
                    RefillLink = model.RefillLink

                };

            using (var ctx = new ApplicationDbContext())
            {
                Medication medication = ctx.Medications.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<MedicationListItem> GetMedications()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Medications
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new MedicationListItem
                                {
                                    MedId = e.MedId,
                                    Name = e.Name,
                                    //PetName = e.Pet.Name,
                                    //Dosage = e.Dosage,
                                    TimesPerDay = e.TimesPerDay,
                                    BeginDate = e.BeginDate,
                                    EndDate = e.EndDate,
                                    RefillLink = e.RefillLink
                                }
                         );
                return query.ToArray();
            }
        }

        public MedicationDetail GetMedicationById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Medications
                        .Single(e => e.MedId == id && e.OwnerId == _userId);
                return
                    new MedicationDetail
                    {
                        MedId = entity.MedId,
                        Name = entity.Name,
                        //PetId = entity.PetId,
                        //PetName = entity.Pet.Name,
                        //Dosage = entity.Dosage,
                        TimesPerDay = entity.TimesPerDay,
                        BeginDate = entity.BeginDate,
                        EndDate = entity.EndDate,
                        RefillLink = entity.RefillLink
                    };
            }
        }

        public bool UpdateMedication(MedicationEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Medications
                        .Single(e => e.MedId == model.MedId && e.OwnerId == _userId);

                entity.Name = model.Name;
                //entity.PetId = model.PetId;
                //entity.Dosage = model.Dosage;
                entity.TimesPerDay = model.TimesPerDay;
                entity.BeginDate = model.BeginDate;
                entity.EndDate = model.EndDate;
                entity.RefillLink = model.RefillLink;

                return ctx.SaveChanges() == 1;

            }
        }

        public bool DeleteMedication(int medId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                        ctx
                            .Medications
                            .Single(e => e.MedId == medId && e.OwnerId == _userId);

                ctx.Medications.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
