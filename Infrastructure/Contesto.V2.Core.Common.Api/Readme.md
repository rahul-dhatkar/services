# Contesto.V2.Core.Common.Api

This project was generated with [Visual Stdio](https://visualstudio.microsoft.com/vs/whatsnew/) version 2017.


## Release Notes

List of Utilities classes

- BaseApiController

- Startup

# Installation

From FULCRUM { Nuget Gallery } :

```csharp
Install-Package Contesto.V2.Core.Common.Api
```

# Features

`Contesto.V2.Core.Common.Api` is a NuGet library that you can add in to your ASP.NET MVC and ASP.NET Web API. 

This will enable below features
> Provides information about the web hosting environment an application is running.

> Swagger UI ("https://swagger.io")  

> ConfigurationReader - Keep application configuration in database.

> NotificationService - Email notification engine along with email template builder.

> MemoryCache - In-memory cache engine

> Build-in Security
- CORS
- Anti Forgery Token (XSRF) - An attribute that causes validation of antiforgery tokens for all unsafe HTTP methods. An antiforgery token is required for HTTP methods other than GET, HEAD, OPTIONS, and TRACE.
- OAuth 2.0 Claim
- JwtBearerTokens validation

## - Startup 

#### 1. Configuration - It's readonly property which holds the App.config values.

```csharp
public IConfigurationRoot Configuration { get; }
```
### Example

```csharp
 var confValue = Configuration.GetConnectionString("DefaultConnection");
```
#### 2. BaseConfigureServices -  Bases the configure services.

```csharp
public void BaseConfigureServices(IServiceCollection services)
```

#### 3. BaseConfigure - Bases the configure. This method gets called by the runtime. Use this method to configure the HTTP request pipeline

```csharp
public void BaseConfigure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
```

### Example - Inherit your ASP.NET Web API Startup class form `Contesto.V2.Core.Common.Api.Base.Startup`

```csharp
 public class Startup : Contesto.V2.Core.Common.Api.Startup
    {
        public StartupKitApplicationStartup(IHostingEnvironment env) : base(env) { }

         public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            BaseConfigureServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            BaseConfigure(app, env, loggerFactory);
        }
    }
```
 
 ## - BaseApiController - It's abstract class and by default Authorize is on.


#### 1. UserId - It's readonly property.

```csharp
//Gets the id of the user.
protected string UserId { get; }
```

#### 2. UserName - It's readonly property.

```csharp
//Gets the name of the user.
protected string UserName { get; }
```
#### 3. Roles - It's readonly property.

```csharp
//Gets the users roles.
protected List<string> Roles { get; }
```

#### 3. GetValidationResult - Gets the validation result.

```csharp
 protected ValidationResult GetValidationResult(dynamic model, dynamic validator)
```

#### 4. BuildValidationErrorMessage - Builds the validation error message.

```csharp
 protected  List<ErrorModel> BuildValidationErrorMessage(ValidationResult validationResult)
```

### Example - Inherit your ASP.NET Web API Controller class form `Contesto.V2.Core.Common.Api.Base.BaseApiController`

```csharp

using System.Threading.Tasks;
using Contesto.V2.Core.Common.Api.Base;
using Contesto.V2.ManageStartupKit.Manager.Interfaces;
using Contesto.V2.ManageStartupKit.Manager.ViewModels;
using Contesto.V2.ManageStartupKit.Manager.ViewModelValidators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Contesto.V2.ManageStartupKit.Api.Controllers
{
    [Route("api/v1/startupKits/common")]
    public class StartupKitsController : BaseApiController
    {
        #region "Variables"

        private readonly IQueryStartupKitManagerService _queryMasterManagerService;
        private readonly ICommandStartupKitManagerService _commandMasterManagerService;

        #endregion

        #region "Constructor"
 
        public StartupKitsController(IQueryStartupKitManagerService queryMasterManagerService,
                                ICommandStartupKitManagerService commandMasterManagerService)
        {
            _queryMasterManagerService = queryMasterManagerService;
            _commandMasterManagerService = commandMasterManagerService;
        }
        #endregion

        #region "MasterApis"
 
        [HttpGet]
        public async Task<ActionResult> Get(int id)
        {
            var result = await _queryMasterManagerService.GetStartupKitById(id).ConfigureAwait(false);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Get(string searchText)
        {
            var result = await _queryMasterManagerService.GetStartupKits(searchText).ConfigureAwait(false);
            if (result != null && result.Success)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet, Route("SummaryByTypeId")]
        public async Task<ActionResult> GetByTypeId(StartupKitSummaryRequestViewModel model)
        {
            var result = await _queryMasterManagerService.GetStartupKitSummaryByTypeId(model).ConfigureAwait(false);

            if (result != null && result.Success)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] StartupKitViewModel model)
        {
            var results = GetValidationResult(model, new StartupKitViewModelValidation());
            if (!results.IsValid)
                return BadRequest(BuildValidationErrorMessage(results));

            model.CreatedBy = this.UserName;

            var result = await _commandMasterManagerService.CreateStartupKit(model).ConfigureAwait(false);
            if (result != null && result.Success)
            {
                return Accepted(result);
            }

            return BadRequest(result.ErrorMessages);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] StartupKitViewModel model)
        {
            var results = GetValidationResult(model, new StartupKitViewModelValidation());
            if (!results.IsValid)
                return BadRequest(BuildValidationErrorMessage(results));

            var result = await _commandMasterManagerService.UpdateStartupKit(model).ConfigureAwait(false);
            if (result != null && result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.ErrorMessages);
        }

        #endregion
    }
}
```

