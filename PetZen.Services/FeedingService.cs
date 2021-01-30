using PetZen.Data;
using PetZen.Models.FeedingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetZen.Services
{
    public class FeedingService
    {
        private readonly Guid _userId;

        public FeedingService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateFeeding(FeedingCreate model)
        {
            var entity =
                new FeedingListitem()
                {
                    OwnerId = _userId,
                    FeedDateTime = DateTimeOffset.Now,
                    PetId = model.PetId,
                    FoodId = model.FoodId,
                    AmountFed = model.AmountFed,
                    Measurement = model.Measurement,
                    Default = model.Default,
                    Notes = model.Notes

                };

            using (var ctx = new ApplicationDbContext())
            {
                FeedingListitem Feeding = ctx.Feedings.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<FeedingListItem> GetFeedings()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Feedings.OrderBy(e => e.PetId)
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new FeedingListItem
                                {
                                    FeedingId = e.FeedingId,
                                    FeedDateTime = e.FeedDateTime,
                                    PetName = e.Pet.Name,
                                    FoodName = e.Food.Name,
                                    AmountFed = e.AmountFed,
                                    Measurement = e.Measurement,
                                    Default = e.Default,
                                    Notes = e.Notes
                                }
                        
                         );
                return query.ToArray();
            }
        }

        public FeedingDetail GetFeedingById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Feedings
                        .Single(e => e.FeedingId == id && e.OwnerId == _userId);
                return
                    new FeedingDetail
                    {
                        FeedingId = entity.FeedingId,
                        FeedDateTime = entity.FeedDateTime,
                        PetId = entity.PetId,
                        PetName = entity.Pet.Name,
                        FoodId = entity.FoodId,
                        FoodName = entity.Food.Name,
                        AmountFed =entity.AmountFed,
                        Measurement = entity.Measurement,
                        Default = entity.Default,
                        Notes = entity.Notes,
                    };
            }
        }

        public bool UpdateFeeding(FeedingEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Feedings
                        .Single(e => e.FeedingId == model.FeedingId && e.OwnerId == _userId);

                entity.FeedDateTime = model.FeedDateTime;
                entity.PetId = model.PetId;
                entity.FoodId = model.FoodId;
                entity.AmountFed = model.AmountFed;
                entity.Measurement = model.Measurement;
                entity.Default = model.Default;
                entity.Notes = model.Notes;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteFeeding (int FeedingId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                        ctx
                            .Feedings
                            .Single(e => e.FeedingId == FeedingId && e.OwnerId == _userId);

                ctx.Feedings.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<FeedingListItem> GetFeedingsByPet(int petId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Feedings
                        .Where(e => e.PetId == petId && e.OwnerId == _userId )
                        .Select(
                            e =>
                                new FeedingListItem
                                {
                                    FeedingId = e.FeedingId,
                                    FeedDateTime = e.FeedDateTime,
                                    PetName = e.Pet.Name,
                                    FoodName = e.Food.Name,
                                    AmountFed = e.AmountFed,
                                    Measurement = e.Measurement,
                                    Default = e.Default,
                                    Notes = e.Notes
                                }
                         );
                return query.ToArray();
            }
        }
    }
}

