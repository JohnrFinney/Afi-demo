# Afi-demo


Using Postman, create a new POST request with the following URL: https://localhost:7054/demo


There is an optional parameter appended at the end of the URL and setting this to true will result in receiving multiple errors (if any) within the 
ISO standardized exception details and data envelope.

Raw Json body is:
{
  "firstName": "string",
  "surname": "string",
  "referenceNumber": "XX-888888",
  "dateOfBirth": "2000-03-03",
  "email": "aa@b.com"
}


There is an exported v2.1 Postman collection (AFI Postman Collection_v2.1.json) in the same folder as the VS solution so importing this should work when the 
API is running.


Connecting to a small Azure SQL Server DB and also deployed this API to the following destination (with Swagger enabled):
https://ukw-api-afi-demo-01-csbxeufde8d3dnan.ukwest-01.azurewebsites.net/

swagger....
https://ukw-api-afi-demo-01-csbxeufde8d3dnan.ukwest-01.azurewebsites.net/swagger

I hope this helps...   :-)

