using PetZen.Data;
using PetZen.Models.AdministrationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetZen.Services
{
    public class AdministrationService
    {
        private readonly Guid _userId;

        public AdministrationService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateAdministration(AdministrationCreate model)
        {
            var entity =
                new Administration()
                {
                    OwnerId = _userId,
                    PetId = model.PetId,
                    MedId = model.MedId,
                    AdminDateTime = model.AdminDateTime,
                    Dosage = model.Dosage,
                    DoseMeasure = model.DoseMeasure,
                    Defalut = model.Defalut,
                    Notes = model.Notes

                };

            using (var ctx = new ApplicationDbContext())
            {
                Administration Administration = ctx.Administrations.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<AdministrationListItem> GetAdministrations()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Administrations
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new AdministrationListItem
                                {
                                    AdminId = e.AdminId,                         PetName = e.Pet.Name,
                                    MedName = e.Medication.Name,
                                    AdminDateTime= e.AdminDateTime,
                                    Dosage = e.Dosage,
                                    DoseMeasure = e.DoseMeasure,
                                    Default = e.Defalut,
                                    Notes = e.Notes
                                }
                         );
                return query.ToArray();
            }
        }

        public AdministrationDetail GetAdministrationById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Administrations
                        .Single(e => e.AdminId == id && e.OwnerId == _userId);
                return
                    new AdministrationDetail
                    {
                        AdminId = entity.AdminId,
                        PetName = entity.Pet.Name,
                        MedName = entity.Medication.Name,
                        AdminDateTime = entity.AdminDateTime,
                        Dosage = entity.Dosage,
                        DoseMeasure =entity.DoseMeasure,
                        Default = entity.Defalut,
                        Notes = entity.Notes,
                    };
            }
        }

        public bool UpdateAdministration(AdministrationEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Administrations
                        .Single(e => e.AdminId == model.AdminId && e.OwnerId == _userId);
                entity.PetId = model.PetId;
                entity.MedId = model.MedId;
                entity.AdminDateTime= model.AdminDateTime;
                entity.Dosage = model.Dosage;
                entity.DoseMeasure = model.DoseMeasure;
                entity.Defalut = model.Default;
                entity.Notes = model.Notes;

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
