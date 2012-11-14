@echo off
call "%~dp0build.cmd"
echo Creating one executable for jasmine-headless-webkit-dotnet.exe
set buildconfig=release
set bindir=%~dp0jasmine-headless-webkit-dotnet\bin\%buildconfig%\
set packagedir=%~dp0package\
set ilmergedir=%~dp0packages\ilmerge.2.12.0803\
if not exist "%packagedir%" md "%packagedir%"
if not "%ProgramFiles(x86)%"=="" set programfilesnormalized=%ProgramFiles(x86)%
if "%programfilesnormalized%"=="" set programfilesnormalized=%ProgramFiles%
"%ilmergedir%ilmerge.exe" /targetplatform:v4,"%programfilesnormalized%\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0" "%bindir%jasmine-headless-webkit-dotnet.exe" "%bindir%Args.dll" "%bindir%CoffeeSharp.dll" "%bindir%Jurassic.dll" "%bindir%Newtonsoft.Json.dll" /out:"%packagedir%\jasmine-headless-webkit-dotnet.exe" %1 %2 %3 %4
echo Executable created at "%packagedir%jasmine-headless-webkit-dotnet.exe"