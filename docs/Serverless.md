# Serverless

Serverless is a framework to facilitate the deployment of AWS Lamda.  
By code you can define the function, the events (that triggers teh function), the resources.  

Docs:
- https://www.serverless.com/framework/docs/providers/aws/guide/intro/


### GitHub Action step _serverless/github-action_
```yaml
# in jobs steps
    - name: Serverless deploy
      uses: serverless/github-action@master
      with: 
        args: deploy
      env: 
        AWS_ACCESS_KEY: ${{ secrets.AWS_ACCESS_KEY }}
```

On GitHub it uses a special step: _serverless/github-action@master_  
GitHub action find out the action needs some package and install it. 
"Build serverless/github-action@master"   
It executes some npm install steps and then ``prebuild-install || node-gyp rebuild``
It fails with this error: "gyp WARN EACCES current user ("nobody") does not have permission to access the dev dir "/root/.cache/node-gyp/14.17.0" " but it continue with some other commands and in the end Serverless Framework is installed successfully.  

### GitHub Action step _aws-actions/configure-aws-credentials_

I copied this from another online example:

```yaml
# in jobs steps
    - name: Configure AWS credentials
      uses: aws-actions/configure-aws-credentials@v1
      with: 
        aws-access-key-id: ${{ secrets.AWS_ACCESS_KEY }}
        aws-secret-access-key: ${{ secrets.AWS_ACCESS_KEY_SECRET }}
        aws-region: eu-central-1
```

What is the Access Key and its Secret ?  
Simply the Access Key and Secret Access Key of an AWS user.  
If missing it fails with " The security token included in the request is invalid." if there is no user set with the access key.  

In AWS IAM create a new user (example: "github-learning").  
Add it to a group (example "github-learning") with this policies:
- AWSLambda_FullAccess
- IAMFullAccess   WHY ???
- CloudWatchLogsFullAccess 
- AWSLambdaRole   WHY ???
- AWSCloudFormationFullAccess: it is the actual deployment of the Lambda function
- AmazonS3FullAccess: CloudFront "create Stack" step needs to create an S3 bucket

It failed on CloudFront "create Stack" step:
> Serverless: Creating Stack...
> Serverless: Checking Stack create progress...
> An error occurred: ServerlessDeploymentBucket - API: s3:CreateBucket Access Denied.

So it needs permisisons to create an S3 bucket.  

_serverless.yml_
```yaml
provider:
  name: aws
  runtime: python3.8 
  stage: test
  region: eu-central-1
```

_2021-06-14_
_python3.9_ still not supported, _dotnetcore2.1_ is the only dotnet supported.

## Packaging
https://www.serverless.com/framework/docs/providers/aws/guide/packaging/

By default it copy everything in the root folder (apart fro predefined ignored files).  
I tried to use "patterns" to exclude what is not meant to be packaged but it didn't worked.
```yaml
package:
  patterns:
    - "!**"
    - "src/**"
```

```yaml
package:
  individually: true
```