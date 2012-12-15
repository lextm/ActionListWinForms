CALL clean.bat
CALL release.bat
CALL clean.bat
CALL release.net40.bat
CALL build_installer.bat
cd .nuget
CALL nuget.exe pack