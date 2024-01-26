using Database.Data;
using Database.Data.Interfaces;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace MusikApiIntegrationsTests
{
    public class PersonRepositoryTests
    {

        [Fact]
        public async Task GetAllUsers_ShouldReturnAllUsers()
        {

            // Arrange 
            DbContextOptions<MusicContext> options = new DbContextOptionsBuilder<MusicContext>()
                .UseInMemoryDatabase("Test-GetAllUsers")
                .Options;

            var context = new MusicContext(options);
            IPersonRepo personRepo = new PersonRepo(context);


            context.Users.AddRange(
                new User { Id = 10, Fullname = "Andreas" },
                new User { Id = 5, Fullname = "Alex" },
                new User { Id = 1, Fullname = "Maria" },
                new User { Id = 15, Fullname = "Henry" });

            context.SaveChanges();

            // Act 
            var result = await personRepo.GetAllUsers();


            // Assert
            Assert.Equal(4, result.Count);

        }


        [Fact]
        public async Task GetAllGenresByPersonId_ShouldReturnAllGenresForPersonWithId_1()
        {

            // Arrange 
            DbContextOptions<MusicContext> options = new DbContextOptionsBuilder<MusicContext>()
                .UseInMemoryDatabase("Test-GetAllGenresByPersonId")
                .Options;

            var context = new MusicContext(options);
            IPersonRepo personRepo = new PersonRepo(context);

            context.Users.Add(new User { Id = 10, Fullname = "Henry" });

            context.UserGenres.AddRange(
                new UserGenre { UserId = 1, GenreId = 1 },
                new UserGenre { UserId = 1, GenreId = 2 });

            context.Genres.AddRange(
                new Genre { Id = 1, Title = "Schlager" },
                new Genre { Id = 2, Title = "Rock" });

            context.SaveChanges();

            // Act 
            var result = await personRepo.GetAllGenresByPersonId(1);


            // Assert
            Assert.Equal("Rock", result[1].Title);
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].Id);

        }


        [Fact]
        public async Task AddUserGenreAsync_ShouldReturnTheNewCreatedUserGenre_CheckingForNutNull_UserId1_GenreId3()
        {
            {

                // Arrange 
                DbContextOptions<MusicContext> options = new DbContextOptionsBuilder<MusicContext>()
                    .UseInMemoryDatabase("Test-AddGenre")
                    .Options;

                var context = new MusicContext(options);
                IPersonRepo personRepo = new PersonRepo(context);

                context.Users.Add(new User { Id = 1, Fullname = "Henry" });
                context.Genres.Add(new Genre { Id = 3, Title = "Dansband" });

                var addGenre = new UserGenre { UserId = 1, GenreId = 3 };

                context.SaveChanges();


                // act
                var result = await personRepo.AddUserGenreAsync(addGenre);


                // Assert
                Assert.NotNull(result);
                Assert.Equal(1, result.UserId);
                Assert.Equal(3, result.GenreId);

            }



        }
    }
}