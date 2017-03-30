using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace DrinkApi.Models
{
    public class Result
    {
        [JsonProperty(PropertyName = "Result")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ResultCode ResultCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Exception { get; set; }

        [JsonProperty(PropertyName = "Drink",NullValueHandling =NullValueHandling.Ignore)]
        public Drink drink { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Drink> DrinkList { get; set; }
    }

    public enum ResultCode
    {
        OK,
        INVALID_REQUEST,
        UNEXPECTED_ERROR,
        FAILED,
        NOT_FOUND,
    }
}