using RestaurantOrderApp.Core.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantOrderApp.Core.Interfaces.Repositories
{
    public interface IDishRepository
    {
        public Task<List<Entities.Dish>> GetByTimeOfDayAndTypes(string timeOfDay, List<DishType> dishTypes);
    }
}
