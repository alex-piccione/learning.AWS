module SmokeTests.Portfolio

open System
open System.Text
open System.Security.Cryptography
open NUnit.Framework
open FsUnit
open Flurl.Http
open System.Threading.Tasks

let SERVICE = ""

// AWS Signature Version 4

// https://docs.aws.amazon.com/general/latest/gr/sigv4-create-canonical-request.html
let calculateAuthorization method path querystring headers =

    // create canonical request
    (*
        CanonicalRequest =
            HTTPRequestMethod + '\n' +
            CanonicalURI + '\n' +
            CanonicalQueryString + '\n' +
            CanonicalHeaders + '\n' +
            SignedHeaders + '\n' +
            HexEncode(Hash(RequestPayload))
    *)

    //let hexEncode input = HexEncode 
    //String.Concat(hash.Select( fun b -> b.ToString("X2")))

    let url = System.Web.HttpUtility.UrlEncodeUnicode $"{secrets.url}/{path}"

    let hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secrets.secretKey))

    let canonicalRequest = $"{method}\n{url}\n{querystring}\n"


    ""


[<Test>]
let ``CreateFund`` () =

    let random = Random(System.DateTime.Now.Millisecond).Next()

    let authorization = calculateAuthorization "post" "learning/portfolio/fund" "" ""

    let data = (
               "FundName" = $"test-name-{random}",
               "FundCode" = $"test-code-{random}"
               )

    
    let response = $"https://{secrets.url}/learning/portfolio/fund".PostJsonAsync(data).Result
   
    response.StatusCode |> should equal 200

    let content = response.GetStringAsync().Result

    content |> should not' (be NullOrEmptyString)

    // https://2knxndownk.execute-api.eu-central-1.amazonaws.com/test/learning/portfolio/fund

    // secrets.accessKey, secrets.secretKey
    //let signer = AWSRequestSignerV4.Sign(request, SERVICE, )
