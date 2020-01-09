using Forum.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Infra.Services
{
    public class ApiService
    {
        public HttpClient Client { get; }

        public ApiService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://localhost:44317/api/", UriKind.Absolute);
            Client = client;
        }

        public async Task<IEnumerable<Section>> GetSections()
        {
            var response = await Client.GetAsync("sections");

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Section>>(responseBody);
        }

        public async Task PostSection(object content, string token)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await Client.PostAsync("sections", new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        public async Task<Section> GetSectionById(long id)
        {
            var response = await Client.GetAsync($"sections/{id}");

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Section>(responseBody);
        }

        public async Task<IEnumerable<Category>> GetCategoriesFromSection(long sectionId)
        {
            var response = await Client.GetAsync("categories");

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(responseBody);

            return categories.Where(c => c.SectionId == sectionId);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var response = await Client.GetAsync("categories");

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Category>>(responseBody);
        }

        public async Task PostCategory(object content, string token)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await Client.PostAsync("categories", new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<Topic>> GetTopics()
        {
            var response = await Client.GetAsync("topics");

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Topic>>(responseBody);
        }

        public async Task<IEnumerable<Topic>> GetTopicsFromCategory(long categoryId)
        {
            var response = await Client.GetAsync("topics");

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var topics = JsonConvert.DeserializeObject<IEnumerable<Topic>>(responseBody);

            return topics.Where(c => c.CategoryId == categoryId);
        }

        public async Task PostTopic(object content, string token)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await Client.PostAsync("topics", new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        public async Task<Topic> GetTopicById(long id)
        {
            var response = await Client.GetAsync($"topics/{id}");

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Topic>(responseBody);
        }

        public async Task PostReply(object content, string token)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await Client.PostAsync("replies", new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        public async Task<User> GetCurrentLoggedUser(string token)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await Client.GetAsync("profile");

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(responseBody);
        }

        public async Task UpdateUserInfo(object content, string token)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await Client.PutAsync("replies", new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }
    }
}
