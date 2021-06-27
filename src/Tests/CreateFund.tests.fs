module UnitTests.CreateFundFunction

open NUnit.Framework
open Foq
open FsUnit
open Amazon.Lambda.APIGatewayEvents
open Amazon.Lambda.Core
open Learning.Portfolio


[<Test>]
let ``CreateFund OK``() =

    let requestBody = CreateFundRequest()
    requestBody.Code <- "code"
    requestBody.Name <- "name"

    let context = Foq.Mock<ILambdaContext>().Create()
    let request = APIGatewayProxyRequest()
    request.Body <- Jil.JSON.Serialize(requestBody, Jil.Options.CamelCase) // lower camel case

    let response = CreateFund().Handle(request, context)

    response.StatusCode |> should equal 204


[<Test>]
let ``CreateFund <when> JSON properties are Uppercase <should> fail``() =

    let requestBody = $@"{{
        ""Name"": ""name test"",
        ""Code"": ""code test""
    }}"

    let context = Foq.Mock<ILambdaContext>().Create()
    let request = APIGatewayProxyRequest()
    
    request.Body <- requestBody

    let response = CreateFund().Handle(request, context)

    response.StatusCode |> should equal 409

