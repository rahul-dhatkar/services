
# Contesto.V2.Core.Infrastructure.LoggerService

This project was generated with [Visual Stdio](https://visualstudio.microsoft.com/vs/whatsnew/) version 2017.


## Release Notes

List of classes

- LoggerExtensions

# Installation

From FULCRUM { Nuget Gallery } :

```csharp
Install-Package Contesto.V2.Core.Infrastructure.LoggerService
```

# Features

`Contesto.V2.Core.Infrastructure.LoggerService` is a NuGet library that you can add in to your ASP.NET MVC, ASP.NET Web API, Console Application, and Windows Service for application loggings. 

This will enable below features
> logger configuration in App.config.

> Environment wise logger settings


 ## Example 
 > In ASP.NET Core 2.0 and above

### 1. Add below line of configuration settings in 

`appsettings.Development.json `

```csharp
  "Logging": {
    "IncludeScopes": false,
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  }
```
`appsettings.Qa.json`

```csharp
  "Logging": {
    "IncludeScopes": false,
    "LogLevel": {
      "Default": "Information",
      "System": "Information",
      "Microsoft": "Information"
    }
  }
```

`appsettings.Production.json`

```csharp
 "Logging": {
    "IncludeScopes": false,
    "LogLevel": {
      "Default": "Error",
      "System": "Error",
      "Microsoft": "Error"
    }
  }
```
### 2. Add below line of code in your `StartUp.cs` file 

```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
          loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddContext(Infrastructure.LoggerService.Dtos.LoggerTypeEnum.Database, LogLevel.Information, Configuration.GetConnectionString("DefaultConnection"));
        
    }
```

`NOTE`:- No need to add above line of code If your are using `Contesto.V2.Core.Common.Api` Nuget package. 

### 3. In Web Api controller  

```csharp
using System.Linq;
using Contesto.V2.Core.Infrastructure.LoggerService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FrameworkOne.StartupKit.Api.Controllers
{
   using Contesto.V2.Core.Common.Api.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FrameworkOne.StartupKit.Api.Controllers
{
    /// <summary>
    /// CcnfigurationService Controller
    /// </summary>
    /// <seealso cref="Controller" />
    public class LoggerServiceController :  BaseApiController
    {
        private readonly ILogger<LoggerServiceController> _logger;
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerServiceController" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public LoggerServiceController(ILogger<LoggerServiceController>  logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Get()
        {
            _logger.LogCritical("LogCritical Message");
            _logger.LogError("LogError Message");
            _logger.LogInformation("LogInformation Message");
            _logger.LogTrace("LogTrace Message");
            _logger.LogWarning("LogWarning Message");
            return Ok();
        }
    }
}

```


# Database Scripts

Create below table and store procedure in your project database to support the configuration

## Example - `SQL Server`

### 1. Table

` Loggers`

| Column Name  | DataType | Default Value | Allow Null|
| ------------- | ------------- | -------------|-------------|  
| Id  | int  |Identity (Auto Increment)|No|
| EventId  | int   ||Yes|
| Message  | varchar(MAX)  ||No|
| InnerExceptionMessage  | varchar(MAX)  ||Yes|
| StackTrace  | varchar(MAX)  ||Yes|
| LogDateTime  | datetime  |GETDATE()|No|


### 2. Store Procedure 

`CreateLogger`

```SQL

/*============================================================================= 
Name: dbo.CreateLogger 
-------------------------------------------------------------------------------
Author: Dhiraj Gupta 
Date: 08/15/2018
Purpose & Screen Name: To insert into Document table
Description :  - 
 
-------------------------------------------------------------------------------
If you are modifying any procedure Add below information 
Modified By : Name    Modified Date 
Description : Write notes why you have modified any procedure 
=============================================================================== 


DECLARE @Json VARCHAR(MAX),
		@InsertedId bigint = 0
 SET @Json = 
 N'{
	"Id":0,
	"EventId":1,
	"LogLevel":"Information",
	"Message":"Request starting HTTP/1.1 POST http://localhost:20788/api/v1/accounts/login application/json-patch+json 89",
	"InnerExceptionMessage":"",
	"StackTrace":"",
	"LogDateTime":null
	}'

EXEC dbo.CreateLogger @Json, @InsertedId
*/

ALTER PROCEDURE [dbo].[CreateLogger] (
	@Json VARCHAR(MAX)
	,@InsertedId BIGINT OUTPUT
	)
AS
BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	BEGIN TRANSACTION

	BEGIN TRY
		DECLARE @CurrentLoggerId INT = 0

		SELECT JsonValuesForLoggers.EventId
			,JsonValuesForLoggers.LogLevel
			,JsonValuesForLoggers.[Message]
			,JsonValuesForLoggers.InnerExceptionMessage
			,JsonValuesForLoggers.StackTrace
			,GETDATE() 
			FROM OPENJSON(@Json) WITH (
				EventId INT N'strict $."EventId"'
				,LogLevel VARCHAR(15) N'strict $."LogLevel"'
				,[Message] VARCHAR(MAX) N'strict $."Message"'
				,InnerExceptionMessage VARCHAR(MAX) N'strict $."InnerExceptionMessage"'
				,StackTrace VARCHAR(MAX) N'strict $."StackTrace"'
				) AS JsonValuesForLoggers

		INSERT INTO dbo.Loggers (
			 EventId
			,LogLevel
			,[Message]
			,InnerExceptionMessage
			,StackTrace
			,LogDateTime
			) (
			SELECT JsonValuesForLoggers.EventId
			,JsonValuesForLoggers.LogLevel
			,JsonValuesForLoggers.[Message]
			,JsonValuesForLoggers.InnerExceptionMessage
			,JsonValuesForLoggers.StackTrace
			,GETDATE() 
			FROM OPENJSON(@Json) WITH (
				EventId INT N'strict $."EventId"'
				,LogLevel VARCHAR(50) N'strict $."LogLevel"'
				,[Message] VARCHAR(MAX) N'strict $."Message"'
				,InnerExceptionMessage VARCHAR(MAX) N'strict $."InnerExceptionMessage"'
				,StackTrace VARCHAR(MAX) N'strict $."StackTrace"'
				) AS JsonValuesForLoggers
			)

		SET @CurrentLoggerId = SCOPE_IDENTITY()

		COMMIT TRANSACTION
	END TRY

	BEGIN CATCH
		ROLLBACK TRANSACTION
	END CATCH
END

SET @InsertedId = @CurrentLoggerId

RETURN @InsertedId
```
