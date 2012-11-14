@echo off
echo Building solution
msbuild "%~dp0jasmine-headless-webkit-dotnet.sln" /p:Configuration=Release /verbosity:minimal %1 %2 %3 %4 %5
echo Solution built