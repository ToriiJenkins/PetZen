using PetZen.Data;
using PetZen.Models;
using PetZen.Models.PetModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetZen.Services
{
    public class PetService
    {
        private readonly Guid _userId;

        public PetService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreatePet(PetCreate model)
        {
            var entity =
                new Pet()
                {
                    OwnerId = _userId,
                    Name = model.Name,
                    Species = model.Species,
                    Breed = model.Breed,
                    Weight = model.Weight,
                    DateOfBirth = model.DateOfBirth,
                    MealsPerDay = model.MealsPerDay
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
                                    MealsPerDay = e.MealsPerDay
                                }
                         ) ;
                return query.ToArray();
            }
        }

        public PetDetail GetPetById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Pets
                        .Single(e => e.PetId == id && e.OwnerId == _userId);
                return
                    new PetDetail
                    {
                        PetId = entity.PetId,
                        Name = entity.Name,
                        Species = entity.Species,
                        Breed = entity.Breed,
                        Weight = entity.Weight,
                        DateOfBirth = entity.DateOfBirth,
                        MealsPerDay = entity.MealsPerDay
                    };
            }
        }

        public bool UpdatePet(PetEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Pets
                        .Single(e => e.PetId == model.PetId && e.OwnerId == _userId);

                entity.Name = model.Name;
                entity.Species = model.Species;
                entity.Breed = model.Breed;
                entity.Weight = model.Weight;
                entity.DateOfBirth = model.DateOfBirth;
                entity.MealsPerDay = model.MealsPerDay;

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
    }
}
