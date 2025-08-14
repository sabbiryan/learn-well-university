# Learn Well University
This is a simple university course management domain project, where includes student, instructor and staff to manage the courses and classes and enrollemnts

## Prerequisites
* Dotnet 9
* Docker
* Angular v20
* Code Editor (Visual Studio 2022 recommended)

## Tech Insights
* Clean Architecture
* DDD
* Generic Repository Pattern
* Unit of Work
* Generic CRUD Apis (See details bellow how to use it)
* Postgres
* SEQ

## Getting Started
This has applciation has two external docker container dependencies, we need to ensure this two docker container service is runing. Here are the following docker command.


### Run by docker commands
```
docker run --name learnwelluniversity.seq -d -p 5341:80  -e ACCEPT_EULA=Y  -e SEQ_FIRSTRUN_ADMINPASSWORD=pass123   datalust/seq 

docker run --name learnwelluniversity.db -e POSTGRES_PASSWORD=pass123 -p 5433:5432 -d postgres //

```
* Clone and open the server code in visual studio and select **Conatiner profile** by setting the WebApi project as startup. Then build and run the server project.
* Make sure you have updated your appsetting.Development.json as following
```
DefaultConnection": "Host=host.docker.internal;Port=5433;Database=LearnWellUniversityDb;Username=postgres;Password=pass123; //Connect youi web api project to docker containarized postgres database are publicly exposed

"Serilog":"WriteTo":"Args":"serverUrl": "http://host.docker.internal:5341" //This ensure to write logs into SEQ server.
```

### Run by container orchastation 
```
docker compose -p learnwelluniversity up

```

* Now check the following urls:
  1. SEQ: http://localhost:5341
  2. Web Api: https://localhost:7105 //make sure you are using https not http.
 





