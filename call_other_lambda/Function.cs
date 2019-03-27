using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace call_other_lambda
{
    public class Function
    {
        public async Task<object> FunctionHandler(ILambdaContext context)
        {
            try
            {
                using (AmazonLambdaClient client = new AmazonLambdaClient(RegionEndpoint.APSoutheast1))
                {

                    var request = new Amazon.Lambda.Model.InvokeRequest
                    {
                        FunctionName = "test_call_rest_api",
                        Payload = ""
                    };

                    var response = await client.InvokeAsync(request);

                    using (var sr = new StreamReader(response.Payload))
                    {

                        var unit = JsonConvert.DeserializeObject(sr.ReadToEndAsync().Result,
                            new JsonSerializerSettings { 
                                NullValueHandling = NullValueHandling.Ignore 
                            });
                        
                        return unit;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }

}
