# Contesto.V2.Core.Common.Utility

This project was generated with [Visual Stdio](https://visualstudio.microsoft.com/vs/whatsnew/) version 2017.


## Release Notes

List of Utilities classes

- PasswordHelper

- SecurityHelper

- QueueManager

- UtilityHelper

- ServiceCollectionExtensions

- IServiceFactory

# Installation

From FULCRUM { Nuget Gallery } :

```csharp
Install-Package Contesto.V2.Core.Common.Utility
```

# Features

`Contesto.V2.Core.Common.Utility` is a NuGet library that you can add in to your project for common Utility.

## - PasswordHelper - This is singleton  class

#### 1. Hash a password
```csharp
public string HashPassword(string password)
```


### Example

```csharp
 var hashedPassword = PasswordHelper.Instance.HashPassword("Password");
```

#### 2. Verify the hash password against the given password

```csharp
public bool VerifyPassword(string hashPassword, string password)
```


### Example

```csharp
 var isPasswordMatching = PasswordHelper.Instance.VerifyPassword("hashPassword", "Password");
```

## - SecurityHelper - This is singleton  class

#### 1. Encrypts the message
```csharp
public string EncryptMessage(string messageData, string securityKeySuffix)
```


### Example

```csharp
 var encryptedString = SecurityHelper.Instance.EncryptMessage("helloWorld", "securityKey");
```

#### 2. Decrypts the message

```csharp
public string DecryptMessage(string messageData, string securityKeySuffix)
```


### Example

```csharp
 var actualString = PasswordHelper.Instance.VerifyPassword("encryptedString", "securityKey");
```


## - QueueManager<T> - This is singleton class 

###  Represents a thread-safe first in-first out (FIFO) collection.


#### 1. Enqueues the specified model
```csharp
// Adds an object to the end of the System.Collections.Concurrent.ConcurrentQueue`1.
public void Enqueue(T model)
```


### Example

```csharp
 // Add TaskModel class in Queue
 QueueManager.Instance.Enqueue(new TaskModel(){ Id = 1, Name="Task1", TimeStamp= DateTime.Now});
```

#### 2. Dequeues the last model from queue.

```csharp
//Tries to remove and return the object at the beginning of the concurrent queue.
public T Dequeue()
```

### Example

```csharp
 var taskModel = QueueManager.Instance.Dequeue()
```
#### 3. Peek the last model from queue.

```csharp
//Tries to return an object from the beginning of the System.Collections.Concurrent.ConcurrentQueue`1  without removing it.
public T Peek()
```

### Example

```csharp
 var taskModel = QueueManager.Instance.Peek()
```
#### 4. Count return item count in queue.

```csharp
//Gets the number of elements contained in the System.Collections.Concurrent.ConcurrentQueue`1.
public int Count()
```

### Example

```csharp
 var itemCount = QueueManager.Instance.Count()
```

#### 4. Items return items in queue.

```csharp
public ConcurrentQueue<T> Items()
```

### Example

```csharp
 var items = QueueManager.Instance.Items()
```


## - ServiceCollectionExtensions - This is Extensions methods for  ServiceCollection class 

###  Specifies the contract for a collection of service descriptors. 
 `Dependency injection in ASP.NET Core` [Click]("https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1")


#### 1. AddSingletonFactory
```csharp
// Adds the singleton factory.
public static IServiceCollection AddSingletonFactory<T, TFactory>(this IServiceCollection collection)
            where T : class where TFactory : class, IServiceFactory<T>
```


### Example

```csharp
 // Add this line of code in Startup.cs class
 services.AddSingletonFactory<IDbConfigurationManager, ConfigurationFactory>();;
```

#### 2. AddTransientFactory
```csharp
// Adds the transient factory.
public static IServiceCollection AddTransientFactory<T, TFactory>(this IServiceCollection collection)
            where T : class where TFactory : class, IServiceFactory<T>
```

#### 3. AddScopedFactory
```csharp
// Adds the scoped factory.
 public static IServiceCollection AddScopedFactory<T, TFactory>(this IServiceCollection collection, TFactory factory)
            where T : class where TFactory : class, IServiceFactory<T>
```
