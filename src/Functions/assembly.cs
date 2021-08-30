using Amazon.Lambda.Core;
using System.Runtime.CompilerServices;
//using Amazon.Lambda.Serialization.SystemTextJson;

// ref: https://dzone.com/articles/under-the-hood-of-net-core-lambda-request

// AWS documentation suggests to use LambdaJsonSerializer because of better performance
// but it results deprecated because with some DEBUG session it return wrong casing JSON objects

//[assembly: LambdaSerializer(typeof(LambdaJsonSerializer))]


[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
[assembly: InternalsVisibleTo("Learning.UnitTests")]
[assembly: InternalsVisibleTo("Learning.IntegrationTests")]
