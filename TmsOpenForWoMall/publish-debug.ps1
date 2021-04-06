if(test-path D:\Tms\Development\TmsOpenForWoMall\*){
    remove-item D:\Tms\Development\TmsOpenForWoMall\* -recurse
}
$x = Split-Path -Parent $MyInvocation.MyCommand.Definition
dotnet publish "$x\TmsOpenForWoMall.csproj" -c Debug -f netcoreapp2.2 -f -o D:\Tms\Development\TmsOpenForWoMall\ -r win-x64
pause