set msBuildDir=C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin
call "%MSBuildDir%\msbuild" Crad.Windows.Forms.Actions.sln /t:build /p:Configuration=Release /p:TargetFrameworkVersion=v4.5 /p:OutputPath=..\bin\net45\
@IF %ERRORLEVEL% NEQ 0 PAUSE