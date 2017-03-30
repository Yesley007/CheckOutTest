using DrinkApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DrinkApi.Controllers
{
    [RoutePrefix("drinkapi")]
    public class DrinkController : ApiController
    {
        public static IList<string> _Drinks = new List<string>();
        public static Dictionary<string, int> _DrinksStock = new Dictionary<string, int>();


        // GET: api/Drink
        [Route("getAllDrink")]
        [HttpGet]
        public Result Get()
        {
            List<Drink> colDrink = new List<Drink>();
            foreach (var drink in _DrinksStock)
            {
                colDrink.Add(new Drink
                {
                    DrinkName = drink.Key,
                    DrinkCount = drink.Value
                });
            }

            return new Result {
                ResultCode = ResultCode.OK,
                DrinkList = colDrink
            };
        }

        [HttpGet]
        [Route("CustomSearch")]
        public Result Get([FromUri]Search searchCriteria)
        {
            List<Drink> colDrink = new List<Drink>();
            foreach (var drink in _DrinksStock)
            {
                colDrink.Add(new Drink
                {
                    DrinkName = drink.Key,
                    DrinkCount = drink.Value
                });
            }
            if (colDrink.Count < 1)
            {
                return new Result()
                {
                    ResultCode = ResultCode.NOT_FOUND,
                    Exception = "No drink present in shopping list !"
                };
            }

            if (colDrink.Count > 1 && !string.IsNullOrEmpty(searchCriteria.OrderBy) && !string.IsNullOrWhiteSpace(searchCriteria.OrderBy))
            {
                if (!string.IsNullOrEmpty(searchCriteria.SortDirection) && !string.IsNullOrWhiteSpace(searchCriteria.SortDirection) && "desc".Equals(searchCriteria.SortDirection.ToLower()))
                {
                    colDrink = colDrink.OrderByDescending(x => ("drinkname".Equals(searchCriteria.OrderBy.ToLower())
                    ? x.DrinkName :
                    x.DrinkCount.ToString())).ToList();
                }
                else
                {
                    colDrink = colDrink.OrderBy(x => ("drinkname".Equals(searchCriteria.OrderBy.ToLower())
                    ? x.DrinkName :
                    x.DrinkCount.ToString())).ToList();
                }
            }
            var num = (searchCriteria.PageNumber - 1) * searchCriteria.PageSize;
            var pagination = colDrink.Count > num ? true : false;
            if (colDrink.Count >= 1)
            {
                return new Result()
                {
                    ResultCode = ResultCode.OK,
                    DrinkList = colDrink.Skip(num).Take(searchCriteria.PageSize).ToList()
                };
            }

            return new Result()
            {
                ResultCode = ResultCode.NOT_FOUND
            };
        }

        // GET: api/Drink/5
        [Route("getDrink/{name}")]
        [HttpGet]
        public Result Get(string name)
        {
            if (_Drinks.Contains(name.ToLower()))
            {
                return new Result
                {
                    ResultCode = ResultCode.OK,
                    drink = new Drink()
                    {
                        DrinkName = name,
                        DrinkCount = _DrinksStock[name]
                    }
                };
            }
            else
            {
                return new Result
                {
                    ResultCode = ResultCode.NOT_FOUND,
                    Exception = "Drink not present in shopping list"
                };
            }
        }

        // POST: api/Drink
        [Route("addDrink")]
        [HttpPost]
        public Result AddDrink(Drink drink)
        {
            if (drink.DrinkCount < 1)
            {
                return new Result
                {
                    ResultCode = ResultCode.INVALID_REQUEST,
                    Exception = "Drink count should be at least 1"
                };
            }
            else
            {
                if (!_Drinks.Contains(drink.DrinkName.ToLower()))
                {
                    _Drinks.Add(drink.DrinkName.ToLower());
                    if (drink.DrinkCount == 0)
                    {
                        _DrinksStock.Add(drink.DrinkName.ToLower(), 1);
                    }
                    else
                    {
                        _DrinksStock.Add(drink.DrinkName.ToLower(), drink.DrinkCount);
                    }
                }
                else
                {
                    int initialStock = _DrinksStock[drink.DrinkName.ToLower()];
                    _DrinksStock[drink.DrinkName.ToLower()] = initialStock + drink.DrinkCount;
                }
                return new Result
                {
                    ResultCode = ResultCode.OK
                };
            }
        }

        // PUT: api/Drink/5
        [Route("UpdateDrinkStock")]
        [HttpPut]
        public Result UpdateDrink(Drink drink)
        {
            if (!_Drinks.Contains(drink.DrinkName.ToLower()))
            {
                return new Result
                {
                    ResultCode = ResultCode.NOT_FOUND,
                    Exception = "Drink not present in shopping list"
                };
            }
            int initialStock = _DrinksStock[drink.DrinkName.ToLower()];
            _DrinksStock[drink.DrinkName.ToLower()] = initialStock + drink.DrinkCount;

            if (_DrinksStock[drink.DrinkName.ToLower()] < 1)
            {
                _Drinks.Remove(drink.DrinkName.ToLower());
                _DrinksStock.Remove(drink.DrinkName.ToLower());
            }

            return new Result
            {
                ResultCode = ResultCode.OK
            };
        }

        // DELETE: api/Drink/5
        [HttpDelete]
        [Route("deleteDrink/{drinkName}")]
        public Result DeleteDrink(string drinkName)
        {
            if (_Drinks.Contains(drinkName.ToLower()))
            {
                _Drinks.Remove(drinkName.ToLower());
                _DrinksStock.Remove(drinkName.ToLower());
                return new Result
                {
                    ResultCode = ResultCode.OK
                };
            }
            else
            {
                return new Result { ResultCode=ResultCode.NOT_FOUND, Exception="Drink Not Found !" };
            }
        }
        
        /** 
         * Method i created to populate drink list
         * **/
        [Route("Initialise")]
        [HttpPost]
        public void InitialiseStock()
        {
            _Drinks.Add("a");
            _DrinksStock.Add("a",91);
            _Drinks.Add("b");
            _DrinksStock.Add("b", 82);
            _Drinks.Add("c");
            _DrinksStock.Add("c",73);
            _Drinks.Add("d");
            _DrinksStock.Add("d", 64);
            _Drinks.Add("e");
            _DrinksStock.Add("e", 55);
            _Drinks.Add("f");
            _DrinksStock.Add("f", 46);
            _Drinks.Add("g");
            _DrinksStock.Add("g", 37);
            _Drinks.Add("h");
            _DrinksStock.Add("h", 28);
            _Drinks.Add("i");
            _DrinksStock.Add("i", 19);
        }
    }
}
