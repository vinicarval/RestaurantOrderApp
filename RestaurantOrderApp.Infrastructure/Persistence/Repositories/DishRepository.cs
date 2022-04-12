using RestaurantOrderApp.Core.Entities;
using RestaurantOrderApp.Core.Enums;
using RestaurantOrderApp.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantOrderApp.Infrastructure.Persistence.Repositories
{
    public class DishRepository : IDishRepository
    {
        //dishes mocadas por conta de não usar o banco de dados
        private readonly List<Dish> _dishes;

        public DishRepository()
        {
            _dishes = new List<Dish>
            {
                new Dish("eggs", DishType.entree, "morning"),
                new Dish("steak", DishType.entree, "night"),
                new Dish("toast", DishType.side, "morning"),
                new Dish("potato", DishType.side, "night"),
                new Dish("coffee", DishType.drink, "morning"),
                new Dish("wine", DishType.drink, "night"),
                new Dish("cake", DishType.dessert, "night")
            };
        }

        public Task<List<Dish>> GetByTimeOfDayAndTypes(string timeOfDay, List<DishType> dishTypes)
        {
            return Task.FromResult(_dishes.Where(x => x.TimeOfDay.Equals(timeOfDay) && dishTypes.Contains(x.Type))?.OrderBy(x => x.Type)?.ToList());
        }
    }
}
