using System.ComponentModel;

namespace RestaurantOrderApp.Core.Enums
{
    public enum DishType
    {
        [Description("entrée")]
        entree = 1,
        side = 2,
        drink = 3,
        dessert = 4
    }
}
