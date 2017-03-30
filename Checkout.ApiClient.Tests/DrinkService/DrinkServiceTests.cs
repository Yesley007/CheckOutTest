using FluentAssertions;
using NUnit.Framework;
using Checkout.ApiServices.Drinks.RequestModels;

namespace Tests.DrinkService
{
    [TestFixture(Category = "DrinksApi")]
    class DrinkServiceTests : BaseServiceTests
    {
        [Test]
        public void AddDrink()
        {
            Drink drink = new Drink {DrinkCount=2,DrinkName="Test" };
            var response = CheckoutClient.DrinkService.AddDrink(drink);
            response.Model.Result.Should().NotBeEmpty();
            response.Model.Result.Should().Equals("OK");

        }

        [Test]
        public void UpdateDrink()
        {
            Drink drink = new Drink { DrinkCount = 2, DrinkName = "Test" };
            var response = CheckoutClient.DrinkService.UpdateDrinkStock(drink);
            response.Model.Result.Should().NotBeEmpty();
            response.Model.Result.Should().Equals("OK");

        }

        [Test]
        public void Delete()
        {
            var response = CheckoutClient.DrinkService.DeleteDrink("Test");
            response.Model.Result.Should().NotBeEmpty();
            response.Model.Result.Should().Equals("OK");

        }
        [Test]
        public void GetDrink()
        {
            var response = CheckoutClient.DrinkService.GetDrink("Test");
            response.Model.Result.Should().NotBeEmpty();
            response.Model.Result.Should().Equals("NOT_FOUND");
            response.Model.Drink.Should().BeNull();
            response.Model.Exception.Should().Equals("Drink not present in shopping list");

        }

    }
}
