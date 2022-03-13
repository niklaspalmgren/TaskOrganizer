## General info
A small blazor server app that can be used to create and organize tasks in different task boards.

## Technologies
Project is created with:
* .NET6
* Entity Framework
* Auto Mapper
* Blazor
* Bootstrap

## Setup
In order to run this project you'll need to have docker installed and running.
https://www.docker.com/get-started

1. #### Clone repository
```
git clone https://github.com/niklaspalmgren/TaskOrganizer
```

2. #### Build
```
cd <project root>
docker-compose build
```

3. #### Run
```
cd <project root>
docker-compose up -d
```
4. #### Access
Once the containers have started you should be able to access the web app through the following url: http://localhost:7070
