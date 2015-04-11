@ECHO OFF
SETLOCAL enabledelayedexpansion

SET        FILE_SETUP=".\Seobiseu.iss"
SET     FILE_SOLUTION="..\Source\Seobiseu.sln"
SET  FILES_EXECUTABLE="..\Binaries\Seobiseu.exe" "..\Binaries\SeobiseuService.exe"
SET       FILES_OTHER="..\Binaries\ReadMe.txt"
SET     FILES_LICENSE="License.txt"

SET    COMPILE_TOOL_1="%PROGRAMFILES(X86)%\Microsoft Visual Studio 12.0\Common7\IDE\devenv.exe"
SET    COMPILE_TOOL_2="%PROGRAMFILES(X86)%\Microsoft Visual Studio 12.0\Common7\IDE\WDExpress.exe"
SET        SETUP_TOOL="%PROGRAMFILES(x86)%\Inno Setup 5\iscc.exe"

SET         SIGN_TOOL="%PROGRAMFILES(X86)%\Windows Kits\8.0\bin\x86\signtool.exe"
SET         SIGN_HASH="C02FF227D5EE9F555C13D4C622697DF15C6FF871"
SET SIGN_TIMESTAMPURL="http://timestamp.comodoca.com/rfc3161"

FOR /F "delims=" %%N IN ('hg id -i 2^> NUL') DO @SET HG_NODE=%%N%
FOR /F "delims=+" %%N IN ('hg id -n 2^> NUL') DO @SET HG_NODE_NUMBER=%%N%


ECHO --- BUILD SOLUTION
ECHO.

IF EXIST %COMPILE_TOOL_1% (
    ECHO Using Visual Studio
    SET COMPILE_TOOL=%COMPILE_TOOL_1%
) ELSE (
    IF EXIST %COMPILE_TOOL_2% (
        ECHO Using Visual Studio Express
        SET COMPILE_TOOL=%COMPILE_TOOL_2%
    ) ELSE (
        ECHO Cannot find Visual Studio!
        PAUSE && EXIT /B 255
    )
)

RMDIR /Q /S "..\Binaries" 2> NUL
%COMPILE_TOOL% /Build "Release" %FILE_SOLUTION%
COPY ..\ReadMe.text ..\Binaries\ReadMe.txt
IF ERRORLEVEL 1 PAUSE && EXIT /B %ERRORLEVEL%

ECHO.


CERTUTIL -silent -verifystore -user My %SIGN_HASH% > NUL
IF %ERRORLEVEL%==0 (
    ECHO --- SIGN SOLUTION
    ECHO.
    
    IF [%SIGN_TIMESTAMPURL%]==[] (
        %SIGN_TOOL% sign /s "My" /sha1 %SIGN_HASH% /v %FILES_EXECUTABLE%
    ) ELSE (
        %SIGN_TOOL% sign /s "My" /sha1 %SIGN_HASH% /tr %SIGN_TIMESTAMPURL% /v %FILES_EXECUTABLE%
    )
    IF ERRORLEVEL 1 PAUSE && EXIT /B %ERRORLEVEL%
) ELSE (
    ECHO --- DID NOT SIGN SOLUTION
    IF NOT [%SIGN_HASH%]==[] (
        ECHO.
        ECHO No certificate with hash %SIGN_HASH%.
    ) 
)
ECHO.
ECHO.


ECHO --- BUILD SETUP
ECHO.

RMDIR /Q /S ".\Temp" 2> NUL
CALL %SETUP_TOOL% /DHgNode=%HgNode% /O".\Temp" %FILE_SETUP%
IF ERRORLEVEL 1 PAUSE && EXIT /B %ERRORLEVEL%

FOR /F %%I IN ('DIR ".\Temp\*.exe" /B') DO SET _SETUPEXE=%%I
ECHO Setup is in file %_SETUPEXE%

ECHO.
ECHO.


ECHO --- RENAME BUILD
ECHO.

SET _OLDSETUPEXE=%_SETUPEXE%
IF NOT [%HG_NODE%]==[] (
    SET _SETUPEXE=!_SETUPEXE:000=-rev%HG_NODE_NUMBER%-%HG_NODE%!
)
IF NOT "%_OLDSETUPEXE%"=="%_SETUPEXE%" (
    ECHO Renaming %_OLDSETUPEXE% to %_SETUPEXE%
    MOVE ".\Temp\%_OLDSETUPEXE%" ".\Temp\%_SETUPEXE%"
) ELSE (
    ECHO No rename needed.
)

ECHO.
ECHO.


CERTUTIL -silent -verifystore -user My %SIGN_HASH% > NUL
IF %ERRORLEVEL%==0 (
    ECHO --- SIGN SETUP
    ECHO.
    
    IF [%SIGN_TIMESTAMPURL%]==[] (
        %SIGN_TOOL% sign /s "My" /sha1 %SIGN_HASH% /v ".\Temp\%_SETUPEXE%"
    ) ELSE (
        %SIGN_TOOL% sign /s "My" /sha1 %SIGN_HASH% /tr %SIGN_TIMESTAMPURL% /v ".\Temp\%_SETUPEXE%"
    )
    IF ERRORLEVEL 1 PAUSE && EXIT /B %ERRORLEVEL%
) ELSE (
    ECHO --- DID NOT SIGN SETUP
)
ECHO.
ECHO.


ECHO --- RELEASE
ECHO.

MKDIR "..\Releases" 2> NUL
MOVE ".\Temp\*.*" "..\Releases\." > NUL
IF ERRORLEVEL 1 PAUSE && EXIT /B %ERRORLEVEL%
RMDIR /Q /S ".\Temp"

ECHO.


ECHO --- DONE
ECHO.

PAUSE
