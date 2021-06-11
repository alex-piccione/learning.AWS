import json
  
def lambda_handler(event, context):
    #debugEvent(event)

    #.1 Parse the querystring parameters
    params = readParams(event)

    #.2 Construct the body of the response
    responseBody = {
        "transactionId": params["transactionId"],
        "transactionType": params["transactionType"],
        "transactionAmount": params["transactionAmount"],
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


def readParams(event):
    if not ("queryStringParameters" in event):
        raise Exception("Querystring is missed")
    
    params = event["queryStringParameters"]

    return {
        "transactionId": readParam(params, "transactionId"),
        "transactionType": readParam(params, "type"),
        "transactionAmount": readParam(params, "amount")
    }


def readParam(params, paramName):
    if paramName in params: return params[paramName]
    else: raise Exception(f"{paramName} in Querystring is missed")


def debugEvent(event):
    print("debugEvent")
    for x in event:
        print(x)