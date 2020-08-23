# Media-Assets-Management-System

## Web Api Setup
Create SQL Database with name `MediaAssetManagementSystem.DB`.
```
cd ./api/MediaAssetsManagementRestApi/Adapters/Driven/Persistence
dotnet ef database update
```

## Run Web Api
```
cd ./api/MediaAssetsManagementRestApi/Adapters/Driving/Api
dotnet run
```

## Run Client
```
cd ./client
yarn start
```