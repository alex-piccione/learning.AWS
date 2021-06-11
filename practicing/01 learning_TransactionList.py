import json

def createAsset(assetCode:str, amount:float):
    return {
        "asset": assetCode,
        "amount": amount
    }


def lambda_handler(event, context):

    #.1 Parse querystring parameters
    params = extractQSParams(event) 

    #.2 Create return list and response body
    list = [
        createAsset("GBP", 100.00),
        createAsset("EUR", 1234.56),
        createAsset("YEN", 12000),
    ]

    body = {
        "params": {
            "from": params["from"],
            "to": params["to"],
        },
        "assets": list
    }

    return {
        'statusCode': 200,
        'body': json.dumps(body)
    }


def extractQSParams(event):
    if not ("queryStringParameters" in event):
        raise Exception("Querystring is missed")
    
    params = event["queryStringParameters"]

    return {
        "from": readQSParamOrDefault(params, "from", None),
        "to": readQSParamOrDefault(params, "to", None)
    }


def readQSParam(params, paramName, mandatory = True, default = None):
    if paramName in params: return params[paramName]
    else: raise Exception(f"{paramName} in Querystring is missed")

def readQSParamOrDefault(params, paramName, default):
    if paramName in params: return params[paramName]
    else: return default