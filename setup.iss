#define MyAppID "{16887BC4-900D-454A-AAA2-B7FE95E99DCE}"
#define MyAppCopyright "Copyright (C) 2006-2012 Marco De Sanctis, Lex Li and other contributors"
#define MyAppName "ActionList for Windows Forms"
#define MyAppVersion GetFileVersion(".\bin\net20\Crad.Windows.Forms.Actions.dll")
#define ConflictingProcess "devenv.exe"
#define ConflictingApp "Visual Studio"
#pragma message "Detailed version info: " + MyAppVersion

[Setup]
AppName={#MyAppName}
AppVerName={#MyAppName}
AppPublisher=Lex Li (lextm)
AppPublisherURL=http://lextm.com
AppSupportURL=http://lextm.com
AppUpdatesURL=http://github.com/lextm/ActionListWinForms
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
OutputDir=.
SolidCompression=true
AppCopyright={#MyAppCopyright}
VersionInfoVersion={#MyAppVersion}
VersionInfoCompany=LeXtudio
VersionInfoDescription={#MyAppName} {#MyAppVersion} Setup
VersionInfoTextVersion={#MyAppVersion}
InternalCompressLevel=ultra
VersionInfoCopyright={#MyAppCopyright}
PrivilegesRequired=admin
ShowLanguageDialog=yes
WindowVisible=false
AppVersion={#MyAppVersion}
AppId={{#MyAppID}
UninstallDisplayName={#MyAppName}
CompressionThreads=2
MinVersion=0,5.01sp3

[Languages]
Name: english; MessagesFile: compiler:Default.isl

[Files]
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

; dll used to check running notepad at install time
Source: ".\processviewer\processviewer.exe"; Flags: dontcopy

;psvince is installed in {app} folder, so it will be
;loaded at uninstall time ;to check if notepad is running
Source: ".\processviewer\processviewer.exe"; DestDir: "{app}"
Source: ".\bin\net20\*.*"; DestDir: "{app}\net20"; Flags: ignoreversion
Source: ".\bin\net40\*.*"; DestDir: "{app}\net40"; Flags: ignoreversion
Source: ".\tools\*.*"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}";
Name: "{group}\Author's Blog"; Filename: "http://lextm.com";
Name: "{group}\Report A Bug"; Filename: "https://github.com/lextm/ActionListWinForms/issues"; 
Name: "{group}\Homepage"; Filename: "https://github.com/lextm/ActionListWinForms";

[Registry]
Root: "HKLM"; Subkey: "SOFTWARE\Microsoft\.NETFramework\v2.0.50727\AssemblyFoldersEx\ActionListWinForms"; ValueType: string; ValueData: "{app}\net20\"; Flags: uninsdeletekey
Root: "HKLM"; Subkey: "SOFTWARE\Microsoft\.NETFramework\v4.0.30319\AssemblyFoldersEx\ActionListWinForms"; ValueType: string; ValueData: "{app}\net40\"; Flags: uninsdeletekey

[Run]
Filename: "{app}\Toolbox.exe"; Parameters: "/vs2008 /installdesktop ""{app}\net20\Crad.Windows.Forms.Actions.dll"" ""ActionList for WinForms"""; WorkingDir: "{app}"; Flags: waituntilterminated
Filename: "{app}\Toolbox.exe"; Parameters: "/vs2010 /installdesktop ""{app}\net20\Crad.Windows.Forms.Actions.dll"" ""ActionList for WinForms"""; WorkingDir: "{app}"; Flags: waituntilterminated
Filename: "{app}\gacutil_2.0.exe"; Parameters: "-i ""{app}\net20\Crad.Windows.Forms.Actions.dll"""; WorkingDir: "{app}"; Flags: waituntilterminated runhidden
Filename: "{app}\gacutil_2.0.exe"; Parameters: "-i ""{app}\net20\Crad.Windows.Forms.Actions.Design.dll"""; WorkingDir: "{app}"; Flags: waituntilterminated runhidden

[UninstallRun]
Filename: "{app}\Toolbox.exe"; Parameters: "/vs2008 /uninstall ""ActionList for WinForms"""; WorkingDir: "{app}"; Flags: waituntilterminated
Filename: "{app}\Toolbox.exe"; Parameters: "/vs2010 /uninstall ""ActionList for WinForms"""; WorkingDir: "{app}"; Flags: waituntilterminated
Filename: "{app}\gacutil_2.0.exe"; Parameters: "-u Crad.Windows.Forms.Actions"; WorkingDir: "{app}"; Flags: waituntilterminated
Filename: "{app}\gacutil_2.0.exe"; Parameters: "-u Crad.Windows.Forms.Actions.Design"; WorkingDir: "{app}"; Flags: waituntilterminated

[Code]
// =======================================
// Testing if under Windows safe mode
// =======================================
function GetSystemMetrics( define: Integer ): Integer; external
'GetSystemMetrics@user32.dll stdcall';

Const SM_CLEANBOOT = 67;

function IsSafeModeBoot(): Boolean;
begin
  // 0 = normal boot, 1 = safe mode, 2 = safe mode with networking
 Result := ( GetSystemMetrics( SM_CLEANBOOT ) <> 0 );
end;

// ======================================
// Testing version number string
// ======================================
function GetNumber(var temp: String): Integer;
var
  part: String;
  pos1: Integer;
begin
  if Length(temp) = 0 then
  begin
    Result := -1;
    Exit;
  end;
  pos1 := Pos('.', temp);
  if (pos1 = 0) then
  begin
    Result := StrToInt(temp);
  temp := '';
  end
  else
  begin
  part := Copy(temp, 1, pos1 - 1);
    temp := Copy(temp, pos1 + 1, Length(temp));
    Result := StrToInt(part);
  end;
end;

function CompareInner(var temp1, temp2: String): Integer;
var
  num1, num2: Integer;
begin
  num1 := GetNumber(temp1);
  num2 := GetNumber(temp2);
  if (num1 = -1) or (num2 = -1) then
  begin
    Result := 0;
    Exit;
  end;
  if (num1 > num2) then
  begin
  Result := 1;
  end
  else if (num1 < num2) then
  begin
  Result := -1;
  end
  else
  begin
  Result := CompareInner(temp1, temp2);
  end;
end;

function CompareVersion(str1, str2: String): Integer;
var
  temp1, temp2: String;
begin
  temp1 := str1;
  temp2 := str2;
  Result := CompareInner(temp1, temp2);
end;

function ProductRunning(): Boolean;
var
  ResultCode: Integer;
begin  
  ExtractTemporaryFile('processviewer.exe');
  if Exec(ExpandConstant('{tmp}\processviewer.exe'), '{#ConflictingApp}', '', SW_HIDE,
     ewWaitUntilTerminated, ResultCode) then
  begin
    Result := ResultCode > 0;
    Exit;    
  end;  
  
  MsgBox('failed to check process', mbError, MB_OK);
end;

function ProductRunningU(): Boolean;
var
  ResultCode: Integer;
begin  
  if Exec(ExpandConstant('{app}\processviewer.exe'), '{#ConflictingApp}', '', SW_HIDE,
     ewWaitUntilTerminated, ResultCode) then
  begin
    Result := ResultCode > 0;
    Exit;    
  end;  
  
  MsgBox('failed to check process.', mbError, MB_OK);
end;

function ProductInstalled(): Boolean;
begin
  Result := RegKeyExists(HKEY_LOCAL_MACHINE,
  'SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{#MyAppID}_is1');
end;

function VCRuntimeInstalled(): Boolean;
var 
  installed: Cardinal;
begin
  if not (RegQueryDWordValue(HKEY_LOCAL_MACHINE,
  'SOFTWARE\Microsoft\VisualStudio\10.0\VC\VCRedist\x86', 'Installed', installed)) then
  begin
    Result := False;
    Exit;
  end;

  Result := installed = 1;
end;

function VCRuntimeInstall(): Boolean;
var
  ResultCode: Integer;
begin  
  ExtractTemporaryFile('vcredist_x86.exe');
  if Exec(ExpandConstant('{tmp}\vcredist_x86.exe'), '/q', '', SW_HIDE,
     ewWaitUntilTerminated, ResultCode) then
  begin
    Result := ResultCode = 0;
    Exit;    
  end;  
  
  MsgBox('Failed to install Visual C++ 2010 runtime', mbError, MB_OK);
end;

function DotNetFrameworkInstalled(): Boolean;
begin
  Result := RegKeyExists(HKLM, 'Software\Microsoft\.NETFramework\policy\v2.0');
end;

function DotNetFramework4Installed(): Boolean;
begin
  Result := RegKeyExists(HKLM, 'Software\Microsoft\.NETFramework\policy\v4.0');
end;

function DotNetFrameworkInstall(): Boolean;
var
  ResultCode: Integer;
begin  
  ExtractTemporaryFile('dotNetFx40_Full_x86.exe');
  if Exec(ExpandConstant('{tmp}\dotNetFx40_Full_x86.exe'), '/q', '', SW_HIDE,
     ewWaitUntilTerminated, ResultCode) then
  begin
    Result := ResultCode = 0;
    Exit;    
  end;  
  
  MsgBox('Failed to install .NET Framework 4.0', mbError, MB_OK);
end;

function InitializeSetup(): Boolean;
var
  oldVersion: String;
  uninstaller: String;
  ErrorCode: Integer;
  compareResult: Integer;
  ResultCode: Integer;
begin
  if IsSafeModeBoot then
  begin
    MsgBox('Cannot install under Windows Safe Mode.', mbError, MB_OK);
    Result := False;
    Exit;
  end;
  
  if not DotNetFrameworkInstalled then
  begin
    // Ask the user a Yes/No question
    if MsgBox('.NET Framework 2.0 is needed. Click Yes to learn how to install it, or click No to exit.', mbConfirmation, MB_YESNO) = IDYES then
    begin
      // user clicked Yes
      Exec('iexplore.exe', 'http://msdn.microsoft.com/en-us/library/aa480242.aspx', '', SW_SHOW, ewNoWait, ResultCode);
    end
    else
    begin
      Result := False;
      Exit;
    end;
  end;
  
  while ProductRunning do
  begin
    if MsgBox( '{#ConflictingApp} is running. Click Yes to shut it down and continue installation, or click No to exit.', mbConfirmation, MB_YESNO ) = IDNO then
    begin
      Result := False;
      Exit;
    end;

    Exec('cmd.exe', '/C "taskkill /F /IM {#ConflictingProcess}"', '', SW_HIDE,
     ewWaitUntilTerminated, ResultCode);
  end;

  if not ProductInstalled then
  begin
    Result := True;
    Exit;
  end;

  RegQueryStringValue(HKEY_LOCAL_MACHINE,
    'SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{#MyAppID}_is1',
    'DisplayVersion', oldVersion);
  compareResult := CompareVersion(oldVersion, '{#MyAppVersion}');
  if (compareResult > 0) then
  begin
    MsgBox('Version ' + oldVersion + ' of {#MyAppName} is already installed. It is newer than {#MyAppVersion}. This installer will exit.',
    mbInformation, MB_OK);
    Result := False;
    Exit;
  end
  else if (compareResult = 0) then
  begin
    if (MsgBox('{#MyAppName} ' + oldVersion + ' is already installed. Do you want to repair it now?',
    mbConfirmation, MB_YESNO) = IDNO) then
  begin
    Result := False;
    Exit;
    end;
  end
  else
  begin
    if (MsgBox('{#MyAppName} ' + oldVersion + ' is already installed. Do you want to override it with {#MyAppVersion} now?',
    mbConfirmation, MB_YESNO) = IDNO) then
  begin
    Result := False;
    Exit;
    end;
  end;
  // remove old version
  RegQueryStringValue(HKEY_LOCAL_MACHINE,
  'SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{#MyAppID}_is1',
  'UninstallString', uninstaller);
  ShellExec('runas', uninstaller, '/SILENT', '', SW_HIDE, ewWaitUntilTerminated, ErrorCode);
  if (ErrorCode <> 0) then
  begin
  MsgBox( 'Failed to uninstall {#MyAppName} version ' + oldVersion + '. Please restart Windows and run setup again.',
   mbError, MB_OK );
  Result := False;
  Exit;
  end;

  Result := True;
end;

function InitializeUninstall(): Boolean;
var
  ResultCode: Integer;
begin
  if IsSafeModeBoot then
  begin
    MsgBox( 'Cannot uninstall under Windows Safe Mode.', mbError, MB_OK);
    Result := False;
    Exit;
  end;

  while ProductRunningU do
  begin
    if MsgBox( '{#ConflictingApp} is running. Click Yes to shut it down and continue installation, or click No to exit.', mbConfirmation, MB_YESNO ) = IDNO then
    begin
      Result := False;
      Exit;
    end;

    Exec('cmd.exe', '/C "taskkill /F /IM {#ConflictingProcess}"', '', SW_HIDE,
     ewWaitUntilTerminated, ResultCode)
  end;

  Result := true;
end;

procedure CurStepChanged(CurStep: TSetupStep);
var
  ErrorCode: Integer;
begin
  if (CurStep = ssPostInstall) then
  begin
    if DotNetFramework4Installed then
    begin
      ShellExec('', ExpandConstant('{app}\gacutil_4.0.exe'),
        ExpandConstant('-i "{app}\net40\Crad.Windows.Forms.Actions.dll"'), '', SW_HIDE, ewWaitUntilTerminated, ErrorCode);
      ShellExec('', ExpandConstant('{app}\gacutil_4.0.exe'),
        ExpandConstant('-i "{app}\net40\Crad.Windows.Forms.Actions.Design.dll"'), '', SW_HIDE, ewWaitUntilTerminated, ErrorCode);       
    end;
  end;
end;

procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
var
  ErrorCode: Integer;
begin
  if (CurUninstallStep = usAppMutexCheck) then
  begin
    if DotNetFramework4Installed then
    begin
      ShellExec('', ExpandConstant('{app}\gacutil_4.0.exe'),
        '-u Crad.Windows.Forms.Actions', '', SW_HIDE, ewWaitUntilTerminated, ErrorCode);
      ShellExec('', ExpandConstant('{app}\gacutil_4.0.exe'),
        '-u Crad.Windows.Forms.Actions.Design', '', SW_HIDE, ewWaitUntilTerminated, ErrorCode);       
    end;
  end;
end;
