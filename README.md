# study.AWS
Learn AWS Lambda, Api Gateway, Serverless, CD/CI deployment

## Courses

- Pluralsight "Fundamental Cloud Concepts for AWS" by David Tucker
- content.aws.training "Introduction to Serverless Development"
- amazon.qwiklabs.com
  + Security on AWS

https://amazon.qwiklabs.com/quests/22?catalog_rank=%7B%22rank%22%3A1%2C%22num_filters%22%3A0%2C%22has_search%22%3Afalse%7D
https://amazon.qwiklabs.com/my_learning
https://amazon.qwiklabs.com/focuses/10383?parent=catalog
https://aws.amazon.com/training/self-paced-labs/?src=aws_training_email&ref_=pe_2254020_154131260

## Stuff covered
- Create an AWS Account
- Create Budget
  Monthly budget (repeated) with max 10 GBP
- AWS Regions, Avalilability Zones and Edge Locations (Amazon Cloudfront) ([here](docs/aws-global-infrastructure.md))  
- Create a shared role for Lambda functions
- Create a Lambda function (with Python 3.8)
  By default a new role is created with basic permission (Machine Policy, CloudWatch to write log)
- TODO: Create a Lamda function (with C#)




## Portfolio service

[![Portfolio deploy master](https://github.com/alex-piccione/learning.AWS/actions/workflows/main.yml/badge.svg)](https://github.com/alex-piccione/learning.AWS/actions/workflows/main.yml)

Region: eu-central-1 (Frankfurt)
Role: learning
Bucket for data: learning.portfolio


## Deplot C# Lamda
https://docs.aws.amazon.com/lambda/latest/dg/csharp-package-cli.html  
Assume you have the src/myHandler.cs file.  
``dotnet lambda``