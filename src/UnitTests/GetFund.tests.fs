namespace UnitTests

open NUnit.Framework
open Foq
open FsUnit
open Amazon.Lambda.APIGatewayEvents
open Amazon.Lambda.Core
open Learning.Portfolio
open System.Text.Json


type ``GetFund Function``() =

    let TEST_ID = "TEST"
    let logger = Mock<ILambdaLogger>().Create()
    let context = Mock<ILambdaContext>()
                    .Setup(fun x -> <@ x.Logger @>).Returns(logger)
                    //.SetupByName("Logger").Returns(logger)
                    .Create()

    let repository = Mock<IFundRepository>().Create()
                         //.Setup(fun x -> x.Create())

    [<Test>]
    member test.``GetFund <when> the record exists <should> return the record``() =

        let repository = 
            Mock<IFundRepository>()
                .Setup(fun rep -> <@ rep.Get(TEST_ID) @>).Returns(Fund(TEST_ID, "name", "code"))
                .Create()

        let request = APIGatewayProxyRequest()
        request.PathParameters <- dict ["id", TEST_ID]

        // execute
        let response = GetFund(repository).Handle(request, context)

        response.StatusCode |> should equal 200

        let options = JsonSerializerOptions()
        options.PropertyNameCaseInsensitive <- true 
        let fund = JsonSerializer.Deserialize<Fund>(response.Body, options)
        fund.Id |> should equal TEST_ID
        fund.Code |> should equal "code"
        fund.Name |> should equal "name"

    [<Test>]
    member test.``GetFund <when> the record does not exists <should> return 204 - empty``() =

        let request = APIGatewayProxyRequest()
        request.PathParameters <- dict ["id", TEST_ID]

        // execute
        let response = GetFund(repository).Handle(request, context)

        response.StatusCode |> should equal 204
        response.Body |> should be NullOrEmptyString
