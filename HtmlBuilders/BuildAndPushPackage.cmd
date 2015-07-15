@echo off
set thisScriptFilename=%~n0%~x0

rem -- check if current dir is correct
if not exist %thisScriptFilename% echo ------------- Please call from project-folder as current dir!
if not exist %thisScriptFilename% goto ende

set releaseFolder=.\bin\Release\

rem -- remove old packages if any
if exist "%releaseFolder%*.nupkg" del %releaseFolder%*.nupkg


msbuild /p:Configuration=Release /t:Rebuild
IF NOT %ERRORLEVEL%==0 echo ------------- PLEASE execute from 'Developer Command Prompt'!
IF NOT %ERRORLEVEL%==0 GOTO ende

msbuild /p:Configuration=Release /t:BuildPackage
IF NOT %ERRORLEVEL%==0 GOTO ende


rem -- remove source-packages if any
if exist "%releaseFolder%*symbols.nupkg" del "%releaseFolder%*symbols.nupkg"

rem -- find and set filename for new package
FOR %%f in ("%releaseFolder%*.nupkg") DO set package=%%f

echo.
echo.
echo ---- INFO: Use '..\.nuget\NuGet.exe setApiKey [YourKey]' if you push the first time on this machine
echo.
..\.nuget\nuget push "%package%"
IF NOT %ERRORLEVEL%==0 GOTO ende

:ende
