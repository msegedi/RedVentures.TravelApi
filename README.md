# RV Coding Challenge Submission

## Requirements

* VS 2017 or the latest version of the .NET Core SDK (for .NET Core 1.1).
* SQL Server 2014+. I'm not using any specific features of SQL Server 2014, so SQL Server Express should work.

## Running the application

* Database updates can be easily deployed via the RedVentures.TravelApi.Database project, but it might be faster to run the following SQL for initial setup:
```
USE [master]
GO
/****** Object:  Database [RedVentures.TravelApi]    Script Date: 7/12/2017 8:14:11 PM ******/
CREATE DATABASE [RedVentures.TravelApi]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RedVentures.TravelApi', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQL2014\MSSQL\DATA\RedVentures.TravelApi.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'RedVentures.TravelApi_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQL2014\MSSQL\DATA\RedVentures.TravelApi_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [RedVentures.TravelApi] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RedVentures.TravelApi].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RedVentures.TravelApi] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [RedVentures.TravelApi] SET ANSI_NULLS ON 
GO
ALTER DATABASE [RedVentures.TravelApi] SET ANSI_PADDING ON 
GO
ALTER DATABASE [RedVentures.TravelApi] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [RedVentures.TravelApi] SET ARITHABORT ON 
GO
ALTER DATABASE [RedVentures.TravelApi] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RedVentures.TravelApi] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RedVentures.TravelApi] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RedVentures.TravelApi] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RedVentures.TravelApi] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [RedVentures.TravelApi] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [RedVentures.TravelApi] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RedVentures.TravelApi] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [RedVentures.TravelApi] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RedVentures.TravelApi] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RedVentures.TravelApi] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RedVentures.TravelApi] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RedVentures.TravelApi] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RedVentures.TravelApi] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RedVentures.TravelApi] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RedVentures.TravelApi] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RedVentures.TravelApi] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RedVentures.TravelApi] SET RECOVERY FULL 
GO
ALTER DATABASE [RedVentures.TravelApi] SET  MULTI_USER 
GO
ALTER DATABASE [RedVentures.TravelApi] SET PAGE_VERIFY NONE  
GO
ALTER DATABASE [RedVentures.TravelApi] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RedVentures.TravelApi] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RedVentures.TravelApi] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [RedVentures.TravelApi] SET DELAYED_DURABILITY = DISABLED 
GO
USE [RedVentures.TravelApi]
GO
/****** Object:  User [TravelApi]    Script Date: 7/12/2017 8:14:11 PM ******/
CREATE LOGIN [TravelApi] WITH PASSWORD = 'TravelApi1234!'
GO
CREATE USER [TravelApi] FOR LOGIN [TravelApi] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [TravelApi]
GO
/****** Object:  Table [dbo].[City]    Script Date: 7/12/2017 8:14:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[City](
	[CityId] [int] IDENTITY(1,1) NOT NULL,
	[StateId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Location] [geography] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_City_StateId_Name] UNIQUE NONCLUSTERED 
(
	[StateId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
/****** Object:  Table [dbo].[State]    Script Date: 7/12/2017 8:14:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[State](
	[StateId] [int] NOT NULL,
	[Code] [char](2) NOT NULL,
	[Name] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_State_Code] UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
/****** Object:  Table [dbo].[User]    Script Date: 7/12/2017 8:14:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserUid] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_User_UserUid] UNIQUE NONCLUSTERED 
(
	[UserUid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
/****** Object:  Table [dbo].[Visit]    Script Date: 7/12/2017 8:14:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Visit](
	[VisitId] [int] IDENTITY(1,1) NOT NULL,
	[VisitUid] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[UserId] [int] NOT NULL,
	[CityId] [int] NOT NULL,
	[DateTimeAdded] [datetime] NOT NULL DEFAULT (getutcdate()),
PRIMARY KEY CLUSTERED 
(
	[VisitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[City]  WITH NOCHECK ADD  CONSTRAINT [FK_City_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([StateId])
GO
ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_City_State]
GO
USE [master]
GO
ALTER DATABASE [RedVentures.TravelApi] SET  READ_WRITE 
GO
```
* You might also consider adding some city/state seed data by running the RedVentures.TravelApi.Database/Post-Deployment/SeedData.sql. I did not seed any test users, so you'll want to manually create at least one of those.
* F5 or Ctrl + F5 in VS to launch it with Kestrel. Or `dotnet restore` and `dotnet run` via the command line.

## Endpoints

* `GET http://localhost:50779/state/{stateCode}/cities`
* `POST http://localhost:50779/user/{userUid}/visits`
	```
	{
		"city": "Birmingham",
		"state": "AL"
	}
	```
* `DEL http://localhost:50779//user/{userUid}/visit/{visitUid}`
* `GET http://localhost:50779//user/{userUid}/visits`
* `GET http://localhost:50779//user/{userUid}/visits/states`

## Notes

* I couldn't decide whether to "follow the directions" or implement the API how I, personally, would choose to do it. I took liberties with the data layer, but followed the spec for the API endpoints. Had I structured the endpoints how I would've liked to, it would resulted in the following:
  * Plural resource names: i.e. "states" instead of "state".
  * Versioning of all resources.
  * GET /api/v1/cities?stateCode={stateCode}
  * POST /api/v1/visits
    * cityName
	* stateCode
  * DEL /api/v1/visits/{visitUid}
  * GET /api/v1/visits?userUid={userUid}
    * Covers cities and states. but if you want some business logic worked in:
	* GET /v1/users/{userUid}?fields=visitedCities
	* GET /v1/users/{userUid}?fields=visitedStates
  * Advantages offered by this design:
    * /api/ to keep the root directory clean. Often used for Swagger.
    * Easily have separate APIs for each type of resource - now or down the road.
    * Reduces complexity of versioning.
* I planned for visits to have datetimes supplied in the future. a user could visit a city on more than one occasion.
* There isn't a Business layer/project right now. If this is going to be a small API or microservice, I tihnk it's ok to keep the business logic in the controller unless code needs to be re-used across multiple projects within the solution. To ensure the business logic can be more easily unit tested, though, you might keep the controllers thin with a thicker business layer for that reason.
* Speaking of unit tests, I didn't write any type of unit or integration tests. Typically I would write unit tests for all business logic and create integration tests for each endpoint.
* Essential real-world API features that I did not implement for this project:
  * Paging
  * Filtering
  * Sorting
  * Projecting / selecting certain fields
  * Security
  * Exception handling
  * Error handling
  * Use of a server framework to be shared across all APIs: validating filters and project fields, error codes, etc.
* More far-reaching things I would do in an API-heavy ecosystem:
  * API guidelines document. Helps all developers across the organization follow standards for API endpoint and resource design including: resource naming, versioning, SLAs, request limits, and much, much more.
  * Creation of a client framework to easily work with these APIs across the ecosystem. Follows standards set forth by the API guidelines.
  * API Key assignment on a per-api basis.
  * Swagger
  * Auto trimming of request parameters
  * Force HTTPS for all endpoints
  * Centralized ValidateModelStateFilter
  * Consistent BadRequest model including error code, field names, and descriptions
  * Exception handling
  * Logging
  * Method XML comments
  * I'm really just scratching the surface here...

## Notes on "Things to Consider" from the original instructions

* How should you deal with invalid or improperly formed requests?
  * I handled some basic errors like missing required data and not found resources (users, states, cities). It's a pretty big debate on whether or not 404s should be used for that, but I did for this project. As far as mistyped URLs in general, 404s typically are used for that as well, hence the debate.
* How should you handle requests that result in large data sets?
  * The paging, filtering, sorting, and projecting features I mentioned above would handle this. Typically I'd want a rule of thumb that only X records may be requested at a time. The client framework would mostly hide this and make it easy for consumers to not have to worry about paging unless they really want fine-grained control over it.

## Notes on "Bonus Points" from the original instructions

* Handle authentication of users prior to allowing changes to their visits
  * This would definitely require more thought and I'd have a handful of questions to ask first: What kind of application is consuming this API? Will the user already have logged in / authenticated? Do users only have access to their own visits? In a perfect world, the application consuming the API would have already authenticated the user and might have somethign like a JWT available that could be passed onto the API, which would confirm the user is who they say they are. Authentication filters could be written and applied to handle that verification as well as responding to unauthorized API calls.
* Make use of the lat/long data for cities in a creative way that provides additional functionality for the client
  * Something simple with this might be to display pins on a map given the coordinates of visited cities. Maybe even have some stats generated for them such as "total distance traveled during all trips".
