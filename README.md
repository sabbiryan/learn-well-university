# Learn Well University
This is a simple university course management domain project, where includes student, instructor and staff to manage the courses and classes

## Prerequisites
* Dotnet 9
* Docker
* Angular v20
*  Code Editor (Visual Studio 2022 recommended)

## Getting Started
This has applciation has two external docker container dependenciys, we need to ensure this two docker container service is runing. Here are the following docker command.

```
docker run --name seq -d -p 5341:80  -e ACCEPT_EULA=Y  -e SEQ_FIRSTRUN_ADMINPASSWORD=pass123   datalust/seq 

docker run --name learn-well-university -e POSTGRES_PASSWORD=pass123 -p 5432:5432 -d postgres

```
* Clone and open the server code in visual studio and select Conatiner prfoile. Then build and run the server project.

* Now check the following urls:
  1. SEQ: http://localhost:5341
  2. Web Api: https://localhost:7105
 
* For database you can try to connect from pgAdmin or dbeaver using the credentials we set during the docker container run. 





