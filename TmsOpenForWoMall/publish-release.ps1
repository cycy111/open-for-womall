if(test-path D:\Tms\Production\TmsOpenForWoMall\*){
    remove-item D:\Tms\Production\TmsOpenForWoMall\* -recurse
}
$x = Split-Path -Parent $MyInvocation.MyCommand.Definition
dotnet publish "$x\TmsOpenForWoMall.csproj" -c Release -f netcoreapp2.2 -f -o D:\Tms\Production\TmsOpenForWoMall\ -r win-x64
pause