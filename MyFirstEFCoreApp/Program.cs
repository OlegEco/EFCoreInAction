using MyFirstEFCoreApp;

Console.WriteLine(
    "Commands: l (list), u (change url), r (resetDb) and e (exit) - add -l to first two for logs");
Console.Write(
    "Checking if database exists... ");
Console.WriteLine(Commands.WipeCreateSeed(false) ? "created database and seeded it." : "it exists.");

do
{
    Console.WriteLine("> ");
    var command = Console.ReadLine();
    switch (command)
    {
        case "l":
            Commands.ListAll();
            break;
        case "u":
            Commands.ChangeWebUrl();
            break;
    }
} while (true);