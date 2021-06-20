module Learning.Tests

open Amazon.Lambda.APIGatewayEvents
open Amazon.Lambda.Core

open NUnit.Framework;
open FsUnit;

open Learning.Portfolio


[<Test>]
let Get () =
    let func = RandomNumber()

    let max = 1

    let context = Foq.Mock<ILambdaContext>().Create()
    let request = APIGatewayProxyRequest()
    request.QueryStringParameters <- dict([ ("max", max.ToString()) ])

    let response = func.Handle(request, context)

    response.StatusCode |> should equal 200
    int(response.Body) |> should not' (be greaterThan max)