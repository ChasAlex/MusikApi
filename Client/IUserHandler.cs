using Client.Models.Viewmodels;

namespace Client
{
    interface IUserHandler
    {
        public void ArtistById(User loggedInUser) { }
        public void GenresByid(User loggedInUser) { }
        public void SongById(User loggedInUser) { }
        public void ConnectUserToArtist(User loggedInUser) { }
        public void ConnectUserToGenre(int id, string genre) { }
        public void ConnectUserToSong(int id, string song) { }
        public void GettingInfoFromArtist(string artist) { }
    }
}
