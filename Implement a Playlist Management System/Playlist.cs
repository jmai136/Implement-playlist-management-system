using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Implement_a_Playlist_Management_System
{
    // Queue: FIFO
    public enum PlaylistOrder
    {
        Forward,
        Backward
    }

    public class Playlist
    {
        private SongNode head = null;

        public SongNode GetFirst()
        {
            return head;
        }

        public SongNode GetLast()
        {

            SongNode current = head;
            // If there's already songs after the first song
            while (current.Next != null)
            {
                // Loops through until there's not a 'next song' anymore
                // Finds the last song in the queue
                current = current.Next;
            }

            return current;
        }

        bool IsSong(SongNode songToCheckTitle, string title)
        {
            return songToCheckTitle.Title == title;
        }

        // Loop through all the thingies, check based on title, keep the count as position
        // Return -1 if it's not in queue
        public int GetSongPositionByTitle(string title)
        {
            int position = -1;
            int index = 1;

            if (head == null)
                return position;

            SongNode current = head;
            // If there's already songs after the first song
            while (current.Next != null)
            {
                // This is for checking the first song
                if (current.Title == title)
                    return index;

                // Loops through until there's not a 'next song' anymore
                // Finds the last song in the queue

                // And then everything after
                index++;
                current = current.Next;
            }

            // This is for if there's only the head
            if (current.Title == title)
                return index;

            // Otherwise just return -1
            return position;
        }

        // Assumes that you'll always add a new song to the end of the playlist
        public bool AddSong(string title, string artist, double duration)
        {
            SongNode newSong = new SongNode(title, artist, duration);

            // When there's no songs already present
            if (head == null)
            {
                head = newSong;
            }
            else
            {
                // Grabs the first song
                SongNode current = head;
                // If there's already songs after the first song
                while (current.Next != null)
                {
                    // Loops through until there's not a 'next song' anymore
                    // Finds the last song in the queue
                    current = current.Next;
                }
                // Last song's next song will become our inputted song
                current.Next = newSong;

                // No point in adding next song for our new song since there isn't one
                // Need to keep track of our new song's song that comes before
                newSong.Prev = current;
            }

            return newSong.Equals(GetLast());
        }

        // add at specific position
        public void AddSong(string title, string artist, double duration, int position)
        {
            if (position < 1)
            {
                Console.WriteLine("Invalid position");
                return;
            }

            // https://www.geeksforgeeks.org/insertion-in-linked-list/
            SongNode newSong = new SongNode(title, artist, duration);

            if (head == null)
            {
                head = newSong;
                Console.WriteLine("Currently there are no songs, this song will automatically be the first.");
                return;
            }

            // Traverse the list to find the node before the insertion point, range from head to one step before the position you want to place
            SongNode current = head;
            // Keep in mind that this is for anything that's not position 1 because it will never run
            // Wait, let's say we slot in at 3 again
            // It goes to 2

            // Or we change the way this is done
            // If we had position - 1 
            // It'll never run because 1 is not less than 1
            // So it'll never add a second one
            for (int i = 1; i < position && current != null; ++i)
            {
                // Current song = A
                // Current song is B = A + 1
                current = current.Next;
            }

            // Current ain't no so it's gonna skip all the way down there, damnit

            // Current.Next could be null, and therefore that's how we get out of bounds
            // Works for both head because head could be null but current could also be the 'next song'

            // Problem, if we add a song to it, with a position, 1 - 1 = 0
            // Therefore the loop will never run
            // But current is still head and head isn't null
            if (current == null)
            {
                Console.WriteLine("Position is out of bounds, it will be added as the last song.");

                // Actually, just have it add to the end of the playlist
                AddSong(title, artist, duration);
                return;
            }

            // Pass in Tokio Funka at position 3
            // The Tragic Prince at position 3
            // Loop above grabs position 3 - 1
            // current song is set to whatever comes after 金木犀 so it'll not be null
            // Check to make sure that the current song isn't the first song otherwise handle that down there
            if (!current.Equals(head))
            {
                // Gotta set the new song too and set both ends
                // Tokio Funka's previous song will be 金木犀, its next song will be Tragic Prince
                newSong.Prev = current.Prev;
                newSong.Next = current;

                // So we get Tragic Prince
                // We set 金木犀's next song to Tokio Funka
                current.Prev.Next = newSong;

                // We then set Tragic Prince's previous song to Tokio Funka
                current.Prev = newSong;
                return;
            }

            // Replace first, problem
            // The loop will never run if position is passed at 1 due to the condition
            // Add to first song when there's already a first song:
            // new A's previous song will be A's previous song which should be null
            // new A's next song iscurrently what's A because A will come after new A
            newSong.Prev = current.Prev;
            newSong.Next = current;

            // Current A's previous song will be new A, because it's now 2
            current.Prev = newSong;

            // Don't have this check and it causes problems because second song will always replace first song
            if (position == 1)
                head = newSong;
        }

        // removing by title

        // https://www.geeksforgeeks.org/deletion-in-linked-list/

        /*
         To perform the deletion, If the position is 1, we update the head to point to the next node and delete the current head. For other positions, we traverse the list to reach the node just before the specified position. If the target node exists, we adjust the next of this previous node to point to next of next nodes, which will result in skipping the target node.
         */

        public bool RemoveByTitle(string title)
        {
            bool hasFoundTitle = false;

            if (title == "")
            {
                Console.WriteLine("No title to search for.");
                return hasFoundTitle;
            }
            // if the playlist contains a song node with this title, remove the song node
            if (head == null)
            {
                Console.WriteLine("Playlist is empty.");
                return hasFoundTitle;
            }

            SongNode current = head;

            //There's more than one element but we're deleting the head
            if (current.Next != null && (IsSong(current, title)))
            {
                // Grab whatever's next to current
                // Get the previous of that node set to null
                current.Next.Prev = null;

                // Set the head to it
                head = current.Next;
                return true;
            }

            // Always check forward, no need to worry about previous
            // Ok, this is gonna be an issue because there might only be one element
            // Below should handle it
            while (current.Next != null)
            {
                // So now current song's gonna be D = C's next song
                // Etc.
                current = current.Next;

                // Song B
                if (IsSong(current, title) && !hasFoundTitle)
                {
                    Console.WriteLine($"Song to be removed: {current.Title}");
                    hasFoundTitle = true;
                }

                // Since can't delete, set the previous song's next song to be the next song of the current song's upcoming song
                // B is current
                // B's previous is A and A's next song is B's next song which is C
                // A -> B -> C to A -> C
                // A <- B <- C to A <- C
                if (hasFoundTitle)
                {
                    current.Prev.Next = current.Next;

                    if (current.Next != null)
                        current.Next.Prev = current.Prev;

                    // Problem, next could be null so gotta take into account of that
                    // Would we have to set .Next to null
                    // Yea, gotta have checks for this
                }

                // Shouldn't have to worry about 'previous songs'
                // Because this means that we'd need to take into account of whatever's after C
                // Right?
            }

            if (hasFoundTitle)
                return hasFoundTitle;

            // Basically it's checking like this because it's possible head's the only element
            // If there's no next node then that means there's only one node and it'll skip down to here
            // Because hasFoundTitle will always be false
            if (IsSong(current, title))
            {
                head = null;
                return true;
            }

            return false;
        }

        public Playlist GetPlaylist(PlaylistOrder order = PlaylistOrder.Forward)
        {
            // Essentially how this works is that you're making an instance of a playlist in itself
            // If it's printing forwards, the playlist already exists so just grab this
            if (order == PlaylistOrder.Forward)
                return this;

            SongNode current = head;

            Stack<SongNode> backwardsPlaylist = new Stack<SongNode>();

            while (current != null)
            {
                backwardsPlaylist.Push(current);
                current = current.Next;
            }

            Playlist playlist = new Playlist();

            foreach (SongNode node in backwardsPlaylist)
                playlist.AddSong(node.Title, node.Artist, node.Duration);

            // Otherwise gonna have to make a new instance of the playlist
            // Then we return it and use it
            return playlist;
        }

        // Print the playlist forward and backward.
        // Modify this to have the parameter accept printing forward or backward then in here print based on that
        public void PrintPlaylist(PlaylistOrder order = PlaylistOrder.Forward)
        {
            if (head == null)
            {
                Console.WriteLine("Playlist is empty.");
                return;
            }

            // Use a stack to print out backwards: LIFO
            Playlist playlist = GetPlaylist(order);

            SongNode current = playlist.head;

            while (current != null)
            {
                Console.WriteLine($"{current.Title} by {current.Artist} ({current.Duration} mins)");
                current = current.Next;
            }
        }
    }
}
