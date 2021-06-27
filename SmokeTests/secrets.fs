module secrets

open Microsoft.Extensions.Configuration

let configuration =
    ConfigurationBuilder()
        .AddUserSecrets("learning.026d69da-e3fc-4abe-a3f4-068da978c308")
        .Build()

let url =    configuration.["AWS:url"] 
let accessKey = configuration.["AWS:access key"]
let secretKey = configuration.["AWS:secret key"]