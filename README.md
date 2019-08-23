# RoomOccupancy
Project of integrated room occupancy navigation at the Warsaw University of Life Sciences.

## Get started

To run this project, make sure you have intalled the latest versions of [a nodeJS](https://nodejs.org/en/), [a Angular CLI](https://angular.io/guide/setup-local) and 
[a .Net Core SDK](https://dotnet.microsoft.com/download)

### Clone the repo or download the zip

```shell
git clone https://github.com/lukaszsoleski/RoomOccupancy.git
cd RoomOccupancy/RoomOccupancy.API/AngularClient
```

### Install npm packages

Install the `npm` packages described in the `package.json`. 

```shell
npm install
```

### Run Angular application

This command will compile Angular application and open it in a new browser window.

```shell
npm serve -o
```

### Run .Net Core server

From the API folder 'RoomOccupancy/RoomOccupancy.API' run the a following command:

```shell
dotnet run --urls=http://localhost:51583/
```
This will: 
- compile solution projects
- create and migrate local database
- seed example data
- run .net core Web API at localhost:51583



