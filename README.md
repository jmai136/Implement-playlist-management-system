Project Description: You will create a C# program using doubly linked lists to manage a playlist of songs. Each node will represent a song with attributes such as title, artist, and duration. The program should support the following features:

Add a song to the playlist (beginning and end).
Remove a song by title.
Print the playlist forward and backward.
Basic Project Code:

```
public class SongNode
{
    public string Title { get; set; }
    public string Artist { get; set; }
    public double Duration { get; set; }
    public SongNode Next { get; set; }
    public SongNode Prev { get; set; }

    public SongNode(string title, string artist, double duration)
    {
        Title = title;
        Artist = artist;
        Duration = duration;
        Next = null;
        Prev = null;
    }
}

public class Playlist
{
    private SongNode head;

    public void AddSong(string title, string artist, double duration)
    {
        SongNode newSong = new SongNode(title, artist, duration);
        if (head == null)
        {
            head = newSong;
        }
        else
        {
            SongNode current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newSong;
            newSong.Prev = current;
        }
    }

    public void PrintPlaylist()
    {
        SongNode current = head;
        while (current != null)
        {
            Console.WriteLine($"{current.Title} by {current.Artist} ({current.Duration} mins)");
            current = current.Next;
        }
    }
}
```

Solution:
Full implementation should include methods for adding songs at specific positions, removing by title, and reverse printing.
You should test adding and removing songs, ensuring both forward and backward printing are correct.
