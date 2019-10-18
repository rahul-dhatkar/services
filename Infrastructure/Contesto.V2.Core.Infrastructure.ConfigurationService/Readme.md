
# Contesto.V2.Core.Infrastructure.ConfigurationService

This project was generated with [Visual Stdio](https://visualstudio.microsoft.com/vs/whatsnew/) version 2017.


## Release Notes

List of classes

- ConfigurationServiceExtension

- ConfigurationConfig

- ConfigurationFactory

- DbConfigurationManager

# Installation

From FULCRUM { Nuget Gallery } :

```csharp
Install-Package Contesto.V2.Core.Infrastructure.ConfigurationService
```

# Features

`Contesto.V2.Core.Infrastructure.ConfigurationService` is a NuGet library that you can add in to your ASP.NET Web API, Console Application, and Windows Service. 

This will enable below features
> Keep application configuration in database.

> Environment wise application settings

> Application settings are In-memory cached 


 ## Example - C# Implementation 

 > In ASP.NET Core 2.0 and above

### 1. Add below line of configuration settings in 

`appsettings.Development.json `

```csharp
  "ConfigurationConfig": {
    "DbConnectionString": "Data Source=localhost;Initial Catalog=YouDB;User ID=sa;Password=XXXXXX; Trusted_Connection = False; MultipleActiveResultSets = true",
    "RunTimeEnvironment": "Development",
    "JsonFilePath": null
  }
```
`appsettings.Qa.json`

```csharp
  "ConfigurationConfig": {
    "DbConnectionString": "Data Source=localhost;Initial Catalog=YouDB;User ID=sa;Password=XXXXXX; Trusted_Connection = False; MultipleActiveResultSets = true",
    "RunTimeEnvironment": "Qa",
    "JsonFilePath": null
  }
```

`appsettings.Production.json`

```csharp
  "ConfigurationConfig": {
    "DbConnectionString": "Data Source=localhost;Initial Catalog=YouDB;User ID=sa;Password=XXXXXX; Trusted_Connection = False; MultipleActiveResultSets = true",
    "RunTimeEnvironment": "Production",
    "JsonFilePath": null
  }
```
### 2. Add below line of code in your `StartUp.cs` file 

```csharp
public IServiceProvider ConfigureServices(IServiceCollection services)
    {
        services.AddDbConfigurationService(Configuration);
    }
```

`NOTE`:- No need to add above line of code If your are using `Contesto.V2.Core.Common.Api` Nuget package. 

### 3. In Web Api controller  

```csharp
using System.Linq;
using Contesto.V2.Core.Infrastructure.ConfigurationService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FrameworkOne.StartupKit.Api.Controllers
{
    /// <summary>
    /// ConfigurationService Controller
    /// </summary>
    /// <seealso cref="Controller" />
    public class ConfigurationServiceController :  Controller
    {
        private IDbConfigurationManager _dbConfigurationManager;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationServiceController"/> class.
        /// </summary>
        /// <param name="dbConfigurationManager">The database configuration manager.</param>
        public ConfigurationServiceController(IDbConfigurationManager dbConfigurationManager)
        {
            _dbConfigurationManager = dbConfigurationManager;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Get()
        {
            var allAppSettings = _dbConfigurationManager.GetAllValues();
            return Ok(allAppSettings.ToList());
        }

        /// <summary>
        /// Gets the specified application key.
        /// </summary>
        /// <param name="appKey">The application key.</param>
        /// <returns></returns>
        public ActionResult Get(string appKey)
        {
            var appSetting = _dbConfigurationManager.AppSettings(appKey);
            return Ok(appSetting);
        }
    }
}

```


# Database Scripts

Create below table and store procedure in your project database to support the configuration

## Example - `SQL Server`

### 1. Table

` ConfigurationSettings`

| Column Name  | DataType | Default Value | Allow Null|
| ------------- | ------------- | -------------|-------------|  
| Id  | int  |Identity (Auto Increment)|No|
| Environment  | varchar(10)   ||No|
| Key  | varchar(20)  ||No|
| Value  | varchar(MAX)  ||Yes|
| CreatedBy  | varchar(50)  ||No|
| CreatedDateTime  | datetime  |GETDATE()|No|
| ModifiedBy  | varchar(50)  ||Yes|
| ModifiedDateTime  | datetime  ||Yes|
| IsActive  | bit  |1|No|
| IsDeleted  | bit  |0|No|


### 2. Store Procedure 

> `GetAllConfigurationSettings`

```SQL
/*============================================================================= 
Name: [dbo].[GetAllConfigurationSettings] 
-------------------------------------------------------------------------------
Author: Creator Name    Created Date 
        Dhiraj Gupta    06/27/2018
Purpose & Screen Name: 
To get Key, Value and Environment from  ConfigurationSettingss table
Description : Add description if any
 
-------------------------------------------------------------------------------
If you are modifying any procedure Add below information 
Modified By : Name    Modified Date 
Description : Write notes why you have modified any procedure 
===============================================================================

EXEC [dbo].[GetAllConfigurationSettings]

*/

CREATE PROCEDURE [dbo].[GetAllConfigurationSettings] (@SearchText VARCHAR(100))
AS
BEGIN
    SET NOCOUNT ON
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

    SELECT Environment
        ,[Key]
        ,[Value]
        ,IsActive
    FROM ConfigurationSettings WITH(NOLOCK)
    WHERE IsDeleted = 0
        AND (
            [Key] LIKE '%' + @SearchText + '%'
            OR @SearchText IS NULL
            )
END
```
