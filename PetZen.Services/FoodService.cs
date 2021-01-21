using PetZen.Data;
using PetZen.Models;
using PetZen.Models.FoodModels;
using PetZen.Models.MedicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetZen.Services
{
    public class FoodService
    {
        private readonly Guid _userId;

        public FoodService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateFood(FoodCreate model)
        {
            var entity =
                new Food()
                {
                    OwnerId = _userId,
                    Name = model.Name,
                    //Species = model.Species,
                    //ServingSize = model.ServingSize,
                    PurchaseLink = model.PurchaseLink
                };

            using (var ctx = new ApplicationDbContext())
            {
                Food food = ctx.Foods.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<FoodListItem> GetFoods()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Foods
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new FoodListItem
                                {
                                    FoodId = e.FoodId,
                                    Name = e.Name,
                                    Species = e.Species,
                                    //ServingSize = e.ServingSize,
                                    PurchaseLink = e.PurchaseLink
                                }
                         );
                return query.ToArray();
            }
        }

        public FoodDetail GetFoodById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Foods
                        .Single(e => e.FoodId == id && e.OwnerId == _userId);
                return
                    new FoodDetail
                    {
                        FoodId = entity.FoodId,
                        Name = entity.Name,
                        Species = entity.Species,
                        //ServingSize = entity.ServingSize,
                        PurchaseLink = entity.PurchaseLink
                        
                    };
            }
        }

        public bool UpdateFood(FoodEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Foods
                        .Single(e => e.FoodId == model.FoodId && e.OwnerId == _userId);

                entity.Name = model.Name;
                entity.Species = model.Species;
                //entity.ServingSize = model.ServingSize;
                entity.PurchaseLink = model.PurchaseLink;

                return ctx.SaveChanges() == 1;

            }
        }

        public bool DeleteFood(int foodId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                        ctx
                            .Foods
                            .Single(e => e.FoodId == foodId && e.OwnerId == _userId);

                ctx.Foods.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
