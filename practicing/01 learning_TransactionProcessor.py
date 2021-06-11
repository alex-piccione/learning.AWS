import json

print("Loading function")

def lambda_handler(event, context):
    #.1 Parse the querystring parameters
    transactionId = event["queryStringParameters"]["transactionId"]
    transactionType = event["queryStringParameters"]["type"]
    transactionAmount = event["queryStringParameters"]["amount"]
    print(f"transactionId: {transactionId}")

    #.2 Construct the body of the response
    responseBody = {
        "transactionId": transactionId,
        "transactionType": transactionType,
        "transactionAmount": transactionAmount,
        "message": "Hello from Lambda"
    }

    #.3 Return the HTTP response
    return {
        'statusCode': 200,
        "headers": [
            {"Content-Type": "application/json"}
        ],
        'body': json.dumps(responseBody)
    }
