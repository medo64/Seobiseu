[Setup]
AppName=Seobiseu
AppVerName=Seobiseu 1.00 (alpha)
DefaultDirName={pf}\Josip Medved\Seobiseu
OutputBaseFilename=seobiseu100a
OutputDir=..\Releases
SourceDir=..\Binaries
AppId=JosipMedved_Seobiseu
AppMutex=Global\JosipMedved_Seobiseu
AppPublisher=Josip Medved
AppPublisherURL=http://www.jmedved.com/seobiseu/
UninstallDisplayIcon={app}\Seobiseu.exe
AlwaysShowComponentsList=no
ArchitecturesInstallIn64BitMode=x64
DisableProgramGroupPage=yes
MergeDuplicateFiles=yes
MinVersion=0,6.01.7200
PrivilegesRequired=admin
ShowLanguageDialog=no
SolidCompression=yes
ChangesAssociations=yes
DisableWelcomePage=yes

[Files]
Source: "Seobiseu.exe";         DestDir: "{app}"; Flags: ignoreversion;
Source: "SeobiseuService.exe";  DestDir: "{app}"; Flags: ignoreversion;
;Source: "ReadMe.txt";           DestDir: "{app}"; Attribs: readonly; Flags: overwritereadonly uninsremovereadonly;

[Icons]
Name: "{userstartmenu}\Seobiseu"; Filename: "{app}\Seobiseu.exe"

[Registry]
Root: HKCU; Subkey: "Software\Josip Medved\Seobiseu"; ValueType: dword; ValueName: "Installed"; ValueData: "1"; Flags: uninsdeletekey
Root: HKCU; Subkey: "Software\Josip Medved"; Flags: uninsdeletekeyifempty

[Run]
Filename: "{app}\SeobiseuService.exe"; Parameters: "/Install"; Flags: runascurrentuser waituntilterminated
Filename: "{app}\ReadMe.txt"; Description: "View ReadMe.txt"; Flags: postinstall runasoriginaluser shellexec nowait skipifsilent unchecked
Filename: "{app}\Seobiseu.exe"; Description: "Launch application now"; Flags: postinstall nowait skipifsilent runasoriginaluser unchecked

[UninstallRun]
Filename: "{app}\SeobiseuService.exe"; Parameters: "/Uninstall"; Flags: runascurrentuser waituntilterminated



[Code]

function PrepareToInstall(var NeedsRestart: Boolean): String;
var
    ResultCode: Integer;
begin
    Exec(ExpandConstant('{app}\SeobiseuService.exe'), '/Uninstall', '', SW_SHOW, ewWaitUntilTerminated, ResultCode)
end;

