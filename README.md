# cafe-management
This is web programming asp.net project

## To run
1. Change name server in `appsettings.json`
```
  "ConnectionStrings": {
  "CafeConnection": "Server={change-here};Database=TheCoffeeHouse;...
}
```

2. open `tools` -> `NuGet Package Manager` -> `Package Manager Console`
    
    -> then run in console 

    - `add-migration`
        - name can be something, like v1 v2 v3

    - `update-Database`