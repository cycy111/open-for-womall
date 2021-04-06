if(test-path D:\Tms\Staging\TmsOpenForWoMall\*){
    remove-item D:\Tms\Staging\TmsOpenForWoMall\* -recurse
}
$x = Split-Path -Parent $MyInvocation.MyCommand.Definition
dotnet publish "$x\TmsOpenForWoMall.csproj" -c Debug -f netcoreapp2.2 -f -o D:\Tms\Staging\TmsOpenForWoMall\ -r win-x64
pause