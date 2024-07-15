using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TestAppAPI.Model;
using NUnit.Framework.Legacy;

namespace TestApp.Tests
{
    [TestFixture]
    public class StudyGroupTests
    {
        private HttpClient _client;
        private const string BaseUrl = "http://localhost:7140/api/studygroup"; // Update with your API base URL

        [SetUp]
        public void Setup()
        {
            // Configurar HttpClient para las pruebas de la API
            _client = new HttpClient();
            _client.BaseAddress = new Uri(BaseUrl);
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }

        [Test]
        public async Task TestCreateStudyGroup()
        {
            // Arrange
            var studyGroup = new StudyGroup(
                studyGroupId: 1,
                name: "Math Study Group",
                subject: Subject.Math,
                createDate: DateTime.Now,
                users: new List<User>() // Agrega usuarios si es necesario
            );

            var json = JsonConvert.SerializeObject(studyGroup);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("", content);
            response.EnsureSuccessStatusCode();

            // Assert new Nunit version use That instead IsTrue
            ClassicAssert.IsTrue(response.IsSuccessStatusCode);
        }

        [Test]
        public async Task TestGetStudyGroups()
        {
            // Arrange & Act
            var response = await _client.GetAsync("");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var studyGroups = JsonConvert.DeserializeObject<List<StudyGroup>>(responseContent);

            // Assert
            ClassicAssert.IsNotNull(studyGroups);
            ClassicAssert.IsTrue(studyGroups.Count > 0);
        }

        [Test]
        public async Task TestJoinStudyGroup()
        {
            // Arrange
            int studyGroupId = 1;
            int userId = 1;

            // Act
            var response = await _client.PostAsync($"/{studyGroupId}/join?userId={userId}", null);
            response.EnsureSuccessStatusCode();

            // Assert
            ClassicAssert.IsTrue(response.IsSuccessStatusCode);
        }

        [Test]
        public async Task TestLeaveStudyGroup()
        {
            // Arrange
            int studyGroupId = 1;
            int userId = 1;

            // Act
            var response = await _client.PostAsync($"/{studyGroupId}/leave?userId={userId}", null);
            response.EnsureSuccessStatusCode();

            // Assert
            ClassicAssert.IsTrue(response.IsSuccessStatusCode);
        }
    }
}