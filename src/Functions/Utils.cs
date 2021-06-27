using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
                    : throw new Exception($@"QuesryString parameter ""{parameterName}"" with value ""{value}"" cannot be parsed to int.") 
                : default(int?);

        public static int GetIntFromQueryString(this APIGatewayProxyRequest request, string parameterName, int ifNull)
        => request.GetIntFromQueryString(parameterName) ?? ifNull;
    }
}
