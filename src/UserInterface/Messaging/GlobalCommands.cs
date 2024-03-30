using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Commands;

namespace ApertureMessenger.UserInterface.Messaging;

public class GlobalCommands
{
    public static readonly ICommand[] Commands =
    [
        new DirectMessage(),
        new Exit()
    ];
}