# checkout-net-library

### Requirements

.Net Framework 4.5 and later

### How to use the library

In order to use the Checkout .Net library you have two options:
- Install the library through Nuget. Search for the nuget package name **Checkout.APIClient** and install it.
- Alternatively, you can download the sourcode from our master branch and reference it in your solution.

After that add the library namespace **using Checkout;** in your code as below:   
```
using Checkout;
```

if you get class name conflicts please use namespace alias as example below:
```
using CheckoutEnvironment = Checkout.Helpers.Environment;
```

#### Configuration
You will be required to **set your secret key** when initialising a new **APIClient** instance. You will also have option for other configurations defined in **AppSettings** of the config file. If you prefer to use config file then you need to have the following configuration in your config file:

- **Checkout.SecretKey**: This is your api secret key 
- **Checkout.PublicKey**: This is your api public key 
- **Checkout.RequestTimeout**: Set your default number of seconds to wait before the request times out on the ApiHttpClient. Default is 60.
- **Checkout.MaxResponseContentBufferSize**: Sets the maximum number of bytes to buffer when reading the response. Default is 10240.
- **Checkout.DebugMode**: If set to true, the request and response result will be logged to console. Set this option to false when going Live. Default is false;
- **Checkout.Environment**: You can set your environment to point to **Sandbox** or **Live**.

```html
<appSettings>
    <add key="Checkout.SecretKey" value="sk_test_32b9cb39-1cd6-4f86-b750-7069a133667d" />
    <add key="Checkout.PublicKey" value="pk_test_2997d616-471e-48a5-ba86-c775ed3ac38a" />
    <add key="Checkout.RequestTimeout" value="60" />
    <add key="Checkout.MaxResponseContentBufferSize" value="10240" />
    <add key="Checkout.DebugMode" value="true" />
    <add key="Checkout.Environment" value="Sandbox" />
</appSettings>
```
#### Constructor configuration 
There are many constructors available for configuring the settings programmatically and you only need to do it once. If you provide settings from constructor it will override the matching setting in the config file otherwise the library will be looking for the settings in config file.

```html
APIClient()
APIClient(string secretKey, Environment env, bool debugMode, int connectTimeout)
APIClient(string secretKey, Environment env, bool debugMode)
APIClient(string secretKey, Environment env)
APIClient(string secretKey, bool debugMode)
APIClient(string secretKey)
```

#### Endpoints 
There are various API endpoints that the **APIClient** interacts with. 

- Charges
- Customers
- Cards
- Tokens

####Charges

#####Charge with card example
```
// Create payload
var cardChargeRequestModel = new CardCharge()
{
	Email = "myEmail@hotmail.com",
	AutoCapture = "Y",
	AutoCapTime = 0,
	Currency = "Usd",
	TrackId = "TRK12345",
	TransactionIndicator = "1",
	CustomerIp = "82.23.168.254",
	Description = "Ipad for Ebs travel",
	Value = "100",
	Card = new CardCreate()
	{
		ExpiryMonth = "06",
		ExpiryYear = "2018",
		Cvv = "100",
		Number = "4242424242424242",
		Name = "Mehmet Ali",
		BillingDetails = new Address()
		{
			AddressLine1 = "Flat 1",
			AddressLine2 = "Glading Fields",
			Postcode = "N16 2BR",
			City = "London",
			State = "Hackney",
			Country = "GB",
			Phone = new Phone()
			{
				CountryCode = "44",
				Number = "203 583 44 55"
			}
		}
	},
	Products = new List<Product>(){
		new Product{ 
			Name="ipad 3", 
			Price=100, 
			Quantity=1, 
			ShippingCost=10.5M, 
			Description="Gold edition", 
			Image="http://goofle.com/?id=12345", 
			Sku="TR12345", TrackingUrl="http://tracket.com?id=123456"
		}
	},
	ShippingDetails = new Address()
	{
		AddressLine1 = "Flat 1",
		AddressLine2 = "Glading Fields",
		Postcode = "N16 2BR",
		City = "London",
		State = "Hackney",
		Country = "GB",
		Phone = new Phone()
		{
			CountryCode = "44",
			Number = "203 583 44 55"
		}
	},
	Metadata = new Dictionary<string, string>() { { "extraInformation", "EBS travel" } },
	Udf1 = "udf1 string",
	Udf2 = "udf2 string",
	Udf3 = "udf3 string",
	Udf4 = "udf4 string",
	Udf5 = "udf5 string"
};

try
{
	// Create APIClient instance with your secret key
	APIClient ckoAPIClient = new APIClient("sk_test_32b9cb39-1cd6-4f86-b750-7069a133667d", Checkout.APIClient.Helpers.Environment.Sandbox);

	// Submit your request and receive an apiResponse
	HttpResponse<Charge> apiResponse = ckoAPIClient.ChargeService.ChargeWithCard(cardChargeRequestModel);

	if (!apiResponse.HasError)
	{
		// Access the response object retrieved from the api
		var charge = apiResponse.Model;
	}
	else
	{
		// Api has returned an error object. You can access the details in the error property of the apiResponse.
		// apiResponse.error
	}
}
catch (Exception e)
{
	//... Handle exception
}
```


####Customers
#####Create customer with card example
```
// Create payload
var customerCreateRequest = new CustomerCreate()
{
	Name = "Miss Matt Quigley",
	Description = "New customer created",
	Email = "myEmail1@hotmail.com",
	Phone =  new Phone()
			{
				CountryCode = "44",
				Number = "203 583 44 55"
			},
	Metadata = new Dictionary<string, string>() { { "Category", "UK customer" } },
	Card =  new CardCreate()
	{
		ExpiryMonth = "06",
		ExpiryYear = "2018",
		Cvv = "100",
		Number = "4242424242424242",
		Name = "Miss Matt Quigley",
		BillingDetails = new Address()
		{
			AddressLine1 = "Flat 1",
			AddressLine2 = "Glading Fields",
			Postcode = "N16 2BR",
			City = "London",
			State = "Hackney",
			Country = "GB",
			Phone = new Phone()
			{
				CountryCode = "44",
				Number = "203 583 44 55"
			}
		}
	}
};

try
{
	// Create APIClient instance with your secret key
	APIClient ckoAPIClient = new APIClient("sk_test_32b9cb39-1cd6-4f86-b750-7069a133667d", Checkout.APIClient.Helpers.Environment.Sandbox);

	// Submit your request and receive an apiResponse
	HttpResponse<Customer> apiResponse = ckoAPIClient.CustomerService.CreateCustomer(customerCreateRequest);

	if (!apiResponse.HasError)
	{
		// Access the response object retrieved from the api
		var customer = apiResponse.Model;
	}
	else
	{
		// Api has returned an error object. You can access the details in the error property of the apiResponse.
		// apiResponse.error
	}
}
catch (Exception e)
{
	//... Handle exception
}
```

####Cards
#####Create card
```
// Create payload
var cardCreateRequest = new CardCreate()
{
	ExpiryMonth = "06",
	ExpiryYear = "2018",
	Cvv = "100",
	Number = "4242424242424242",
	Name = "Miss Matt Quigley",
	BillingDetails = new Address()
	{
		AddressLine1 = "Flat 1",
		AddressLine2 = "Glading Fields",
		Postcode = "N16 2BR",
		City = "London",
		State = "Hackney",
		Country = "GB",
		Phone = new Phone()
		{
			CountryCode = "44",
			Number = "203 583 44 55"
		}
	}, 
	DefaultCard=true
};

try
{
	// Create APIClient instance with your secret key
	APIClient ckoAPIClient = new APIClient("sk_test_32b9cb39-1cd6-4f86-b750-7069a133667d", Checkout.APIClient.Helpers.Environment.Sandbox);

	// Submit your request and receive an apiResponse
	HttpResponse<Card> apiResponse = ckoAPIClient.CardService.CreateCard("cust_9DECF6A8-DBF7-46F3-927D-BA6C3CE1F501", cardCreateRequest);

	if (!apiResponse.HasError)
	{
		// Access the response object retrieved from the api
		var card = apiResponse.Model;
	}
	else
	{
		// Api has returned an error object. You can access the details in the error property of the apiResponse.
		// apiResponse.error
	}
}
catch (Exception e)
{
	//... Handle exception
}
```

####Tokens
#####Create payment token example

```
// Create payload
var paymentTokenRequest = new PaymentTokenCreate()
  {
	  Currency = "usd",
	  Value = "100",
	  AutoCapTime = 1,
	  AutoCapture = "N",
	  ChargeMode = 1,
	  Email = "myEmail1@hotmail.com",
	  CustomerIp = "82.23.168.254",
	  TrackId = "TRK12345", 
	  Description = "new payment token",
	  Products = new List<Product>(){
		new Product{ 
			Name="ipad 3", 
			Price=100, 
			Quantity=1, 
			ShippingCost=10.5M, 
			Description="Gold edition", 
			Image="http://goofle.com/?id=12345", 
			Sku="TR12345", TrackingUrl="http://tracket.com?id=123456"
		}
	},
	  ShippingDetails = new Address()
	  {
		  AddressLine1 = "Flat 1",
		  AddressLine2 = "Glading Fields",
		  Postcode = "N16 2BR",
		  City = "London",
		  State = "Hackney",
		  Country = "GB",
		  Phone = new Phone()
		  {
			  CountryCode = "44",
			  Number = "203 583 44 55"
		  }
	  },
	  Metadata = new Dictionary<string, string>() { { "extraInformation", "EBS travel" } },
	  Udf1 = "udf1 string",
	  Udf2 = "udf2 string",
	  Udf3 = "udf3 string",
	  Udf4 = "udf4 string",
	  Udf5 = "udf5 string"
  };

try
{
	// Create APIClient instance with your secret key
	APIClient ckoAPIClient = new APIClient("sk_test_32b9cb39-1cd6-4f86-b750-7069a133667d", Checkout.APIClient.Helpers.Environment.Sandbox);

	// Submit your request and receive an apiResponse
	HttpResponse<PaymentToken> apiResponse = ckoAPIClient.TokenService.CreatePaymentToken(paymentTokenRequest);

	if (!apiResponse.HasError)
	{
		// Access the response object retrieved from the api
		var paymentToken = apiResponse.Model;
	}
	else
	{
		// Api has returned an error object. You can access the details in the error property of the apiResponse.
		// apiResponse.error
	}
}
catch (Exception e)
{
	//... Handle exception
}
```

#####Verify charge example

```
// Create payload
string paymentToken = "pay_tok_e6ef69d3-11b2-473d-bdc0-6b03c8713454";

try
{
	// Create APIClient instance with your secret key
	APIClient ckoAPIClient = new APIClient("sk_test_32b9cb39-1cd6-4f86-b750-7069a133667d", Checkout.APIClient.Helpers.Environment.Sandbox);

	// Submit your request and receive an apiResponse
	HttpResponse<Charge> apiResponse = ckoAPIClient.ChargeService.VerifyCharge(paymentToken);

	if (!apiResponse.HasError)
	{
		// Access the response object retrieved from the api
		var charge = apiResponse.Model;
	}
	else
	{
		// Api has returned an error object. You can access the details in the error property of the apiResponse.
		// apiResponse.error
	}

}
catch (Exception e)
{
	//... Handle exception
}
```

### Debug Mode

If you enable debug mode by setting the **Checkout.DebugMode** to true in the config file or in code all the http requests and responses will be logged in the console.   

### Unit Tests

All the unit test written with NUnit and resides in the test project.
