namespace UnitTests

open System.Collections.Generic
open NUnit.Framework
open Foq
open FsUnit
open Amazon.Lambda.APIGatewayEvents
open Amazon.Lambda.Core
open Learning.Portfolio


type createRequest = { name:string; code: string }
    with member this.ToJson() = $@"{{ ""name"": ""{this.name}"", ""code"": ""{this.code}"" }}"

type ``CreateFund Function``() =

    let TEST_ID = "TEST"
    let logger = Mock<ILambdaLogger>().Create()
    let context = Mock<ILambdaContext>()
                    .Setup(fun x -> <@ x.Logger @>).Returns(logger)
                    //.SetupByName("Logger").Returns(logger)
                    .Create()

    let repository = Mock<IFundRepository>().Create()
                         //.Setup(fun x -> x.Create())

    [<Test>]
    member test.``CreateFund OK``() =

        let requestBody = CreateFundRequest()
        requestBody.Code <- "code"
        requestBody.Name <- "name"

        let request = APIGatewayProxyRequest()
        request.Body <- Jil.JSON.Serialize(requestBody, Jil.Options.CamelCase) // lower camel case

        // execute
        let response = CreateFund(repository).Handle(request, context)

        response.StatusCode |> should equal 201

        let json = System.Text.Json.JsonDocument.Parse(response.Body)
        json.RootElement.TryGetProperty("id") |> fun (t, _) -> t |> should be True
        json.RootElement.TryGetProperty("code") |> fun (t, _) -> t |> should be True

    [<Test>]
    member test.``CreateFund <when> JSON properties are Uppercase <should> fail with 400``() =

        let requestBody = $@"{{
            ""Name"": ""name test"",
            ""Code"": ""code test""
        }}"


        let request = APIGatewayProxyRequest()
    
        request.Body <- requestBody

        let response = CreateFund(repository).Handle(request, context)

        response.StatusCode |> should equal 400

    [<Test>]
    member test.``CreateFund <when> Name is missing <should> fail with 400``() =

        let requestBody = $@"{{
            ""Code"": ""code test""
        }}"

        let request = APIGatewayProxyRequest()
    
        request.Body <- requestBody

        let response = CreateFund(repository).Handle(request, context)

        response.StatusCode |> should equal 400

    [<Test>]
    member test.``CreateFund <when> Name is duplicated <should> fail with 400``() =

        let name = "Test"
        let requestBodyold = $@"{{
            ""Name"": ""{name}"",
            ""Code"": ""code test""
        }}"
        let requestBody = { name = name; code = "code"}.ToJson()

        let existingFund = [|Fund("", name, "OLD")|] :> ICollection<Fund>
        let repository = Mock<IFundRepository>()
                            .Setup( fun rep -> <@ rep.List() @>).Returns(existingFund)
                            .Create()

        let request = APIGatewayProxyRequest()

        request.Body <- requestBody

        let response = CreateFund(repository).Handle(request, context)

        response.StatusCode |> should equal 400
        response.Body |> should contain "already exists"

