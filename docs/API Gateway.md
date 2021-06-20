# API Gateway

When I was going to create a Gateway API I was asked to choose between
- HTTP API
- WebSocket API
- REST API
- REST API (private)

I'm not sure what is the difference between HTTP API and REST API. 

## HTTP API
Build low-latency and cost-effective REST APIs with built-in features such as OIDC and OAuth2, and native CORS support.
Works with the following:
Lambda, HTTP backends

## REST API
Develop a REST API where you gain complete control over the request and response along with API management capabilities.
Works with the following:
Lambda, HTTP, AWS Services


## API Proxy

When the API is deployed a URL of this type is created:  
https://***.execute-api.eu-central-1.amazonaws.com/{stage}/{resourge}?param1=100  
Every Resource and Action have to be mapped to the LAmda function.  

Using a Proxy ({Proxy+}) is possible to make the HTTP request pass through to the Lambda function (or an external endpoint) directly.
https://***.execute-api.eu-central-1.amazonaws.com/{stage}   
that goes directly to the wanted LAmbda function, without predefined mapping:   
https://***.execute-api.eu-central-1.amazonaws.com/{stage}/learning_getRandomNumber?param1=100  
