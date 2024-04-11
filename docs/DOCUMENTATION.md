# APERTURE MESSENGER DOCUMENTATION

This document attempts to be an exhaustive overview of internal Aperture Messenger functionality.

- author: **Ondřej Schlaichert**
- created in: **2024**
- made as a final school project for the subject PV at **SPŠE Ječná**
- licensed under the **[GNU General Public License v3.0](https://www.gnu.org/licenses/gpl-3.0.en.html)**

## Used tools and requirements
Aperture Messenger is built using C# with **.NET 8.0**.
### Requirements:
- **.NET 8.0** or compatible
#### NuGet packages:
- **Netwonsoft.Json** 13.0.3 or compatible
### Used tools:
- .NET
- NuGet
- JetBrains Rider
- Gnome Terminal
- GIMP

## Building
Before staring, ensure you have **.NET 8.0** or compatible installed and that you have the **Newtonsoft.Json 13.0.3** or compatible NuGet package installed.

After cloning the repository, Aperture Messenger can be built as any C# application by using the `dotnet publish` command in the project directory. The provided self-contained releases are built using the following commands:

- **Linux x64:** `dotnet publish -c Release -r linux-x64 --self-contained true /p:PublishSingleFile=true`
- **Linux ARM:** `dotnet publish -c Release -r linux-arm64 --self-contained true /p:PublishSingleFile=true`
- **MacOS ARM (Apple Silicon):** `dotnet publish -c Release -r osx-arm64 --self-contained true /p:PublishSingleFile=true`
- **Windows x64:** `dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true`

These commands output releases into the `/bin/Release` folder.

### Note about using Aperture Messenger on Microsoft Windows
While Aperture Messenger works on Windows, there are issues with displaying its command line interface, and using it is much more comfortable on good operating systems. Terminal imulators on Windows blink uncomfortably every time the interface of Aperture Messenger gets refreshed, but otherwise, all features are functional.

## Configuration
Builds of Aperture Messenger cannot and shouldn't be further configured - they're standalone applications that should just work without user interference. When building your own builds however, you might want to customise some functionality. This can be done by changing values in the [Settings class](../src/Settings.cs):

```C#
// The base ALMS API url Aperture Messenger should connect to
public const string AlmsUrl = "https://alms.oschl.eu/";

// The ALMS version this version of Aperture Messenger targets
public const string TargetAlmsVersion = "1.0.3";

// How often should the screen/messages be refreshed
public const int RefreshSleepSeconds = 2;
```
## Overview of architecture and functionality
The architecture of Aperture Messenger is split into two main parts:
- **ALMS connection**
- **User interface**

### ALMS connection
A collection of classes that handle communicating with the ALMS API. The class **Connector** is a singleton that includes a single **HttpClient** which is used for all communication with ALMS. It provides functions for sending GET and POST requests, as well as handling global errors, such as bad authorization or rate limiting.

For the rest of the application, the main way of interacting with ALMS is through **Repositories**. They provide access to ALMS **Conversations**, **Employees** and **Messages**, which are mapped to objects. Sending requests to ALMS is usually done by providing the repositories with a **Request** object.

Queries to ALMS throw custom exceptions, so errors with communication can be handled separately depending on their type.

### User interface
The user interface of the application is handled through **Views**, which are classes processed inside the Main() function of the application. The current view is stored in the static **Shared** class. **Views** and **Commands** can modify the active **View**, changing the content that's displayed to the user.

Each **View** handles basic logic for what the user can do to interact with it. Most **Views** provide the user with the ability to type commands or messages. Each view can pass an array of **Command** objects to the **CommandProcessor** class, which then evaluates user input and invokes any submitted command if its included in the array.

Commands are the main way of interacting with the application and support arguments. They must be prefixed with :, everything else is evaluated as not being a command. Commands can interact with ALMS, change the current view, and do various other tasks.

The :help command is available everywhere, and shows a list of all commands which are available in the current context, with definitions, required and optional arguments. 

### Refresher
Some views of Aperture Messenger need to get continuously updated with new information as it appears in ALMS, and the user interface needs to be redrawn periodically to handle when the window size changes. This is done using the **MessageRefresher** class, which refreshes the current **View** periodically depending on the time configured in the **Settings** class.

The refresher runs in a separate thread. Depending on the current view, the refresher will ask ALMS for new messages or updates, and then refresh the user interface with up-to-date information.

## Security
Aperture Messenger only should connect only to ALMS instances that utilise the HTTPS protocol. The "official" instance utilised by released builds (https://alms.oschl.eu/) supports HTTPS connection only.

## Project summary and recollection
I thought of the idea for this project only a few days before I started working on it, and I'm really satisfied with how it turned out. I really enjoyed working on the two individual parts, the client and the server, and making them work together.

It was interesting to see how the two languages I chose differ. One of the requirements for the school assignment was a minimum line of code count of 2500 lines, which would be very difficult to reach if I only worked on this project in JavaScript, but trivial if I worked on it in C#. Certain languages are simply a lot more verbose and vertical than others, showing just how silly and pointless this requirement is and how little it says about the overall complexity or quality of the project. In the end, the two parts combined consist of over 5000 lines of code, so this matters little to me.

One of my favourite aspects of working on ASCAMP was hosting. It was the first time I bought a domain and a VPS, and it was fun to setup Debian on it, figure out how to turn the webserver into a HTTPs one, setup MySQL, etc.

Overall, I am very happy with all the work I've done here.

## LOC count
One of the requirements for this project was a minimum line of code count. Below is the output of the [cloc](https://github.com/AlDanial/cloc) program with the `--by-file` option.

```
------------------------------------------------------------------------------------------------------------------
File                                                                           blank        comment           code
------------------------------------------------------------------------------------------------------------------
src/UserInterface/Authentication/Views/RegisterView.cs                            38              3            296
src/AlmsConnection/Repositories/ConversationRepository.cs                         58              3            224
src/UserInterface/Authentication/Views/LoginView.cs                               27              3            166
src/UserInterface/Console/ComponentWriter.cs                                      29              3            131
src/UserInterface/Messaging/Views/ConversationListView.cs                         14              3            109
src/UserInterface/Messaging/Views/ConversationView.cs                             21              3            107
src/UserInterface/Help/HelpView.cs                                                19              3            106
src/UserInterface/Messaging/Views/MessagingView.cs                                18              3            104
src/AlmsConnection/Repositories/EmployeeRepository.cs                             24              3            101
src/AlmsConnection/Repositories/MessageRepository.cs                              25              3             99
src/UserInterface/Authentication/Views/AuthenticationView.cs                      19              3             99
src/UserInterface/Messaging/Views/EmployeeListView.cs                             14              3             92
src/UserInterface/ErrorHandling/Views/ErrorView.cs                                15              3             83
src/UserInterface/Messaging/Commands/AddEmployeeToGroup.cs                         9              3             82
src/AlmsConnection/Connector.cs                                                   25             16             81
src/UserInterface/Messaging/Commands/CreateGroupConversation.cs                   11              3             80
src/UserInterface/Console/ConsoleWriter.cs                                        15              5             73
src/UserInterface/Messaging/Commands/DirectMessage.cs                              9              3             63
src/UserInterface/Messaging/Views/ColorListView.cs                                 8              0             62
src/UserInterface/Messaging/Commands/ConversationById.cs                           5              3             60
src/AlmsConnection/Authentication/Authenticator.cs                                13              8             56
src/UserInterface/CommandProcessor.cs                                             13              8             56
src/UserInterface/Messaging/Commands/ChangeColor.cs                                8              3             48
src/UserInterface/Objects/CommandFeedback.cs                                       6              3             47
src/AlmsConnection/Objects/Conversation.cs                                        11              0             44
src/AlmsConnection/Authentication/EmployeeCreator.cs                               9              8             40
src/UserInterface/Help/HelpCommand.cs                                              7              3             37
src/UserInterface/Fun/ApertureQuotes.cs                                           15              0             35
src/AlmsConnection/Objects/Message.cs                                              8              0             34
src/UserInterface/MessageRefresher.cs                                              6              4             34
src/AlmsConnection/Queries/Status.cs                                               7              0             30
src/UserInterface/Console/ConsoleColors.cs                                         4              3             29
src/UserInterface/Shared.cs                                                        5              3             29
src/AlmsConnection/Objects/Employee.cs                                             8              0             28
src/AlmsConnection/Helpers/ResponseParser.cs                                       6             13             26
src/Program.cs                                                                     3              0             26
src/UserInterface/Authentication/Commands/Exit.cs                                  5              4             25
src/UserInterface/Authentication/Views/ConnectionView.cs                           7              3             24
src/Logic/AlmsVersionComparer.cs                                                   5              3             23
src/Logic/ValidityCheckers/UsernameValidityChecker.cs                              7              3             23
src/UserInterface/Messaging/GlobalCommands.cs                                      2              3             23
src/AlmsConnection/Requests/RegisterRequest.cs                                     7              0             22
src/UserInterface/Messaging/Commands/Exit.cs                                       4              3             20
src/AlmsConnection/Exceptions/EmployeesDoNotExist.cs                               4              0             19
src/Logic/ValidityCheckers/NameValidityChecker.cs                                  2              3             19
src/UserInterface/Console/ConsoleReader.cs                                         4              3             19
src/AlmsConnection/Requests/AddEmployeeToGroupRequest.cs                           5              0             18
src/AlmsConnection/Requests/CreateGroupConversationRequest.cs                      5              0             18
src/AlmsConnection/Requests/LoginRequest.cs                                        5              0             18
src/AlmsConnection/Requests/SendMessageRequest.cs                                  5              0             18
src/AlmsConnection/Responses/ErrorResponse.cs                                      6              0             18
src/AlmsConnection/Responses/StatsResponse.cs                                      6              0             18
src/UserInterface/Messaging/Commands/ListPossibleColors.cs                         3              3             18
src/AlmsConnection/ConnectionTester.cs                                             3              7             17
src/AlmsConnection/Session.cs                                                      4              3             17
src/UserInterface/Messaging/Commands/Logout.cs                                     3              0             17
src/AlmsConnection/Requests/SetEmployeeColorRequest.cs                             4              0             16
src/Logic/ValidityCheckers/PasswordValidityChecker.cs                              4              3             16
src/AlmsConnection/Exceptions/BadRequestSent.cs                                    3              0             15
src/AlmsConnection/Exceptions/ConversationNotFound.cs                              3              0             15
src/AlmsConnection/Exceptions/ConversationNotGroup.cs                              3              0             15
src/AlmsConnection/Exceptions/EmployeeAlreadyInConversation.cs                     3              0             15
src/AlmsConnection/Exceptions/EmployeeDoesNotExist.cs                              3              0             15
src/AlmsConnection/Exceptions/FailedContactingAlms.cs                              3              0             15
src/AlmsConnection/Exceptions/InternalAlmsError.cs                                 3              0             15
src/AlmsConnection/Exceptions/InvalidColor.cs                                      3              0             15
src/AlmsConnection/Exceptions/MessageContentWasTooLong.cs                          3              0             15
src/AlmsConnection/Exceptions/TokenExpired.cs                                      3              0             15
src/AlmsConnection/Exceptions/TokenInvalid.cs                                      3              0             15
src/AlmsConnection/Exceptions/TokenMissing.cs                                      3              0             15
src/AlmsConnection/Exceptions/TooManyRequests.cs                                   3              0             15
src/AlmsConnection/Exceptions/UnhandledAuthenticationError.cs                      3              0             15
src/AlmsConnection/Exceptions/UnhandledResponseError.cs                            3              0             15
src/AlmsConnection/Responses/LoginResponse.cs                                      4              0             15
src/UserInterface/ErrorHandling/Commands/Exit.cs                                   3              3             15
src/AlmsConnection/Objects/VersionConflictResult.cs                                3              0             14
src/AlmsConnection/Responses/StatusResponse.cs                                     4              0             14
src/UserInterface/Authentication/Commands/Login.cs                                 3              3             13
src/UserInterface/Authentication/Commands/Register.cs                              3              3             13
src/UserInterface/ErrorHandling/Commands/Retry.cs                                  3              3             13
src/UserInterface/Messaging/Commands/ListAllConversations.cs                       3              3             13
src/UserInterface/Messaging/Commands/ListAllEmployees.cs                           3              3             13
src/UserInterface/Messaging/Commands/ListDirectConversations.cs                    3              3             13
src/UserInterface/Messaging/Commands/ListGroupConversations.cs                     3              3             13
src/UserInterface/Messaging/Commands/ListOnlineEmployees.cs                        3              3             13
src/UserInterface/Messaging/Commands/ListUnreadConversations.cs                    3              4             13
src/UserInterface/Console/FillerWriter.cs                                          3              3             11
src/Settings.cs                                                                    3              7              7
src/UserInterface/Interfaces/ICommand.cs                                           1              3              7
src/UserInterface/Interfaces/IHelpCommand.cs                                       1              3              6
src/UserInterface/Interfaces/IView.cs                                              1              3              6
src/AlmsConnection/Requests/IRequest.cs                                            1              0              5
src/UserInterface/Interfaces/IActionCommand.cs                                     1              3              5
------------------------------------------------------------------------------------------------------------------
SUM:                                                                             729            222           3690
------------------------------------------------------------------------------------------------------------------

```