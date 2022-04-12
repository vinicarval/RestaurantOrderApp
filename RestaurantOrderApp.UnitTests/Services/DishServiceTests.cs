using Moq;
using NUnit.Framework;
using RestaurantOrderApp.Application.Services;
using RestaurantOrderApp.Core.Entities;
using RestaurantOrderApp.Core.Enums;
using RestaurantOrderApp.Core.Exceptions;
using RestaurantOrderApp.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderApp.UnitTests.Services
{
    [TestFixture]
    public class DishServiceTests
    {
        private Mock<IDishRepository> _moqDishRepository;

        [SetUp]
        public void Setup()
        {
            _moqDishRepository = new Mock<IDishRepository>();

            _moqDishRepository.Setup
               (x => x.GetByTimeOfDayAndTypes("morning", new List<DishType> { DishType.entree, DishType.side, DishType.drink }))
               .Returns(Task.FromResult(new List<Dish>
               {
                    new Dish("eggs", DishType.entree, "morning"),
                    new Dish("toast", DishType.side, "morning"),
                    new Dish("coffee", DishType.drink, "morning")
               }));

            _moqDishRepository.Setup
                (x => x.GetByTimeOfDayAndTypes("morning", new List<DishType> { DishType.side, DishType.entree, DishType.drink }))
                .Returns(Task.FromResult(new List<Dish>
                {
                    new Dish("eggs", DishType.entree, "morning"),
                    new Dish("toast", DishType.side, "morning"),
                    new Dish("coffee", DishType.drink, "morning")
                }));

            _moqDishRepository.Setup
               (x => x.GetByTimeOfDayAndTypes("morning", new List<DishType> { DishType.entree, DishType.side, DishType.drink, DishType.dessert }))
               .Returns(Task.FromResult(new List<Dish>
               {
                    new Dish("eggs", DishType.entree, "morning"),
                    new Dish("toast", DishType.side, "morning"),
                    new Dish("coffee", DishType.drink, "morning")
               }));

            _moqDishRepository.Setup
                (x => x.GetByTimeOfDayAndTypes("morning", new List<DishType> { DishType.entree, DishType.side, DishType.drink, DishType.drink, DishType.drink }))
                .Returns(Task.FromResult(new List<Dish>
                {
                    new Dish("eggs", DishType.entree, "morning"),
                    new Dish("toast", DishType.side, "morning"),
                    new Dish("coffee", DishType.drink, "morning")
                }));

            _moqDishRepository.Setup
                (x => x.GetByTimeOfDayAndTypes("night", new List<DishType> { DishType.entree, DishType.side, DishType.drink, DishType.dessert }))
                .Returns(Task.FromResult(new List<Dish>
                {
                    new Dish("steak", DishType.entree, "night"),
                    new Dish("potato", DishType.side, "night"),
                    new Dish("wine", DishType.drink, "night"),
                    new Dish("cake", DishType.dessert, "night")
                }));

            _moqDishRepository.Setup
                (x => x.GetByTimeOfDayAndTypes("night", new List<DishType> { DishType.entree, DishType.side, DishType.side, DishType.dessert }))
                .Returns(Task.FromResult(new List<Dish>
                {
                    new Dish("steak", DishType.entree, "night"),
                    new Dish("potato", DishType.side, "night"),
                    new Dish("cake", DishType.dessert, "night")
                }));

            _moqDishRepository.Setup
                (x => x.GetByTimeOfDayAndTypes("night", new List<DishType> { DishType.entree, DishType.side, DishType.drink }))
                .Returns(Task.FromResult(new List<Dish>
                {
                    new Dish("steak", DishType.entree, "night"),
                    new Dish("potato", DishType.side, "night"),
                    new Dish("wine", DishType.drink, "night")
                }));
            _moqDishRepository.Setup
                (x => x.GetByTimeOfDayAndTypes("night", new List<DishType> { DishType.entree, DishType.entree, DishType.side, DishType.drink }))
                .Returns(Task.FromResult(new List<Dish>
                {
                    new Dish("steak", DishType.entree, "night"),
                    new Dish("potato", DishType.side, "night"),
                    new Dish("wine", DishType.drink, "night")
                }));
        }

        [Test]
        public void GetByTimeOfDayAndTypesValidations()
        {
            var dishService = new DishService(_moqDishRepository.Object);

            var exception = Assert.ThrowsAsync<BusinessValidationException>(async () => await dishService.GetByTimeOfDayAndTypes("test", "1,2,3"));
            Assert.AreEqual("dish.getByTimeOfDayAndTypes.invalidTimeOfDay", exception.Message);

            exception = Assert.ThrowsAsync<BusinessValidationException>(async () => await dishService.GetByTimeOfDayAndTypes("morning", null));
            Assert.AreEqual("dish.getByTimeOfDayAndTypes.invalidDishTypes", exception.Message);

            exception = Assert.ThrowsAsync<BusinessValidationException>(async () => await dishService.GetByTimeOfDayAndTypes("morning", string.Empty));
            Assert.AreEqual("dish.getByTimeOfDayAndTypes.invalidDishTypes", exception.Message);
        }

        [Test]
        public async Task GetByTimeOfDayAndTypes()
        {
            var dishService = new DishService(_moqDishRepository.Object);

            var result = await dishService.GetByTimeOfDayAndTypes("morning", "1,2,3");
            Assert.AreEqual("eggs, toast, coffee", result);

            result = await dishService.GetByTimeOfDayAndTypes("morning", "2,1,3");
            Assert.AreEqual("eggs, toast, coffee", result);

            result = await dishService.GetByTimeOfDayAndTypes("morning", "1,2,3,4");
            Assert.AreEqual("eggs, toast, coffee, error", result);

            result = await dishService.GetByTimeOfDayAndTypes("morning", "1,2,3,3,3");
            Assert.AreEqual("eggs, toast, coffee(x3)", result);

            result = await dishService.GetByTimeOfDayAndTypes("night", "1,2,3,4");
            Assert.AreEqual("steak, potato, wine, cake", result);

            result = await dishService.GetByTimeOfDayAndTypes("night", "1,2,2,4");
            Assert.AreEqual("steak, potato(x2), cake", result);

            result = await dishService.GetByTimeOfDayAndTypes("night", "1,2,3,5");
            Assert.AreEqual("steak, potato, wine, error", result);

            result = await dishService.GetByTimeOfDayAndTypes("night", "1,1,2,3,5");
            Assert.AreEqual("steak, error", result);
        }
    }
}
