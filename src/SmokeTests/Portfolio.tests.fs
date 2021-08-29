module SmokeTests.Portfolio

open System
open NUnit.Framework
open FsUnit
open Flurl.Http

open AWSRequestSignerFlurl


[<Test>]
let ``CreateFund`` () =

    let random = Random(System.DateTime.Now.Millisecond).Next()
    let json = $@"{{ ""name"": ""name-{random}"", ""code"": ""code-123"" }}"

    // try use the HttpMessage
    (*
    let message = new HttpRequestMessage(HttpMethod.Post, $"https://{secrets.url}/portfolio/fund")
    message.Content <- new System.Net.Http.StringContent(json)

    let signedMessage = AWSRequestSigner.Sign(message, "execute-api", "eu-central-1", None)

    use client = new HttpClient()
    client.BaseAddress <- Uri($"https://{secrets.url}")
    let httpResponse = client.Send(message)

    let error = 
        if httpResponse.IsSuccessStatusCode then ""
        else $"{httpResponse.StatusCode} {httpResponse.ReasonPhrase} {httpResponse.Content.ReadAsStringAsync().Result}"
    *)

    let response = 
        $"https://{secrets.url}/portfolio/fund"
            .AllowAnyHttpStatus()
            .Sign("POST", Some(json), "execute-api", "eu-central-1")
            .PostStringAsync(json).Result

    let content = response.GetStringAsync().Result

    if response.StatusCode <> 201 
    then
        Assert.Fail(content)
    else
        content |> should not' (be NullOrEmptyString)
