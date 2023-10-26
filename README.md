# AnyStatus

[AnyStatus](https://www.anystat.us) is a desktop application that brings together Metrics and Events from various sources into one simple dashboard. AnyStatus integrates with Azure DevOps, Jenkins, Docker, Binance, and more. You can also develop your own custom plugins and widgets.

## Build and Test

### Prerequisites

- Install `nvm` and Node.js version `>= 18.0.0`
- Install Appium `npm i --location=global appium`
- Install Appium Windows Driver `appium driver install --source=npm appium-windows-driver`
- Install [WinAppDriver v1.2.1](https://github.com/microsoft/WinAppDriver/releases/download/v1.2.1/WindowsApplicationDriver_1.2.1.msi)
- Visual Studio 2022 and ReSharper installed
- .NET 6

### Build

- Open solution in Visual Studio 2022 and Build
- If you get a MSB3270 error, you may need to unload (or remove) the `AnyStatus.Apps.Windows.Package` project

### Run the tests

- In Admin PowerShell terminal, run `& 'C:\pf86\Windows Application Driver\WinAppDriver.exe'`
- In VS2022, right click on the solution and select `Run Unit Tests`

## Download

Download and install AnyStatus from the [Microsoft Store](https://www.microsoft.com/en-us/p/anystatus/9p044vpk62sb). This allows you to always be on the latest version when we release new builds with automatic upgrades.

<a href="https://www.microsoft.com/en-us/p/anystatus/9p044vpk62sb"><img height="52" src="art/download.png"></img></a>

## Screenshots

![AnyStatus](https://www.anystat.us/assets/images/screenshots/anystatus-3.0.293-preview.png)

## Status

|Build|Status|
|-------|------|
|GitHub|![example workflow](https://github.com/anystatus/anystatus/actions/workflows/dotnet.yml/badge.svg)|
|Azure|[![Build Status](https://dev.azure.com/anystatus/AnyStatus/_apis/build/status/AnyStatus?repoName=AnyStatus%2FAnyStatus&branchName=main)](https://dev.azure.com/anystatus/AnyStatus/_build/latest?definitionId=1&repoName=AnyStatus%2FAnyStatus&branchName=main)|

|Release|Status|
|-------|------|
|Store|![Deployment](https://vsrm.dev.azure.com/anystatus/_apis/public/Release/badge/dca19306-f20b-4442-9d85-cd9c57ec81bf/1/5)|
|NuGet|![Deployment](https://vsrm.dev.azure.com/anystatus/_apis/public/Release/badge/dca19306-f20b-4442-9d85-cd9c57ec81bf/2/6)|

## License

AnyStatus is licensed under the [GNU General Public License v3.0](LICENSE)

Copyright © [Alon Amsalem](https://www.alonam.com) and contributors. All rights reserved.
