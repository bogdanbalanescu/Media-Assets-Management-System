# Media-Assets-Management-System

## Run Web Api

#### Web Api Setup
Create SQL Database with name `MediaAssetManagementSystem.DB`.
```
cd ./api/MediaAssetsManagementRestApi/Adapters/Driven/Persistence
dotnet ef database update
```

#### Azure Blob Storage Setup
```
azurite --silent --loose --location c:\azurite_storage --debug c:\azurite_storage\debug.log
```

#### Run Web Api
```
cd ./api/MediaAssetsManagementRestApi/Adapters/Driving/Api
dotnet run
```

## Run Client
```
cd ./client
yarn install
yarn start
```
