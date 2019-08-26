# SGGW spaces
Project of integrated room occupancy navigation at the Warsaw University of Life Sciences.

## Get started

In order to run this project, make sure you have intalled the latest versions of [nodeJS](https://nodejs.org/en/), [Angular CLI](https://angular.io/guide/setup-local) and 
[.Net Core SDK](https://dotnet.microsoft.com/download)

### Clone the repo

```shell
git clone https://github.com/lukaszsoleski/RoomOccupancy.git
```

### Install npm packages

Install the `npm` packages described in the `package.json`. 

```shell
cd RoomOccupancy/RoomOccupancy.API/AngularClient
npm install
```

### Run Angular client

```shell
npm serve -o
```

### Run .Net Core server

From the API folder 'RoomOccupancy/RoomOccupancy.API' run a following command:

```shell
dotnet run --urls=http://localhost:51583/
```



