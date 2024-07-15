
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

using UserManagement;
using Newtonsoft.Json.Linq;
namespace TestProject1
{


    public class AccountControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public AccountControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        //[Fact]
        //public async Task Register_Login_CheckPermission_Flow()
        //{
        //    // Register a new user
        //    var registerResponse = await RegisterUser("testuser", "testuser@example.com", "Password123!", "GroupOwner");
        //    registerResponse.EnsureSuccessStatusCode();

        //    // Login with the new user
        //    var loginResponse = await LoginUser("testuser", "Password123!");
        //    loginResponse.EnsureSuccessStatusCode();

        //    var loginContent = await loginResponse.Content.ReadAsStringAsync();
        //    var loginResult = JsonConvert.DeserializeObject<LoginResult>(loginContent);

        //    // Set the token in the Authorization header
        //    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.Token);

        //    // Check permission (assuming some test data is already seeded)
        //    var checkPermissionResponse = await CheckPermission(1, 1, 1, 1, "View");
        //    Assert.Equal(System.Net.HttpStatusCode.Forbidden, checkPermissionResponse.StatusCode);

        //    // Map permissions
        //    var mapPermissionResponse = await MapPermissions(new MapPermissionModel
        //    {
        //        UserId = "testuser",
        //        GroupId = 1,
        //        EntityId = 1,
        //        ModuleId = 1,
        //        SubmoduleId = 1,
        //        PermissionId = 4 // "View" permission
        //    });
        //    mapPermissionResponse.EnsureSuccessStatusCode();

        //    // Check permission again
        //    checkPermissionResponse = await CheckPermission(1, 1, 1, 1, "View");
        //    Assert.Equal(System.Net.HttpStatusCode.OK, checkPermissionResponse.StatusCode);
        //}
        [Fact]
        public async Task Check_Permission_ReturnsOk()
        {
            // Arrange: Register and login user to get token
            var registerModel = new RegisterModel
            {
                Username = "Satish-tata",
                Email = "satish-tata@example.com",
                Password = "Test@123",
                Role = "User"
            };
            var registerContent = new StringContent(JsonConvert.SerializeObject(registerModel), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/useraccount/register", registerContent);
            registerModel = new RegisterModel
            {
                Username = "testuser",
                Email = "testuser@example.com",
                Password = "Test@123",
                Role = "GroupOwner"
            };
            registerContent = new StringContent(JsonConvert.SerializeObject(registerModel), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/useraccount/register", registerContent);

            var loginModel = new LoginModel
            {
                Username = "testuser",
                Password = "Test@123"
            };
            var loginContent = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");
            var loginResponse = await _client.PostAsync("/api/useraccount/login", loginContent);
            var loginResponseString = await loginResponse.Content.ReadAsStringAsync();
            var jwtToken = JsonConvert.DeserializeObject<JObject>(loginResponseString)["token"].ToString();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            var mapPermissionResponse = await MapPermissions(new MapPermissionModel
            {
                UserId = "Satish-tata",
                GroupId = 1,
                EntityId = 1,
                ModuleId = 1,
                SubmoduleId = 1,
                PermissionId = 4 // "View" permission
            });

            mapPermissionResponse.EnsureSuccessStatusCode();

            var loginModel1 = new LoginModel
            {
                Username = "Satish-tata",
                Password = "Test@123"
            };
            var loginContent1 = new StringContent(JsonConvert.SerializeObject(loginModel1), Encoding.UTF8, "application/json");
            var loginResponse1 = await _client.PostAsync("/api/useraccount/login", loginContent1);
            var loginResponseString1 = await loginResponse1.Content.ReadAsStringAsync();
            var jwtToken1 = JsonConvert.DeserializeObject<JObject>(loginResponseString1)["token"].ToString();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken1);
            // Check permission again
            var checkPermissionResponse = await CheckPermission(1, 1, 1, 1, "View");
            Assert.Equal(System.Net.HttpStatusCode.OK, checkPermissionResponse.StatusCode);
            var response = await _client.GetAsync("/api/useraccount/get-permissions");
            // Act
           //  response = await _client.GetAsync("/api/useraccount/check-permission?groupId=1&entityId=1&moduleId=1&submoduleId=1&permissionName=View");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Permission granted", responseString);
        }
        private async Task<HttpResponseMessage> RegisterUser(string username, string email, string password, string role)
        {
            var registerModel = new RegisterModel
            {
                Username = username,
                Email = email,
                Password = password,
                Role = role
            };
            var content = new StringContent(JsonConvert.SerializeObject(registerModel), Encoding.UTF8, "application/json");
            return await _client.PostAsync("/api/useraccount/register", content);
        }

        private async Task<HttpResponseMessage> LoginUser(string username, string password)
        {
            var loginModel = new LoginModel
            {
                Username = username,
                Password = password
            };
            var content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");
            return await _client.PostAsync("/api/useraccount/login", content);
        }

        private async Task<HttpResponseMessage> CheckPermission(int groupId, int entityId, int moduleId, int submoduleId, string permissionName)
        {
            return await _client.GetAsync($"/api/useraccount/check-permission?groupId={groupId}&entityId={entityId}&moduleId={moduleId}&submoduleId={submoduleId}&permissionName={permissionName}");
        }

        private async Task<HttpResponseMessage> MapPermissions(MapPermissionModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            return await _client.PostAsync("/api/useraccount/map-permissions", content);
        }
    }

    public class RegisterModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class MapPermissionModel
    {
        public string UserId { get; set; }
        public int GroupId { get; set; }
        public int EntityId { get; set; }
        public int ModuleId { get; set; }
        public int SubmoduleId { get; set; }
        public int PermissionId { get; set; }
    }

    public class LoginResult
    {
        public string Token { get; set; }
        public string UserId { get; set; }
    }

}