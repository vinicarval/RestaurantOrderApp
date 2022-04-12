using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantOrderApp.Core.Interfaces.Services
{
    public interface IDishService
    {
        public Task<string> GetByTimeOfDayAndTypes(string timeOfDay, string dishTypes);
    }
}
