start /d "." dotnet build --project ./MoneyManagerApi/MoneyManagerApi.csproj
start /d "." dotnet build --project ./MoneyManagerUi/MoneyManagerUi.csproj
start /d "." dotnet run --project ./MoneyManagerApi/MoneyManagerApi.csproj
start /d "." dotnet run --project ./MoneyManagerUi/MoneyManagerUi.csproj