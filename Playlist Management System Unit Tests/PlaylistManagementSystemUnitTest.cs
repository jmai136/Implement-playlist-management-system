using Implement_a_Playlist_Management_System;

namespace PlaylistManagementSystemUnitTests
{
    // You should test adding and removing songs, ensuring both forward and backward printing are correct
    [TestClass]
    public class PlaylistManagementSystemUnitTest
    {
        [TestMethod]
        public void IsNewSongDefaultlyAtEndOfQueue()
        {
            Playlist playlist = new Playlist();
            Assert.IsTrue(playlist.AddSong("金木犀", "くじら feat Ado", 140));
        }

        [TestMethod]
        public void CanAddSongAtLastPositionIfNoSong()
        {
            Playlist playlist = new Playlist();

            playlist.AddSong("金木犀", "くじら feat Ado", 140, 5);
            Assert.AreEqual(1, playlist.GetSongPositionByTitle("金木犀"));
        }

        // Correct positioning check
        // Store the position, see if it matches the position in the list?
        [TestMethod]
        public void CanAddSongAtLastPositionIfOutOfBounds()
        {
            Playlist playlist = new Playlist();
            playlist.AddSong("Divine Bloodlines", "Michiru Yamane", 220);
            playlist.AddSong("金木犀", "くじら feat Ado", 140, 5);
            Assert.AreEqual(2, playlist.GetSongPositionByTitle("金木犀"));
        }

        [TestMethod]
        public void CanSlotInSongBetween()
        {
            Playlist playlist = new Playlist();
            // First song isn't being added correctly?
            playlist.AddSong("Divine Bloodlines", "Michiru Yamane", 220, 1);
            playlist.AddSong("金木犀", "くじら feat Ado", 140, 2);
            playlist.AddSong("The Tragic Prince", "Michiru Yamane", 220, 3);
            playlist.AddSong("Bloody Tears", "Michiru Yamane", 220, 4);

            // playlist.PrintPlaylist();

            playlist.AddSong("Tokio Funka", "EVO", 220, 3);

            // playlist.PrintPlaylist();

            Assert.AreEqual(3, playlist.GetSongPositionByTitle("Tokio Funka"));
        }

        [TestMethod]
        public void CanInsertAtFormerLastPosition()
        {
            Playlist playlist = new Playlist();
            // First song isn't being added correctly?
            playlist.AddSong("Divine Bloodlines", "Michiru Yamane", 220, 1);
            playlist.AddSong("金木犀", "くじら feat Ado", 140, 2);
            playlist.AddSong("The Tragic Prince", "Michiru Yamane", 220, 3);
            playlist.AddSong("Bloody Tears", "Michiru Yamane", 220, 4);

            // playlist.PrintPlaylist();

            playlist.AddSong("Tokio Funka", "EVO", 220, 4);

            // playlist.PrintPlaylist();

            Assert.AreEqual(4, playlist.GetSongPositionByTitle("Tokio Funka"));
        }

        [TestMethod]
        public void CanSlotInSongAtBeginningOfQueue()
        {
            Playlist playlist = new Playlist();
            playlist.AddSong("Divine Bloodlines", "Michiru Yamane", 220, 1);
            playlist.AddSong("金木犀", "くじら feat Ado", 140, 2);
            playlist.AddSong("Divine Bloodlines", "Michiru Yamane", 220, 3);

            playlist.AddSong("Tokio Funka", "EVO", 220, 1);

            Assert.AreEqual(1, playlist.GetSongPositionByTitle("Tokio Funka"));
        }

        [TestMethod]
        public void CanDeleteOnlySong()
        {
            Playlist playlist = new Playlist();
            if (playlist.AddSong("金木犀", "くじら feat Ado", 140))
                Assert.IsTrue(playlist.RemoveByTitle("金木犀"));
        }

        [TestMethod]
        public void CanDeleteFirstSong()
        {
            Playlist playlist = new Playlist();
            playlist.AddSong("金木犀", "くじら feat Ado", 140);
            playlist.AddSong("Meaningless", "Suisoh", 184);
            playlist.AddSong("Divine Bloodlines", "Michiru Yamane", 220);

            Assert.IsTrue(playlist.RemoveByTitle("金木犀"));
        }

        [TestMethod]
        public void CanDeleteExistingSong()
        {
            Playlist playlist = new Playlist();
            playlist.AddSong("金木犀", "くじら feat Ado", 140);
            playlist.AddSong("Meaningless", "Suisoh", 184);
            playlist.AddSong("Divine Bloodlines", "Michiru Yamane", 220);
            
            Assert.IsTrue(playlist.RemoveByTitle("Meaningless"));
        }

        [TestMethod]
        public void CanDeleteLastSong()
        {
            Playlist playlist = new Playlist();
            playlist.AddSong("金木犀", "くじら feat Ado", 140);
            playlist.AddSong("Meaningless", "Suisoh", 184);
            playlist.AddSong("Divine Bloodlines", "Michiru Yamane", 220);

            Assert.IsTrue(playlist.RemoveByTitle("Divine Bloodlines"));
        }

        // Printing backwards
        // If the first element in the backwards list is the last element in the song list
        // Let's say we remove a song at a specific positon
        // The sequence would still be the same
        [TestMethod]
        public void IsCorrectSequence()
        {
            Playlist playlist = new Playlist();
            playlist.AddSong("金木犀", "くじら feat Ado", 140);
            playlist.AddSong("Meaningless", "Suisoh", 184);
            playlist.AddSong("Divine Bloodlines", "Michiru Yamane", 220);

            Playlist forwardPlaylist = playlist.GetPlaylist();
            Playlist backwardPlaylist = playlist.GetPlaylist(PlaylistOrder.Backward);

            Assert.IsTrue(forwardPlaylist.GetLast().Equals(backwardPlaylist.GetFirst()) && backwardPlaylist.GetLast().Equals(forwardPlaylist.GetFirst()));
        }
    }
}