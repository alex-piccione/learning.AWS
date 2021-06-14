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
- AWSCloudFormationFullAccess