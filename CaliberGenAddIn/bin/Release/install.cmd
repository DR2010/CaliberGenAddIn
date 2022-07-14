echo on
md c:\img
copy .\img c:\img

copy EAAddIn.dll C:\PROGRA~1\SPARXS~1\EA
regaddin.reg
regasm /unregister C:\PROGRA~1\SPARXS~1\EA\EAAddIn.dll
regasm C:\PROGRA~1\SPARXS~1\EA\EAAddIn.dll 
pause

