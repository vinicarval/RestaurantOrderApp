namespace RestaurantOrderApp.Core.Entities
{
    public class Dish
    {
        public Dish(string name, Enums.DishType type, string timeOfDay)
        {
            Name = name;
            Type = type;
            TimeOfDay = timeOfDay;
        }

        public string Name { get; private set; }
        public Enums.DishType Type { get; private set; }
        public string TimeOfDay { get; private set; }
    }
}
