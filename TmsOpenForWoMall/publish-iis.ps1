import-module -name IISAdministration
### ·¢²¼TmsUserSystem
stop-iissite -name "Wms_Report" -ErrorAction SilentlyContinue
if(test-path D:\Tms\DevOps\Wms_Report\*){
    remove-item D:\Tms\DevOps\Wms_Report\* -recurse -ErrorAction SilentlyContinue
	remove-item D:\Tms\DevOps\Wms_Report\* -recurse -ErrorAction SilentlyContinue
} 
$x = Split-Path -Parent $MyInvocation.MyCommand.Definition
dotnet publish "$x\WmsReport.csproj" -c Debug -f netcoreapp2.1 -f -o D:\Tms\DevOps\Wms_Report\ -r win-x64
start-iissite -name "Wms_Report"

pause