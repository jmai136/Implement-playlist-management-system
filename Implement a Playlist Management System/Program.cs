/*
 * Project Description: You will create a C# program using doubly linked lists to manage a playlist of songs. Each node will represent a song with attributes such as title, artist, and duration. The program should support the following features:

Add a song to the playlist (beginning and end).
Remove a song by title.
Print the playlist forward and backward.

 Solution:

Full implementation should include methods for adding songs at specific positions, removing by title, and reverse printing.
You should test adding and removing songs, ensuring both forward and backward printing are correct.
 */

/*
 Adding and removing should be handled via user input
Also have user be able to choose to print songs backwards and forwards

This should help with manual testing
But for better testing, let's use unit testing
 */

using Implement_a_Playlist_Management_System;
using System.Text.RegularExpressions;

// https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/
public partial class Program
{
    private static CancellationTokenSource cts = new CancellationTokenSource();
    private static Playlist playlist = new Playlist();

    // https://www.meziantou.net/handling-cancelkeypress-using-a-cancellationtoken.htm
    public static async Task Main(string[] args)
    {
        Console.CancelKeyPress += new ConsoleCancelEventHandler(HandleCancelKeyPress);
        await ManipulatePlaylist(args, cts.Token);
    }

    private static void HandleCancelKeyPress(object sender, ConsoleCancelEventArgs e)
    {
        e.Cancel = true;
        cts.Cancel();
    }

    private static async Task ManipulatePlaylist(string[] args, CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested) // https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken?view=net-8.0
            {
                Console.WriteLine(
                    "Please input either 1, 2, or 3, or Ctrl + C to terminate the program\n" +
                    "1. Add a song\n" +
                    "2. Remove a song by a specific title\n" +
                    "3. Print your playlist");

                string input = Console.ReadLine();
                
                // Depending on the key press
                // Run certain functions
                switch (input)
                {
                    case "1":
                        Console.WriteLine("Enter the title, artist, duration of the song, and optionally the position using these syntaxes:\n" +
                            "Title - Artist: Duration\nTitle - Artist: Duration (Positiion)");

                        // https://regex101.com/r/8NW0GF/2
                        string pattern = "^(.+) - (.+): (.+?)(?:\\s\\((\\d+)\\))?$";

                        string info = Console.ReadLine();

                        if (info == null || info == "")
                            break;

                        // https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.match?view=net-8.0
                        Match match = Regex.Match(info, pattern);

                        if (match.Success)
                        {
                            // Convert match group three to double
                            string title = match.Groups[1].Value;
                            string author =  match.Groups[2].Value;

                            // Use TimeSpan for conversion
                            // https://learn.microsoft.com/en-us/dotnet/api/system.timespan?view=net-8.0
                            //https://learn.microsoft.com/en-us/dotnet/api/system.timespan.totalseconds?view=net-8.0
                            double duration = TimeSpan.Parse(match.Groups[3].Value).TotalMinutes;

                            // https://stackoverflow.com/questions/9011524/regex-to-check-whether-a-string-contains-only-numbers
                            // https://stackoverflow.com/questions/7461080/fastest-way-to-check-if-string-contains-only-digits-in-c-sharp

                            if (match.Groups[4].Value == "")
                            {
                                playlist.AddSong(title, author, duration);
                                break;
                            }
                            
                            string pos = match.Groups[4].Value.Replace("(", "").Replace(")", "");
                            int position = (pos.All(char.IsDigit)) ? int.Parse(match.Groups[4].Value) : - 1;

                            if (position == -1)
                            {
                                playlist.AddSong(title, author, duration);
                                break;
                            }

                           playlist.AddSong(title, author, duration, position);
                        }

                        break;
                    case "2":
                        Console.WriteLine("Input the title: ");
                        playlist.RemoveByTitle(Console.ReadLine());
                        break;
                    case "3":
                        Console.WriteLine("1. Print playlist forward\n2. Print playlist backwards");

                        switch (Console.ReadLine())
                        {
                            case "1":
                                playlist.PrintPlaylist(PlaylistOrder.Forward);
                                break;
                            case "2":
                                playlist.PrintPlaylist(PlaylistOrder.Backward);
                                break;
                        }

                        break;
                }

                await Task.Delay(1000, cancellationToken);
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Terminating program...");
        }
    }
}