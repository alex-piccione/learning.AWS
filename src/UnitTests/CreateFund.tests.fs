namespace UnitTests

open NUnit.Framework
open Foq
open FsUnit
open Amazon.Lambda.APIGatewayEvents
open Amazon.Lambda.Core
open Learning.Portfolio


type ``CreateFund Function``() =

    let logger = Mock<ILambdaLogger>().Create()
    let context = Mock<ILambdaContext>()
                    .Setup(fun x -> <@ x.Logger @>).Returns(logger)
                    //.SetupByName("Logger").Returns(logger)
                    .Create()

    [<Test>]
    member test.``CreateFund OK``() =

        let requestBody = CreateFundRequest()
        requestBody.Code <- "code"
        requestBody.Name <- "name"

        let request = APIGatewayProxyRequest()
        request.Body <- Jil.JSON.Serialize(requestBody, Jil.Options.CamelCase) // lower camel case

        let response = CreateFund().Handle(request, context)

        response.StatusCode |> should equal 201


    [<Test>]
    member test.``CreateFund <when> JSON properties are Uppercase <should> fail with 400``() =

        let requestBody = $@"{{
            ""Name"": ""name test"",
            ""Code"": ""code test""
        }}"


        let request = APIGatewayProxyRequest()
    
        request.Body <- requestBody

        let response = CreateFund().Handle(request, context)

        response.StatusCode |> should equal 400

    [<Test>]
    member test.``CreateFund <when> Name is missing <should> fail with 400``() =

        let requestBody = $@"{{
            ""Code"": ""code test""
        }}"

        let request = APIGatewayProxyRequest()
    
        request.Body <- requestBody

        let response = CreateFund().Handle(request, context)

        response.StatusCode |> should equal 400

