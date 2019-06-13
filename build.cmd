@echo off

SET BUILDLOCATION="./Build/Windows/0.0.0.1"

rd %BUILDLOCATION% /q
mkdir %BUILDLOCATION%

"C:\Program Files\Unity\Hub\Editor\2019.1.0f2\Editor\Unity.exe" -quit -batchMode -logFile "%BUILDLOCATION%/Editor.log" -executeMethod BuildHelper.Windows