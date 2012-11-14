@echo off
echo Creating nuget file
set packagedir=%~dp0package\
set nugetdir=%~dp0packages\NuGet.CommandLine.2.1.2\
if not exist "%packagedir%" md "%packagedir%"
call "%~dp0CreateOneExec.cmd"
"%nugetdir%tools\nuget.exe" pack "%~dp0jasmine-headless-webkit-dotnet.nuspec" -OutputDirectory %packagedir% -BasePath %~dp0 %1 %2 %3 %4
echo Nuget package created at "%packagedir%"