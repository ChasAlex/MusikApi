using API;
using API.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using MusikApi.tests;
using Newtonsoft.Json;

namespace MusikApiIntegrationsTests
{
    public class IntegrationsTests : IClassFixture<CustomWebApplicationFactory>
    {

        private readonly WebApplicationFactory<Program> _factory;

        public IntegrationsTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }


        [Fact]
        public async Task GetInfoArtistAsync_ShouldReturnMarkoolioInfo_Name_Playcount_Summary()
        {

            HttpClient client = _factory.CreateClient();
            var response = await client.GetAsync("/artistinfo/{artist}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetInfoArtistViewmodel>(content);

            Assert.NotNull(result);
            Assert.Equal("Markoolio", result.Name);
            Assert.Equal("899076", result.Playcount);
            Assert.Equal("Summary", result.Summary);

        }


        [Fact]
        public async Task GetTopTracksByGenreAsync_ShouldReturnTopTracksByGenre_Name()
        {


            HttpClient client = _factory.CreateClient();
            var response = await client.GetAsync("/genre/{genre}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<GetTopTracksByGenreViewmodel>>(content);


            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.Equal("Hotter Than Hell", result[3].Name);

        }


        [Fact]
        public async Task GetTopTrackByArtistAsync_ShouldReturnAllTopTracksForASpecifiedArtist()
        {


            HttpClient client = _factory.CreateClient();
            var response = await client.GetAsync("/artist/{artist}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<GetTopTrackByArtistViewmodel>>(content);


            Assert.NotNull(result);
            Assert.Equal("Mamma Mia", result[2].Name);
            Assert.Equal("7327889", result[1].Playcount);

        }

    }
}