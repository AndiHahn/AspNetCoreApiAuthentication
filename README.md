# AspNetCoreApiAuthentication  

This project shows three versions of Web Api Authentication in a ASP .NET Core Web Project.  
- Api Key Authentication  
- Basic Authentication  
- Jwt Authentication  

## Project Setup  

The Project uses an SQL Database for Jwt Authentication. LocalDb is automatically created on project startup.  

## Api Key Authentication  

For Authentication a Request header must be provided with required api key.  

#### Implementation  
Authentication handlig is done in class "ApiKeyAuthenticationHandler" which is registered in startup class.  

#### Test with Postman  
Url: https://localhost:5001/api/ApiKeyAuth  
Add header to request: "x-api-key" : "123456789"  

## Basic Authentication  

Basic Authentication uses username and password, which are sent as Bases64 encoded header value.  

#### Implementation  
Authentication handlig is done in class "BasicAuthenticationHandler" which is registered in startup class.  

#### Test with Postman  
Url: https://localhost:5001/api/BasicAuth  
Select "Basic Auth" in Authorization Tab and use following login credentials:  
Username: "username"  
Password: "password"  

## Jwt Authentication  

For JWT Authentication a request on a authentication endpoint is required, in order to get a token, 
which can be used as Bearer token for all furher requests.  

#### Implementation  
Microsoft Identity Packages are used, which provide an IdentityDbContext to store required credentials to a database.  
In addition to authentication, the packages also provide functionality for authorization.  

#### Test with Postman  
##### Create user  
Url: https://localhost:5001/api/JwtAuth/create  
Json body:  
{
    "Username": "User",
    "Email": "user@email.com",
    "Password": "UserPassword0!"
}  

##### Create admin  
Url: https://localhost:5001/api/JwtAuth/createadmin  
Json body:  
{  
    "Username": "Admin",
    "Email": "admin@email.com",
    "Password": "AdminPassword0!"
}  

##### Authenticate  
Url: https://localhost:5001/api/JwtAuth/authenticate  
Json body:  
{  
    "Username": "Admin",
    "Password": "AdminPassword0!"
}  

##### Test endpoint with required user permission  
Url: https://localhost:5001/api/JwtAuthTest/userrole  
Select "Bearer Token" in Authorization Tab and past token, which is returned from authentication endpoint.  

##### Test endpoint with required admin permission  
Url: https://localhost:5001/api/JwtAuthTest/adminrole  
Select "Bearer Token" in Authorization Tab and past token, which is returned from authentication endpoint.  
