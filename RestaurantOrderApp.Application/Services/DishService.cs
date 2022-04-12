using RestaurantOrderApp.Core.Enums;
using RestaurantOrderApp.Core.Exceptions;
using RestaurantOrderApp.Core.Interfaces.Repositories;
using RestaurantOrderApp.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderApp.Application.Services
{
    public class DishService : IDishService
    {
        private readonly IDishRepository _dishRepository;
        public DishService(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }

        public async Task<string> GetByTimeOfDayAndTypes(string timeOfDay, string dishTypes)
        {
            timeOfDay = timeOfDay?.ToLower();
            dishTypes = dishTypes?.ToLower();

            if (!(timeOfDay?.Equals("morning") ?? false) && (!timeOfDay?.Equals("night") ?? false))
            {
                throw new BusinessValidationException("dish.getByTimeOfDayAndTypes.invalidTimeOfDay");
            }

            if (string.IsNullOrEmpty(dishTypes))
            {
                throw new BusinessValidationException("dish.getByTimeOfDayAndTypes.invalidDishTypes");
            }

            List<DishType> dishTypesList = new List<DishType>();
            List<DishType> dishValidTypesList = new List<DishType>();
            var dishTypesString = dishTypes.Split(",");
            foreach (var type in dishTypesString)
            {
                if (!int.TryParse(type, out var intDishType))
                {
                    throw new BusinessValidationException("dish.getByTimeOfDayAndTypes.invalidDishTypes");
                }

                if (Enum.IsDefined(typeof(DishType), intDishType))
                {
                    dishValidTypesList.Add((DishType)intDishType);
                }

                dishTypesList.Add((DishType)intDishType);
            }

            if (!dishValidTypesList.Any())
            {
                throw new BusinessValidationException("dish.getByTimeOfDayAndTypes.invalidDishTypes");
            }

            var dishes = (await _dishRepository.GetByTimeOfDayAndTypes(timeOfDay, dishValidTypesList));

            if (dishes == null || !dishes.Any())
            {
                return null;
            }

            var response = new StringBuilder();
            dishTypesList = dishTypesList.OrderBy(x => x).ToList();
            for (int i = 0; i < dishTypesList.Count; i++)
            {
                var dish = dishes.FirstOrDefault(x => x.Type == dishTypesList[i]);

                if (dish == null || (!dish.Name.Equals("coffee") && !dish.Name.Equals("potato") && response.ToString().Contains(dish.Name)))
                {
                    response.Append(", error");
                    return response.ToString();
                }

                if (!response.ToString().Contains(dish.Name))
                {
                    if (i != 0)
                    {
                        response.Append(", ");
                    }

                    var x = (dish.Name.Equals("coffee") || dish.Name.Equals("potato")) && dishTypesList.Count(x => x == dishTypesList[i]) > 1 ? "(x" + dishTypesList.Count(x => x == dishTypesList[i]) + ")" : null;
                    response.Append($"{dish.Name}{x}");
                }
            }

            return response.ToString();
        }
    }
}
