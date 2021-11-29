@echo off
title TKeyChain instalation...

REM Compile the program and store the output to bin folder
dotnet publish .\TKeyChain\TKeyChain.Console\TKeyChain.Cli.csproj --runtime win-x64 --output ./build --self-contained true --configuration Release /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true

REM Create directory for executable
MKDIR bin

REM Copy the executable file to main directory 
COPY build\TKeyChain.Cli.exe bin\tkc.exe

REM Delete build directory
RMDIR /s /q build

REM Remove the source code directory
RMDIR /s /q TKeyChain

REM Remove the .git hidden directory
RMDIR /s /q .git

REM Remove the .gitignore file
DEL /q .gitignore

REM Remove the installer
DEL /q build.cmd