﻿module secrets

open Microsoft.Extensions.Configuration
open System


let configuration =
    ConfigurationBuilder()
        .AddUserSecrets("learning.026d69da-e3fc-4abe-a3f4-068da978c308")
        .Build()

let loadSecrets path env =
    match configuration.[path] with
    | null -> 
        match Environment.GetEnvironmentVariable(env, EnvironmentVariableTarget.Machine) with 
        | null -> failwith $"""Secret with path "{path}" and Environment variable "{env}" are null."""
        | value ->  value
    | value ->  value

let url =       loadSecrets "AWS:url" "URL"
let accessKey = loadSecrets "AWS:access key" "KEY"
let secretKey = loadSecrets "AWS:secret key" "SECRET"
