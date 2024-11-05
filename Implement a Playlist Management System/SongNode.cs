using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implement_a_Playlist_Management_System
{
    public class SongNode
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public double Duration { get; set; }
        public SongNode Next { get; set; }
        public SongNode Prev { get; set; }

        public bool Equals(Object obj)
        {
            if (obj == null || !(obj is SongNode))
                return false;
                
            SongNode other = obj as SongNode;

            return (this.Title == other.Title) && 
                (this.Artist == other.Artist) && 
                (this.Duration == other.Duration);
        }

        public SongNode(string title, string artist, double duration)
        {
            Title = title;
            Artist = artist;
            Duration = duration;
            Next = null;
            Prev = null;
        }
    }
}
