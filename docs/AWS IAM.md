# AWS IAM

When you create a newq Lambda function it has to be associated to a Role.  
By default a new Role is created with basic permissions to write logs that will be available in Cloudwatch. 

The generate Role has a name that contains the function name.  
Under "Permissionds" it has 1 policy named "AWSLambdaBasicExecutionRole-434a80c0-365d-4ba4-affc-b1bd35298089".  
This policy contains only one service: "CloudWatch Logs" with Access Level: "Write" (and Resources: "Multiple").

In this way every function will generate a new Role while probably we want to reuse a previous created one.  

This is the Policy JSON:
```json
{
    "Version": "2012-10-17",
    "Statement": [
        {
            "Effect": "Allow",
            "Action": "logs:CreateLogGroup",
            "Resource": "arn:aws:logs:eu-central-1:15100000046:*"
        },
        {
            "Effect": "Allow",
            "Action": [
                "logs:CreateLogStream",
                "logs:PutLogEvents"
            ],
            "Resource": [
                "arn:aws:logs:eu-central-1:1500000046:log-group:/aws/lambda/Portfolio_UploadedImages:*"
            ]
        }
    ]
}
```

The Policy "AWSLambdaBasicExecutionRole" is this:
```json
{
    "Version": "2012-10-17",
    "Statement": [
        {
            "Effect": "Allow",
            "Action": [
                "logs:CreateLogGroup",
                "logs:CreateLogStream",
                "logs:PutLogEvents"
            ],
            "Resource": "*"
        }
    ]
}
```
With a very clear description: "Provides write permissions to CloudWatch Logs".  