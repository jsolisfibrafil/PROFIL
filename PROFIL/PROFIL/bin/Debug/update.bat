@echo off 
color 0A
taskkill /f /im profil.exe 
 
ECHO %0 
pause
del /f /q "profil.exe"
rename "PROFIL2.exe" "PROFIL.exe" 
@echo off 
start PROFIL.exe
exit 