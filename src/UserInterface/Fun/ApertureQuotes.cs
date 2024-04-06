namespace ApertureMessenger.UserInterface.Fun;

public static class ApertureQuotes
{
    public static string[] Quotes { get; } =
    {
        "The Enrichment Centre reminds you that the Weighted Companion Cube cannot speak.",

        "The Enrichment Centre reminds you that the cake is real.",

        "The Enrichment Centre reminds you that failure to to exceed 2500 LOC in your assignment will result in an " +
        "'unsatisfactory' mark on your official testing record followed by death. Good luck!",

        "The Enrichment Centre reminds you that if you don't have regular Aperture Messenger conversations, it's " +
        "because you're a worthless, unloved bird with a fat ugly beak.",

        "Please be advised that a noticeable taste of blood an unintended side effect of the Aperture Secure " +
        "Authentication Protocol, which may, in semi-rare cases, emancipate dental fillings, crowns, tooth enamel," +
        " and teeth.",

        "Remember: The Aperture Science Bring Your Daughter to Work Day is the perfect time to have her tested.",

        "The Enrichment Centre reminds you that while safety is one of many Enrichment Center goals, the Aperture " +
        "Laboratories Messaging Platform can and has caused permanent disabilities such as vaporization.",

        "Thank you for using Aperture Messenger. You, [louder]Subject Name Here[/louder], must be the pride of " +
        "[louder]Subject Hometown Here[/louder].",

        "The Enrichment Centre reminds you that when the messaging is over, you will be missed.",

        "The Enrichment Center is committed to the well being of all participants.",

        "Thank you for participating in this Aperture Science computer-aided enrichment activity.",

        "Please be advised that The Enrichment Centre is not responsible for any dissatisfactory emotions after " +
        "sending an unfunny meme to a group chat."
    };

    public static string GetRandomQuote()
    {
        var random = new Random();
        return Quotes[random.Next(0, Quotes.Length)];
    }
}