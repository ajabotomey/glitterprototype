@echo off

SET BUILDLOCATION="./Build/%TARGET%/%VERSION%"

rd %BUILDLOCATION% /q
mkdir %BUILDLOCATION%

"C:\Program Files\Unity\Hub\Editor\2019.1.0f2\Editor\Unity.exe" -quit -batchMode -logFile "%BUILDLOCATION%/Editor.log" -executeMethod BuildHelper.Windows