module SmokeTests.Company

open System
open NUnit.Framework
open FsUnit
open Flurl.Http

open AWSRequestSignerFlurl


[<Test>]
let ``List All`` () =

    let response = 
        $"https://{secrets.url}/company/all"
            .AllowAnyHttpStatus()
            .Sign("GET", None, "execute-api", "eu-central-1")
            .GetAsync().Result

    let content = response.GetStringAsync().Result

    if response.StatusCode <> 200
    then
        Assert.Fail(content)
    else
        content |> should not' (be NullOrEmptyString)
