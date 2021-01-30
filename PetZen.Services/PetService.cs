using PetZen.Contracts;
using PetZen.Data;
using PetZen.Models;
using PetZen.Models.FeedingModels;
using PetZen.Models.PetModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetZen.Services
{
    public class PetService : IPetService
    {
        private readonly Guid _userId;

        public PetService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreatePet(PetCreate petToCreate)
        {
            var entity =
                new Pet()
                {
                    OwnerId = _userId,
                    Name = petToCreate.Name,
                    Species = petToCreate.Species,
                    Breed = petToCreate.Breed,
                    Weight = petToCreate.Weight,
                    DateOfBirth = petToCreate.DateOfBirth,
                    MealsPerDay = petToCreate.MealsPerDay,
                    MedAdminsPerDay = petToCreate.MealsPerDay
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Pets.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }


        public IEnumerable<PetListItem> GetPets()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Pets
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new PetListItem
                                {
                                    PetId = e.PetId,
                                    Name = e.Name,
                                    Species = e.Species,
                                    Breed = e.Breed,
                                    Weight = e.Weight,
                                    DateOfBirth = e.DateOfBirth,
                                    MealsPerDay = e.MealsPerDay,
                                    MedAdminsPerDay= e.MedAdminsPerDay
                                }
                         ) ;
                return query.ToArray();
            }
        }

        public PetDetail GetPetById(int petId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Pets
                        .Single(e => e.PetId == petId && e.OwnerId == _userId);
                return
                    new PetDetail
                    {
                        PetId = entity.PetId,
                        Name = entity.Name,
                        Species = entity.Species,
                        Breed = entity.Breed,
                        Weight = entity.Weight,
                        DateOfBirth = entity.DateOfBirth,
                        MealsPerDay = entity.MealsPerDay,
                        MedAdminsPerDay = entity.MedAdminsPerDay
                    };
            }
        }

        public bool UpdatePet(PetEdit petToEdit)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Pets
                        .Single(e => e.PetId == petToEdit.PetId && e.OwnerId == _userId);

                entity.Name = petToEdit.Name;
                entity.Species = petToEdit.Species;
                entity.Breed = petToEdit.Breed;
                entity.Weight = petToEdit.Weight;
                entity.DateOfBirth = petToEdit.DateOfBirth;
                entity.MealsPerDay = petToEdit.MealsPerDay;
                entity.MedAdminsPerDay = petToEdit.MedAdminsPerDay;

                return ctx.SaveChanges() == 1;


            }
        }

        public bool DeletePet(int petId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                        ctx
                            .Pets
                            .Single(e => e.PetId == petId && e.OwnerId == _userId);

                ctx.Pets.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<DailyPet> GetDailyPets()
        {
            using (var ctx = new ApplicationDbContext())
            {
               
                var query =
                    ctx
                        .Pets
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new DailyPet
                                {
                                    PetId = e.PetId,
                                    Name = e.Name, 
                                    Species = e.Species,
                                    MealsPerDay = e.MealsPerDay,
                                    MedAdminsPerDay = e.MedAdminsPerDay
                                }
                         );
                return query.ToArray();
            }
        }

        //public IEnumerable<FeedingListItem> GetPetFeedings(int petId)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {

        //        var query =
        //            ctx
        //                .Feedings
        //                .Where(e => e.PetId == petId)
        //                .Select(
        //                    e =>
        //                        new FeedingListItem
        //                        {
        //                            FeedingId = e.FeedingId,
                                  
        //                            FeedDateTime = e.FeedDateTime,
        //                            PetName= e.Pet.Name,
        //                            FoodName= e.Food.Name,
        //                            AmountFed = e.AmountFed,
        //                            Measurement = e.Measurement,
        //                            Default = e.Default,
        //                            Notes = e.Notes
        //                        }
        //                 );
        //        return query.ToArray();
        //    }
        //}

        //public IEnumerable<Administration> GetPetAdmins(int petId)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {

        //        var query =
        //            ctx
        //                .Administrations
        //                .Where(e => e.PetId == petId && e.OwnerId == _userId)
        //                .Select(
        //                    e =>
        //                        new Administration
        //                        {
        //                            AdminId = e.AdminId,
        //                            OwnerId = _userId,
        //                            PetId = e.PetId,
        //                            MedId = e.MedId,
        //                            AdminDateTime = e.AdminDateTime,
        //                            Dosage = e.Dosage,
        //                            DoseMeasure = e.DoseMeasure,
        //                            Defalut = e.Defalut,
        //                            Notes = e.Notes
        //                        }
        //                 ); ;
        //        return query.ToArray();
        //    }
        //}

        //public IEnumerable<Activity> GetPetActivities(int petId)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {

        //        var query =
        //            ctx
        //                .Activities
        //                .Where(e => e.PetId == petId && e.OwnerId == _userId)
        //                .Select(
        //                    e =>
        //                        new Activity
        //                        {
        //                            ActivityId = e.ActivityId,
        //                            OwnerId = _userId,
        //                            ActType = e.ActType,
        //                            PetId = e.PetId,
        //                            Default = e.Default,
        //                            Date = e.Date,
        //                            Notes = e.Notes
        //                        }
        //                 ); 
        //        return query.ToArray();
        //    }
        //}


    }
}
