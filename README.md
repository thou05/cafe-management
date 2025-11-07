# cafe-management
This is web programming asp.net project

## To run
1. Change name server in `appsettings.json`
```
  "ConnectionStrings": {
    "Day09LabCFConnection": "Server={change-here}; Database=webDay09; uid=sa; pwd=thou; MultipleActiveResultSets=True;TrustServerCertificate=True;"
  }
```

2. open `tools` -> `NuGet Package Manager` -> `Package Manager Console`
    -> then run in console 
        - `add-migration`
            - name can be something, like v1 v2 v3
        - `update-Database`