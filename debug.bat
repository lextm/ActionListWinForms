set msBuildDir=%WINDIR%\Microsoft.NET\Framework\v4.0.30319
call %MSBuildDir%\msbuild Crad.Windows.Forms.Actions.sln /t:build /p:Configuration="Debug"
@IF %ERRORLEVEL% NEQ 0 PAUSE