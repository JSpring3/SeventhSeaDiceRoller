using System;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace DramaDice.Shell
{
    internal static class Program
    {
        private static async Task<int>Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            if (!AnsiConsole.Profile.Capabilities.Interactive)
            {
                AnsiConsole.MarkupLine("[red]Environment does not support interaction.[/]");
                return await new ValueTask<int>(0);
            }

            RenderEmoji();
            
            Render("| Camille Du Vue |", new BarChart()
                .Width(60)
                .Label("[green bold underline]Traits[/]")
                .CenterLabel()
                .AddItem("Brawn", 44, Color.Green)
                .AddItem("Finesse", 4, Color.Green)
                .AddItem("Resolve", 2, Color.Green)
                .AddItem("Wits", 2, Color.Green)
                .AddItem("Panache", 2, Color.Green)
            );
            
            //:black_large_square:
            //:green_square:

            
            CreateBanner();
            
            var runApp = 0;
            while (runApp == 0)
            {
                runApp = await RunMenu();
            }
            return await ExitMessage(runApp);
        }
        
        private static void RenderEmoji()
        {
            AnsiConsole.Write(
                new Panel($"[white]Brawn: {Emoji.Known.GreenCircle} {Emoji.Known.GreenCircle} {Emoji.Known.RadioButton}    [/]")
                    .RoundedBorder());
            

        }
        
        private static void CreateBanner()
        {
            var font = FigletFont.Load(@"_Fonts\gothic.flf");
            AnsiConsole.Write(
                new FigletText(font, "Drama Dice")
                    .LeftAligned()
                    .Color(Color.Gold1));  
        }
        private static async ValueTask<int> RunMenu()
        {
            var playerType = ConfirmPlayerOrGameMasterSelection();
            AnsiConsole.MarkupLine($"[yellow]You are now a {playerType}[/]");
            
            var cmd = playerType switch
            {
                "Player Character" => ConfirmCharacterSelection(),
                "GM" => ConfirmSceneSelection(),
                _ => "Exit"
            };

            DisplayPlayerTypeSelection(playerType, cmd);

            var panel = new Panel("Hello World");
            panel.Border = BoxBorder.Rounded;

            return cmd switch
            {
                "Camille" => await new ValueTask<int>(2),
                "Kurt" => await new ValueTask<int>(2),
                "Scene 1" => await new ValueTask<int>(2),
                "Exit" => await new ValueTask<int>(1),
                _ => await GoBack()
            };
        }
        private static string ConfirmCharacterSelection()
        {
            AnsiConsole.WriteLine();
            AnsiConsole.Write(new Rule("[yellow]Character Selection[/]").RuleStyle("grey").LeftAligned());

            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .PageSize(10)
                    .Title("Select a 7th Sea character to use")
                    .AddChoices("Camille",
                        "Kurt",
                        "Vito",
                        "Go Back",
                        "Exit"));

            //ADD MENU ITEMS HERE
        }
        private static string ConfirmSceneSelection()
        {
            AnsiConsole.WriteLine();
            AnsiConsole.Write(new Rule("[yellow]Scene Selection[/]").RuleStyle("grey").LeftAligned());

            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .PageSize(10)
                    .Title("Select a Scene to select NPCs")
                    .AddChoices("Scene 1",
                        "Scene 2",
                        "Scene 3",
                        "Go Back",
                        "Exit"));

            //ADD MENU ITEMS HERE
        }
        private static string ConfirmPlayerOrGameMasterSelection()
        {
            AnsiConsole.WriteLine();
            AnsiConsole.Write(new Rule("[yellow]Player Or GM?[/]").RuleStyle("grey").LeftAligned());

            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .PageSize(10)
                    .Title("Are you a player or GM?")
                    .AddChoices("Player Character",
                        "GM",
                        "Exit"));

            //ADD MENU ITEMS HERE
        }
        private static async ValueTask<int> ExitMessage(int runApp)
        {
            var color = runApp switch
            {
                1 => @"lime",
                2 => @"lime",
                _ => @"red"
            };
            var msg = runApp switch
            {
                1 => @"Exiting...You can close the app now.",
                2 => @"Exiting...You can close the app now.",
                _ => @"An error occurred! Check your settings and try again."
            };
            if (runApp == 1)  AnsiConsole.Clear();
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine($"[{color}]{msg}[/]");

            if (runApp is 1 or 2) return await new ValueTask<int>(0);
            return await new ValueTask<int>(1);
        }

        private static async ValueTask<int> GoBack()
        {
            AnsiConsole.Clear();
            return await new ValueTask<int>(0);
        }
        
        private static void DisplayPlayerTypeSelection(string playerType, string selected)
        {
            AnsiConsole.Clear();
            if (selected == "Exit")
            {
                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine($"[lime]Exiting...You can close the app now.[/]");
            }
            if (playerType == "Player Character")
            {
                AnsiConsole.MarkupLine($"[yellow]You are rolling as {selected}[/]");
            }

            if (playerType == "GM" && selected != "Go Back")
            {
                AnsiConsole.MarkupLine($"[yellow]You are rolling as The GM NPCs in scene {selected}[/]");
            }
        }
        
        private static void Render(string title, IRenderable chart)
        {
            AnsiConsole.Write(
                new Panel(chart)
                    .Padding(1, 1)
                    .Header(title,Justify.Center)
                );
        }
    }
}
