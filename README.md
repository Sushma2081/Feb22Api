# 22 Feb 2023 - EF Core with NpgSQL

```
dotnet build
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef
dotnet ef dbcontext info
dotnet ef migrations list
```

## Skip if migration file is already generated

```
dotnet ef migrations add "initial"
```

```
dotnet ef database update
```
