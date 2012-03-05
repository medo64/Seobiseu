[Setup]
AppName = Seobiseu
AppVerName = Seobiseu 1.10
DefaultDirName = {pf}\Josip Medved\Seobiseu
OutputBaseFilename = seobiseu110
OutputDir = ..\Releases
SourceDir = ..\Binaries
AppId = JosipMedved_Seobiseu
AppMutex = Global\JosipMedved_Seobiseu
AppPublisher = Josip Medved
AppPublisherURL = http://www.jmedved.com/seobiseu/
UninstallDisplayIcon = {app}\Seobiseu.exe
AlwaysShowComponentsList = no
ArchitecturesInstallIn64BitMode = x64
DisableProgramGroupPage = yes
MergeDuplicateFiles = yes
MinVersion = 0, 5.1.2600
PrivilegesRequired = admin
ShowLanguageDialog = no
SolidCompression = yes
ChangesAssociations = yes
DisableWelcomePage = yes

[Files]
Source: "Seobiseu.exe";         DestDir: "{app}";  Flags: ignoreversion;
Source: "SeobiseuService.exe";  DestDir: "{app}";  Flags: ignoreversion;
Source: "ReadMe.txt";           DestDir: "{app}";  Flags: overwritereadonly uninsremovereadonly;  Attribs: readonly;

[Icons]
Name: "{userstartmenu}\Seobiseu";  Filename: "{app}\Seobiseu.exe"

[Registry]
Root: HKCU;  Subkey: "Software\Josip Medved\Seobiseu";                 ValueName: "Installed";  ValueType: none;                                                Flags: deletevalue uninsdeletevalue
Root: HKLM;  Subkey: "Software\Josip Medved\Seobiseu";                 ValueName: "Installed";  ValueType: dword;   ValueData: "1";                             Flags: uninsdeletekey
Root: HKCU;  Subkey: "Software\Josip Medved";                                                                                                                   Flags: uninsdeletekeyifempty
Root: HKCU;  Subkey: "Software\Microsoft\Windows\CurrentVersion\Run";  ValueName: "Seobiseu";   ValueType: string;  ValueData: """{app}\Seobiseu.exe"" /tray";  Flags: uninsdeletevalue;

[Run]
Filename: "{app}\SeobiseuService.exe";  Parameters: "/Install";                                          Flags: runascurrentuser waituntilterminated
Filename: "{app}\Seobiseu.exe";                                  Description: "Launch application now";  Flags: postinstall nowait skipifsilent runasoriginaluser
Filename: "{app}\ReadMe.txt";                                    Description: "View ReadMe.txt";         Flags: postinstall runasoriginaluser shellexec nowait skipifsilent unchecked

[UninstallRun]
Filename: "{app}\SeobiseuService.exe";  Parameters: "/Uninstall";  Flags: runascurrentuser waituntilterminated



[Code]

function PrepareToInstall(var NeedsRestart: Boolean): String;
var
    ResultCode: Integer;
begin
    Exec(ExpandConstant('{app}\SeobiseuService.exe'), '/Uninstall', '', SW_SHOW, ewWaitUntilTerminated, ResultCode)
    Result := Result;
end;

