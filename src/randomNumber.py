import random

def get(events, context):
    number = int(random.random()*100)
    print(f"generate number: {number}")
    return number