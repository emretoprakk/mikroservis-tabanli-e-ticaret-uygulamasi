using MultiShop.DtoLayer.CommentDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Services.CommentServices
{
    public class CommentService : ICommentService
    {
        private readonly HttpClient _httpClient;
        public CommentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5000/services/comment/"); // Ocelot URL

        }
        public async Task CreateCommentAsync(CreateCommentDto createCommentDto)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(createCommentDto);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("Comments", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"Yorum eklenirken hata oluştu: {ex.Message}");
            }
        }

        public async Task DeleteCommentAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"Comments?id={id}");
            response.EnsureSuccessStatusCode();
        }
        public async Task<List<ResultCommentDto>> GetAllCommentAsync()
        {
            var responseMessage = await _httpClient.GetAsync("Comments");
            responseMessage.EnsureSuccessStatusCode();
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(jsonData);
            return values ?? new List<ResultCommentDto>();
        }
        public async Task<GetByIdCommentDto> GetByIdCommentAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync($"Comments/{id}");
            responseMessage.EnsureSuccessStatusCode();
            return await responseMessage.Content.ReadFromJsonAsync<GetByIdCommentDto>();
        }
        public async Task<UpdateCommentDto> GetByIdForUpdateCommentAsync(string id)
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync($"Comments/{id}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var comment = JsonConvert.DeserializeObject<UpdateCommentDto>(jsonData);
                    if (comment != null)
                    {
                        return comment;
                    }
                    throw new Exception($"Comment with ID {id} is null after deserialization.");
                }

                throw new Exception($"Comment with ID {id} not found. StatusCode: {responseMessage.StatusCode}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting comment for update: {ex.Message}");
            }
        }

        public async Task UpdateCommentAsync(UpdateCommentDto updateCommentDto)
        {
            var jsonData = JsonConvert.SerializeObject(updateCommentDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("Comments", content);
            response.EnsureSuccessStatusCode();
        }
        public async Task<List<ResultCommentDto>> CommentListByProductId(string id)
        {
            var responseMessage = await _httpClient.GetAsync($"Comments/CommentListByProductId/{id}");
            responseMessage.EnsureSuccessStatusCode();
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(jsonData);
            return values ?? new List<ResultCommentDto>();
        }
    }
}
