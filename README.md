# Aperture Messenger

<img src="./docs/img/logo.png" align="right" alt="Aperture Messenger logo" width="150" height="150">

> *Hello and, again, welcome to the Aperture Science computer-aided messaging platform.*

**Aperture Messenger** is a CLI messaging client for communication in Aperture Science, a fictional research facility from Valve's shared Portal and Half-Life universe.

Aperture Messenger connects to [**ALMS**](https://github.com/oschl-git/alms), a NodeJS API server which serves as a backend for its communication functionality.

## Features
- supports all ALMS communication features
  - direct messaging ✉️
  - group messaging 📪
  - changing user colours 🎨
  - registering accounts and authentication 👩‍🦰
- a fancy coloured CLI 🖥️
- live displaying of messages and information 🔄
- robust error handling 💪
- HTTPS 🔒

## Running
If you don't want to build Aperture Messenger yourself, you can download the [latest release for your operating system](https://github.com/oschl-git/aperture-messenger/releases/tag/3).

On Linux, it might be necessary to change the permissions of the downloaded file to make it executable. You can do so by running the following command: `sudo chmod +x <path to executable>`.

Once you've obtained an executable file, simply run it from your favourite terminal emulator.

## Detailed documentation
- for detailed information about how Aperture Messenger works, refer to the [documentation](docs/DOCUMENTATION.md). 

## Requirements
Note: **Built releases include all dependencies and can be ran as standalone applications without any extra requirements.**

- **.NET 8.0** or compatible

### NuGet packages:
- **Netwonsoft.Json** 13.0.3 or compatible

## Building
Before staring, ensure you have **.NET 8.0** or compatible installed and that you have the **Newtonsoft.Json 13.0.3** or compatible NuGet package installed.

Aperture Messenger can be built as any C# application by using the `dotnet publish` command. The provided self-contained releases are built using the following commands:

- **Linux x64:** `dotnet publish -c Release -r linux-x64 --self-contained true /p:PublishSingleFile=true`
- **Linux ARM:** `dotnet publish -c Release -r linux-arm64 --self-contained true /p:PublishSingleFile=true`
- **MacOS ARM (Apple Silicon):** `dotnet publish -c Release -r osx-arm64 --self-contained true /p:PublishSingleFile=true`
- **Windows x64:** `dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true`

These commands output releases into the `/bin/Release` folder.

While Aperture Messenger works on Windows, there are issues with displaying its command line interface, and using it is much more comfortable on good operating systems.

## Project information
Aperture Messenger, as well as ALMS, are my final projects for the subject PV at the **Secondary Technical School of Electrical Engineering Ječná** in Prague.

## License
Aperture Messenger is licensed under the [GNU General Public License v3.0](https://www.gnu.org/licenses/gpl-3.0.en.html).