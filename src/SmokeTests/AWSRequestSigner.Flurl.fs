module AWSRequestSignerFlurl

open System
open System.Collections.Generic
open System.Text
open System.Web
open System.Linq
open System.Runtime.CompilerServices
open System.Security.Cryptography
open Flurl.Http

[<Extension>]
type Extensions() =
    [<Extension>]
    static member Sign(request:IFlurlRequest, method:string, content:string option, service:string, region:string) = 
        let url = request.Url.Path

        let querystring = request.Url.Query
        let content = content.Value

        if not(request.Headers.Contains("Host")) then
            request.Headers.Add("Host", request.Url.Host)

        let headers = request.Headers |> Seq.map (fun nv -> nv.ToTuple()) |> dict

        let generatedHeaders = AWSRequestSigner.createAuhtorizationHeaders(method, url, querystring, content, headers, service, region)
        request.WithHeaders(generatedHeaders)



// AWS Signature Version 4

// https://docs.aws.amazon.com/general/latest/gr/sigv4-create-canonical-request.html


let getCanonicalQueryParams(querystring:string) =

    let values = SortedDictionary<string, IEnumerable<string>>(StringComparer.Ordinal)

    let querystringData = HttpUtility.ParseQueryString(querystring)
    for key in querystringData.AllKeys do

        let escapedKey = Uri.EscapeDataString(key)
        let value = querystringData.[key]
        if value = null 
        then
            values.Add(escapedKey, [| $"{escapedKey}=" |])
        else
            // Handles multiple values per query parameter
            let queryValues = 
                value.Split(',')
                    // Order by value alphanumerically (required for correct canonical string)
                    .OrderBy(fun v -> v)
                    // Query params must be escaped in upper case (i.e. "%2C", not "%2c").
                    .Select(fun v -> $"{escapedKey}={Uri.EscapeDataString(v)}");

            values.Add(escapedKey, queryValues)

    let queryParams = values.SelectMany(fun a -> a.Value)
    String.Join("&", queryParams)

let getCanonicalHeaders headers =
    ("", "")

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

    let (canonicalHeaders, headersList) = getCanonicalHeaders(headers)

    let hashedPayload = hash()

    let canonicalRequest = $"{method}\n{url}\n{getCanonicalQueryParams(querystring)}\n{canonicalHeaders}\n{headersList}\n{hashedPayload}"

    ""