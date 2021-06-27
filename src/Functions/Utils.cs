using System;
using System.Collections.Generic;
using Amazon.Lambda.APIGatewayEvents;

namespace Learning
{
    internal static class APIGatewayProxyRequestExtensions
    {
        static IDictionary<string, string> emptyQueryString = new Dictionary<string, string>();

        public static IDictionary<string, string> GetQueryString(this APIGatewayProxyRequest request)
            => request.QueryStringParameters ?? emptyQueryString;

        public static int? GetIntFromQueryString(this APIGatewayProxyRequest request, string parameterName)
        => request.GetQueryString().TryGetValue(parameterName, out string value) 
                ? int.TryParse(value, out int result) 
                    ? result 
                    : throw new Exception($@"QueryString parameter ""{parameterName}"" with value ""{value}"" cannot be parsed to int.") 
                : default(int?);

        public static int GetIntFromQueryString(this APIGatewayProxyRequest request, string parameterName, int ifNull)
        => request.GetIntFromQueryString(parameterName) ?? ifNull;

        public static string GetIdFromQueryString(this APIGatewayProxyRequest request)
        => request.GetQueryString().TryGetValue("id", out string value)
            ? value
            : throw new Exception(@"QueryString parameter ""id"" not found");
    }
}
