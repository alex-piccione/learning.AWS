module SmokeTests.Portfolio

open System
open NUnit.Framework
open FsUnit
open Flurl.Http

open AWSRequestSignerFlurl


[<Test>]
let ``CreateFund`` () =


    let random = Random(System.DateTime.Now.Millisecond).Next(1, 999)
    let json = $@"{{ ""name"": ""name-{random}"", ""code"": ""test-{random}"" }}"

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
