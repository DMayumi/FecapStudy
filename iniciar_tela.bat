@echo off
chcp 65001 > nul
set "DOTNET=dotnet"
if exist "%LOCALAPPDATA%\FecapStudy\dotnet\dotnet.exe" set "DOTNET=%LOCALAPPDATA%\FecapStudy\dotnet\dotnet.exe"
"%DOTNET%" run --project "%~dp0FecapStudy\FecapStudyUI\FecapStudyUI.csproj"
if errorlevel 1 pause
