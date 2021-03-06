# Learning AWS
Learn AWS Lambda, Api Gateway, Serverless, CD/CI deployment

## Courses

- Pluralsight "Fundamental Cloud Concepts for AWS" by David Tucker
- Pluralsight "Building Scalable APIs with the AWS API Gateway" by Will Button" [STARTED]
- content.aws.training "Introduction to Serverless Development"
- amazon.qwiklabs.com  
  + Security on AWS

https://amazon.qwiklabs.com/quests/22?catalog_rank=%7B%22rank%22%3A1%2C%22num_filters%22%3A0%2C%22has_search%22%3Afalse%7D
https://amazon.qwiklabs.com/my_learning
https://amazon.qwiklabs.com/focuses/10383?parent=catalog
https://aws.amazon.com/training/self-paced-labs/?src=aws_training_email&ref_=pe_2254020_154131260

TO see:
- *** https://github.com/awsdocs/aws-lambda-developer-guide/tree/main/sample-apps/blank-csharp
- https://github.com/aws/aws-lambda-dotnet#dotnet-cli-templates
- https://nodogmablog.bryanhogan.net/2021/03/c-and-aws-lambdas-part-6-net-5-inside-a-container-inside-a-lambda/

## Stuff covered
- Create an AWS Account
- Create Budget
  Monthly budget (repeated) with max 10 GBP
- AWS Regions, Avalilability Zones and Edge Locations (Amazon Cloudfront) ([here](docs/aws-global-infrastructure.md))  
- Create a shared role for Lambda functions
- Create a Lambda function (with Python 3.8)  
  By default a new role is created with basic permission (Machine Policy, CloudWatch to write log)
- Create a simple Lambda function with C#
- Deploy the Lambda within GitHub Action
- Serverless framework  
  Customized name for the API Gateway (default is {stage}-{service name})


## Portfolio service

### API
[![Portfolio deploy master](https://github.com/alex-piccione/learning.AWS/actions/workflows/main.yml/badge.svg)](https://github.com/alex-piccione/learning.AWS/actions/workflows/main.yml)

![image](https://user-images.githubusercontent.com/876490/130262878-d7d3383a-1592-4645-80fd-d88b0741865c.png)


Region: eu-central-1 (Frankfurt)
Role: learning
Bucket for data: learning.portfolio


## Postman
Postman calls examples here: [README.Postman](docs/README%20Postman.md)

---
# Developer resources & references

## Deploy C# Lambda
https://docs.aws.amazon.com/lambda/latest/dg/csharp-package-cli.html  
Assume you have the src/myHandler.cs file.  
``dotnet lambda``

## CORS
https://www.serverless.com/blog/cors-api-gateway-survival-guide

## Utils
``zip`` command: https://www.cyberciti.biz/faq/how-to-zip-a-folder-in-ubuntu-linux/

## GitHub Actions
variables: https://docs.github.com/en/actions/reference/context-and-expression-syntax-for-github-actions


## Severless
serverless.yml syntax: https://www.serverless.com/framework/docs/providers/aws/guide/serverless.yml/


## Configuration
The __configuration.json__ file inside "AWS Lambda" project contains info that cannot be stored in the code repository.  
During the deployment a precise step replace defined keys with the secrets stored in the deploy system (e.g. GitHub Actions).  

## Dependency Injection
I don't know the best way to make possible having dependency injection, for example to inject database providers.  
I saw some solutions but it seems overcomplicated.  
To solve the problem of having a parameterless constructor for the Lambda Functions and inject the moxked providers for test purpose
I'm using a default empty contructor and also one that accepts the provider(s) interface.  
When the parameterless contructor is used the code look at a configuration file and create the provider.  
