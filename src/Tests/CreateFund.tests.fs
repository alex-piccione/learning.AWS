module UnitTests.CreateFundFunction

open NUnit.Framework
open Foq
open FsUnit
open Amazon.Lambda.APIGatewayEvents
open Amazon.Lambda.Core
open Learning.Portfolio


[<Test>]
let ``CreateFund``() =

    let requestBody = CreateFundRequest()
    requestBody.Code <- "code"
    requestBody.Name <- "name"

    let context = Foq.Mock<ILambdaContext>().Create()
    let request = APIGatewayProxyRequest()
    request.Body <- Jil.JSON.Serialize(requestBody)

    let response = CreateFund().Handle(request, context)

    response |> should not' (be Null)
