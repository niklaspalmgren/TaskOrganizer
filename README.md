## General info
A small blazor server app that can be used to create and organize tasks in different task boards.

## Technologies
Project is created with:
* .NET6
* Entity Framework
* Auto Mapper
* Blazor
* Bootstrap

## Prerequisites
In order to run this project you'll need to have docker installed and running.
https://www.docker.com/get-started

## Setup
Clone this repo to your desktop.
```
$ git clone https://github.com/niklaspalmgren/TaskOrganizer
```
## Usage
After you've cloned this repo to your desktop, go to its root directory and run the following commands to build and run the docker images.
```
$ docker-compose build
$ docker-compose up -d
```
Once all containers have started, access the web app: http://localhost:7070
