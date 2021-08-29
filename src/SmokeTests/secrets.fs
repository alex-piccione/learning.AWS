﻿module secrets

open Microsoft.Extensions.Configuration


let configuration =
    ConfigurationBuilder()
        .AddUserSecrets("learning.026d69da-e3fc-4abe-a3f4-068da978c308")
        .Build()

let loadSecrets path =
    match configuration.[path] with
    | null -> failwith $"""Secret with path "{path}" is null."""
    | value ->  value

let url =       loadSecrets "AWS:url aaa"
let accessKey = loadSecrets "AWS:access key"
let secretKey = loadSecrets "AWS:secret key"
