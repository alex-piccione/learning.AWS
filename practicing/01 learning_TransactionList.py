import json

def createAsset(assetCode:str, amount:float):
    return {
        "asset": assetCode,
        "amount": amount
    }


def lambda_handler(event, context):
    #.1 Parse querystring parameters
    #params = event["queryStringParameters"]
    print("what does \"event\" contains?")
    for key, value in event:
        print(key, ":", value)
    print("parsed params")

    #.2 Create return list
    list = [
        createAsset("GBP", 100.00),
        createAsset("EUR", 1234.56),
        createAsset("YEN", 12000),
    ]

    return {
        'statusCode': 200,
        'body': json.dumps(list)
    }
