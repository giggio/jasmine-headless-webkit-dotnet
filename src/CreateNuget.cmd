@echo off
FOR /F %%i IN (%~dp0versionnumber.txt) DO set version=%%i
set packagedir=%~dp0package\
set nugetdir=%~dp0packages\NuGet.CommandLine.2.1.2\
set nugetfile=%packagedir%jasmine-headless-webkit-dotnet.nupkg
set nugetfilecreated=%packagedir%jasmine-headless-webkit-dotnet.%version%.nupkg
if exist "%packagedir%"*.nupkg del "%packagedir%"*.nupkg
call "%~dp0CreateOneExec.cmd"
if not exist "%packagedir%" goto :error
echo Creating nuget file
"%nugetdir%tools\nuget.exe" pack "%~dp0jasmine-headless-webkit-dotnet.nuspec" -OutputDirectory %packagedir% -BasePath %~dp0 -Version %version% %1 %2 %3 %4
copy "%nugetfilecreated%" "%nugetfile%"
echo Nuget package created at "%nugetfile%"
exit /B

:error
echo Executable not found and cannot be packaged
exit /B 1