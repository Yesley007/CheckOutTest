using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Checkout;
using Checkout.ApiServices.Charges.ResponseModels;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    public class BaseServiceTests
    {
        protected APIClient CheckoutClient;

        [SetUp]
        public void Init()
        {
            CheckoutClient = new APIClient(); 
        }
		 
        #region Protected methods
 
        /// <summary>
        ///     Creates a new charge with default card and new track id and asserts that is not declined
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        protected Charge CreateChargeWithNewTrackId()
        {
            string cardNumber;
            return CreateChargeWithNewTrackId(out cardNumber);
        }
 
        /// <summary>
        ///     Creates a new charge with default card and new track id and asserts that is not declined
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        protected Charge CreateChargeWithNewTrackId(out string cardNumber)
        {
            var cardCreateModel = TestHelper.GetCardChargeCreateModel(TestHelper.RandomData.Email);
            cardCreateModel.TrackId = "TRF" + Guid.NewGuid();
            var chargeResponse = CheckoutClient.ChargeService.ChargeWithCard(cardCreateModel);
 
            chargeResponse.Should().NotBeNull();
            chargeResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            chargeResponse.Model.Status.Should().NotBe("Declined");
 
            cardNumber = cardCreateModel.Card.Number;
            return chargeResponse.Model;
        }
 
        /// <summary>
        ///     Creates a new charge with provided card and new track id and asserts that is not declined
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="cvv"></param>
        /// <param name="expirityMonth"></param>
        /// <param name="expirityYear"></param>
        /// <returns></returns>
        protected Charge CreateChargeWithNewTrackId(string cardNumber, string cvv, string expirityMonth,
            string expirityYear)
        {
            var cardCreateModel = TestHelper.GetCardChargeCreateModel(TestHelper.RandomData.Email);
            cardCreateModel.TrackId = "TRF" + Guid.NewGuid();
            cardCreateModel.Card.Number = cardNumber;
            cardCreateModel.Card.Cvv = cvv;
            cardCreateModel.Card.ExpiryMonth = expirityMonth;
            cardCreateModel.Card.ExpiryYear = expirityYear;
            var chargeResponse = CheckoutClient.ChargeService.ChargeWithCard(cardCreateModel);
 
            chargeResponse.Should().NotBeNull();
            chargeResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            chargeResponse.Model.Status.Should().NotBe("Declined");
 
            return chargeResponse.Model;
        }
 
        #endregion
    }
}
