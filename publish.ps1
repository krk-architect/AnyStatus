
param (
    [string]$Config = 'Release',
    [string]$OutputPath = 'C:\Source\github\krk-architect\publish\AnyStatus'
)

dotnet publish .\src\Apps\Windows\AnyStatus.Apps.Windows\AnyStatus.Apps.Windows.csproj -c $Config -o "$OutputPath"