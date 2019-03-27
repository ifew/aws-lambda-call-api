using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace call_rest_api
{
    public class Function
    {
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<string> FunctionHandler(ILambdaContext context)
        {
            string start_date="2002-01-12";
            string end_date="2002-01-15";
            string currency="USD";
            string baseUrl = "https://apigw1.bot.or.th/bot/public/Stat-ExchangeRate/v2/DAILY_AVG_EXG_RATE/?start_period="+start_date+"&end_period="+end_date+"&currency="+currency;

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true,
            };

            HttpClient client = new HttpClient(handler);
                
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("cache-control", "no-cache");
            client.DefaultRequestHeaders.Add("x-ibm-client-id", "807210b0-8679-49c7-a344-d307ad67563b");
            
            HttpResponseMessage res = await client.GetAsync(baseUrl);
            HttpContent content = res.Content;
            string data = await content.ReadAsStringAsync();
            
            return data;
        }

    }
}
