# Potman examples
(back to [README](../README.md))

URL base: https://at0feyo7s4.execute-api.eu-central-1.amazonaws.com/test/

## Portfolio.Fund

### Create
``POST <url base>/portfolio/fund``
```json
{
    "name": "Euro",
    "code": "EUR"
}
```
"id" is generated server side and returned

### Read
``GET <url base>/portfolio/fund?id=456789``


### List 
``GET <url base>/portfolio/fund``


### Delete
``DELETE <url base>/portfolio/fund?id=456789``
