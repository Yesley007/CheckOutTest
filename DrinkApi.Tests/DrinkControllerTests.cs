using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrinkApi.Controllers;
using NUnit.Framework;
using DrinkApi.Models;
using FluentAssertions;

namespace DrinkApi.Tests
{
    [TestFixture]
    public class DrinkControllerTests
    {
        private DrinkController drinkController = new DrinkController();
        

        [OneTimeSetUp]
        public void Setup()
        {
            drinkController.InitialiseStock();
        }

  

        [Test]
        public void AddDrinkTest()
        {
            Drink drink = new Drink()
            {
                DrinkCount = 10,
                DrinkName = "test2"
            };

            Result result = drinkController.AddDrink(drink);
            result.Should().NotBeNull();
            result.ResultCode.Should().Equals(ResultCode.OK);
        }

        [Test]
        public void GetDrinkAllDrinkTest()
        {
            Result result = drinkController.Get();
            result.Should().NotBeNull();
            result.ResultCode.Should().Equals(ResultCode.OK);
            result.Exception.Should().BeNull();
            result.DrinkList.Should().NotBeNull();
            result.drink.Should().BeNull();
        }

        [Test]
        public void GetDrinkByName()
        {
            Result result = drinkController.Get("a");
            result.Should().NotBeNull();
            result.ResultCode.Should().Equals(ResultCode.OK);
            result.Exception.Should().BeNull();
            result.DrinkList.Should().BeNull();
            result.drink.Should().NotBeNull();
            result.drink.DrinkName.Should().Equals("a");
            result.drink.DrinkCount.Should().Equals("15");
        }

        [Test]
        public void CustomDrinkSearch()
        {
            Search searchCriteria = new Search() {
                PageSize = 5,
                PageNumber = 1
            };
            Result result = drinkController.Get(searchCriteria);
            result.Should().NotBeNull();
            result.ResultCode.Should().Equals(ResultCode.OK);
            result.Exception.Should().BeNull();
            result.DrinkList.Should().NotBeNull();
            result.drink.Should().BeNull();
            result.DrinkList.Count.Should().Equals(5);
        }

        [Test]
        public void DeleteDrink()
        {
            Result result = drinkController.DeleteDrink("asdad");
            result.Should().NotBeNull();
            result.ResultCode.Should().Equals(ResultCode.NOT_FOUND);
        }

        [Test]
        public void UpdateDrink()
        {
            Drink drink = new Drink { DrinkName = "a", DrinkCount = 15 };
            Result result = drinkController.UpdateDrink(drink);
            result.Should().NotBeNull();
            result.ResultCode.Should().Equals(ResultCode.OK);
            result.Exception.Should().BeNull();
            result.DrinkList.Should().BeNull();
            result.drink.Should().BeNull();

            result = drinkController.Get("a");
            result.Should().NotBeNull();
            result.ResultCode.Should().Equals(ResultCode.OK);
            result.Exception.Should().BeNull();
            result.DrinkList.Should().BeNull();
            result.drink.Should().NotBeNull();
            result.drink.DrinkName.Should().Equals("a");
            result.drink.DrinkCount.Should().Equals("30");
        }

    }
}
