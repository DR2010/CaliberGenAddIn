@echo off

echo Cleaning previous versions
echo --------------------------


del /q /s c:\tfs\soluti~1\calibergenaddin\deploy\
c:
md\tfs\soluti~1\calibergenaddin\deploy\10.0.285.65535
md\tfs\soluti~1\calibergenaddin\deploy\10.0.345.65535
md\tfs\soluti~1\calibergenaddin\deploy\10.0.345.65535 14Mar2009 1523


echo Building for CaliberRM V10
echo --------------------------


copy "\\edmgt022\eakeystore$\EA Software\DEEWR AddIn\CaliberVersions\10.0.285.65535\*.*" "C:\Program Files\Borland\CaliberRM\"

C:\WINDOWS\Microsoft.NET\Framework\v3.5\msbuild c:\tfs\soluti~1\calibergenaddin\calibergenaddin.sln /t:rebuild

copy "C:\Program Files\Sparx Systems\EA\EAAddIn.dll" 			c:\tfs\soluti~1\calibergenaddin\deploy\10.0.285.65535\
copy "C:\Program Files\Sparx Systems\EA\CaliberRMSDK100.NET.dll" 	c:\tfs\soluti~1\calibergenaddin\deploy\10.0.285.65535\


echo Building for CaliberRM V10 SP1a
echo ------------------------------


copy "\\edmgt022\eakeystore$\EA Software\DEEWR AddIn\CaliberVersions\10.0.345.65535\*.*" "C:\Program Files\Borland\CaliberRM\"

C:\WINDOWS\Microsoft.NET\Framework\v3.5\msbuild c:\tfs\soluti~1\calibergenaddin\calibergenaddin.sln /t:rebuild

copy "C:\Program Files\Sparx Systems\EA\EAAddIn.dll" 			c:\tfs\soluti~1\calibergenaddin\deploy\10.0.345.65535\
copy "C:\Program Files\Sparx Systems\EA\CaliberRMSDK100.NET.dll" 	c:\tfs\soluti~1\calibergenaddin\deploy\10.0.345.65535\

echo Building for CaliberRM V10 SP1b
echo ------------------------------


copy "\\edmgt022\eakeystore$\EA Software\DEEWR AddIn\CaliberVersions\10.0.345.65535 14Mar2009 1523\*.*" "C:\Program Files\Borland\CaliberRM\"

C:\WINDOWS\Microsoft.NET\Framework\v3.5\msbuild c:\tfs\soluti~1\calibergenaddin\calibergenaddin.sln /t:rebuild

copy "C:\Program Files\Sparx Systems\EA\EAAddIn.dll" 			c:\tfs\soluti~1\calibergenaddin\deploy\10.0.345.65535 14Mar2009 1523\
copy "C:\Program Files\Sparx Systems\EA\CaliberRMSDK100.NET.dll" 	c:\tfs\soluti~1\calibergenaddin\deploy\10.0.345.65535 14Mar2009 1523\

echo =====================================
echo              ALL   DONE 
echo =====================================

pause
