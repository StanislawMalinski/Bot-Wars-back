#!/bin/bash

dotnet run --project "BotWars/BotWars.csproj" &
sleep 2

start https://localhost:7001/swagger/index.html