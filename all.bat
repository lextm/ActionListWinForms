CALL clean.bat
CALL release.bat
CALL clean.bat
CALL release.net40.bat
CALL .nuget/nuget.exe pack
CALL build_installer.bat