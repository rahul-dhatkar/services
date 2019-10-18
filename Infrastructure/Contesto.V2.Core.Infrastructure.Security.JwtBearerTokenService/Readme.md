# Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService
This project was generated with [Visual Stdio](https://visualstudio.microsoft.com/vs/whatsnew/) version 2017.

## Release Notes

List of classes
# Installation

From FULCRUM { Nuget Gallery } :

```csharp
Install-Package Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService
```

# Features

`Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService` is a NuGet library that you can add in to your ASP.NET MVC and ASP.NET Web API to enable token based authorization. 

 ## Example - C# Implementation 

 > In ASP.NET Core 2.0 and above

### 1. Add below line of code in your `StartUp.cs` file 

```csharp
public IServiceProvider ConfigureServices(IServiceCollection services)
    {
         services.AddJwtBearerIdentityService(Configuration);
    }

public void BaseConfigure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
		// Add below line before `app.UseMvc()`
		app.UseAuthentication();
		app.UseMvc();
    }

```

### 2. Add below line of code in your `Controller` file 

```csharp
	[Authorize]
	public class TestController : ControllerBase
    {
	}
```

`NOTE`:- No need to add above line of code If your are using `Contesto.V2.Core.Common.Api` Nuget package. 

# Database Scripts

Create below table and store procedure in your project database to support the configuration

## Example - `SQL Server`

### 1. Table

` UserTokens`

| Column Name  | DataType | Default Value | Allow Null|
| ------------- | ------------- | -------------|-------------|  
| Id  | int  |Identity (Auto Increment)|No|
| UserId  | varchar(MAX)   ||No|
| AccessToken  | datetimeoffset(7)  ||No|
| AccessTokenExpiresDateTime  | datetimeoffset(7)  ||No|
| RefreshToken  | varchar(MAX)  ||No|
| RefreshTokenSource  | varchar(MAX)  ||No|
| RefreshTokenExpiresDateTime  | datetimeoffset(7)  ||No|

### 2. Store Procedure 
> `CreateUserToken`

```SQL
/*============================================================================= 
Name: dbo.CreateUserToken
-------------------------------------------------------------------------------
Author: Dhiraj Gupta 
Date: 08/08/2018
Purpose & Screen Name: To insert into UserToken table
Description :  - 
-------------------------------------------------------------------------------
*/
CREATE PROCEDURE [dbo].[CreateUserToken] (
	@Json VARCHAR(MAX)
	,@InsertedId BIGINT OUTPUT
	)
AS
BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	BEGIN TRANSACTION
	BEGIN TRY
		DECLARE @CurrentUserTokenId INT = 0
		INSERT INTO dbo.UserTokens (
			 [UserId]
			,[AccessToken]
			,[AccessTokenExpiresDateTime]
			,[RefreshToken]
			,[RefreshTokenSource]
			,[RefreshTokenExpiresDateTime]
			) (
			SELECT JsonValuesForUserTokens.UserId
			,JsonValuesForUserTokens.AccessToken
			,JsonValuesForUserTokens.AccessTokenExpiresDateTime
			,JsonValuesForUserTokens.RefreshToken
			,JsonValuesForUserTokens.RefreshTokenSource
			,JsonValuesForUserTokens.RefreshTokenExpiresDateTime
			 FROM OPENJSON(@Json) WITH (
				UserId VARCHAR(MAX) N'strict $."UserId"'
				,AccessToken VARCHAR(MAX) N'strict $."AccessToken"'
				,AccessTokenExpiresDateTime DATETIMEOFFSET(7) N'strict $."AccessTokenExpiresDateTime"'
				,RefreshToken VARCHAR(MAX) N'strict $."RefreshToken"'
				,RefreshTokenSource VARCHAR(MAX) N'strict $."RefreshTokenSource"'
				,RefreshTokenExpiresDateTime DATETIMEOFFSET(7) N'strict $."RefreshTokenExpiresDateTime"'
				) AS JsonValuesForUserTokens
			)
		SET @CurrentUserTokenId = SCOPE_IDENTITY()
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
	END CATCH
END
SET @InsertedId = @CurrentUserTokenId
RETURN @InsertedId
```
> `UpdateToken`

```SQL
/*============================================================================= 
Name: dbo.UpdateUserToken
-------------------------------------------------------------------------------
Author: Dhiraj Gupta 
Date: 12/11/2018
Purpose & Screen Name: To insert into UserToken table
Description :  - 
-------------------------------------------------------------------------------
*/
ALTER PROCEDURE [dbo].[UpdateUserToken] (
	@Json VARCHAR(MAX)
	,@UpdatedId BIGINT OUTPUT
	)
AS
BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	BEGIN TRANSACTION

	BEGIN TRY
		DECLARE @UserId VARCHAR(MAX)
		SET @UserId = (SELECT UserId FROM OPENJSON(@Json) WITH (UserId  VARCHAR(MAX) N'strict $."UserId"'))
		UPDATE dbo.UserTokens
		SET 
			 [AccessTokenExpiresDateTime] = JsonValuesForUserTokens.AccessTokenExpiresDateTime,
			 [RefreshTokenExpiresDateTime] = JsonValuesForUserTokens.AccessTokenExpiresDateTime
			FROM OPENJSON(@Json) WITH (
				AccessTokenExpiresDateTime DATETIMEOFFSET(7) N'strict $."AccessTokenExpiresDateTime"'
			    ,RefreshTokenExpiresDateTime DATETIMEOFFSET(7) N'strict $."RefreshTokenExpiresDateTime"'
				) AS JsonValuesForUserTokens
			WHERE UserId = @UserId  
 
		SET @UpdatedId = 1
		COMMIT TRANSACTION
	END TRY

	BEGIN CATCH
		ROLLBACK TRANSACTION
		SET @UpdatedId = 0
	END CATCH
END

RETURN @UpdatedId
```
> `GetUserTokenById`

```SQL

CREATE PROCEDURE [dbo].[GetUserTokenById]
(
	@UserId VARCHAR(MAX)
)
AS
BEGIN
		SET NOCOUNT ON
		 SELECT 
		   [Id]
		  ,[UserId]
		  ,[AccessToken]
		  ,[AccessTokenExpiresDateTime]
		  ,[RefreshToken]
		  ,[RefreshTokenSource]
		  ,[RefreshTokenExpiresDateTime]
  FROM  [dbo].[UserTokens] WHERE UserId = UserId
   
END
```

> `DeleteUserToken`

```SQL

/*============================================================================= 
Name: [dbo].[DeleteUserToken] 
-------------------------------------------------------------------------------
Author: Creator Name		Created Date 
        DHIRAJ G			08/18/2018
Purpose & Screen Name 
Delete Case
Description : Add description if any
 
-------------------------------------------------------------------------------
*/
CREATE PROCEDURE [dbo].[DeleteUserToken] (
	@UserId						VARCHAR(MAX) NULL, 
	@RefreshToken				VARCHAR(MAX) NULL,
	@RefreshTokenSource			VARCHAR(MAX) NULL,
	@ResultStatus				BIT OUTPUT
	)
AS
BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	IF (@UserId IS NOT NULL)
	BEGIN
	  DELETE FROM UserTokens WHERE UserId = @UserId
	  SET @ResultStatus = 1
	END
	ELSE IF  (@RefreshToken IS NOT NULL)
	BEGIN
	  DELETE FROM UserTokens WHERE RefreshToken = @RefreshToken
	  SET @ResultStatus=1
	END
	ELSE IF (@RefreshTokenSource IS NOT NULL)
	BEGIN
		DELETE FROM UserTokens WHERE RefreshTokenSource = @RefreshTokenSource
		SET @ResultStatus=1
	END
	ELSE
	BEGIN
		DELETE FROM UserTokens WHERE RefreshTokenExpiresDateTime < GETUTCDATE()
		SET @ResultStatus=1

	END

	SELECT @ResultStatus

END
```