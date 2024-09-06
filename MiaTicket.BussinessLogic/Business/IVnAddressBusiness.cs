using MiaTicket.BussinessLogic.Model;
using MiaTicket.BussinessLogic.Response;
using System.Text.Json;
using System.Net;
using Microsoft.AspNetCore.Http;
namespace MiaTicket.BussinessLogic.Business
{
    public interface IVnAddressBusiness
    {
        Task<GetProvincesResponse> GetProvincesAsync();
        Task<GetDistrictsResponse> GetDisTrictsAsync(int provinceId);
        Task<GetWardsResponse> GetWardsAsync(int districtId);
    }

    public class VnAddressBusiness : IVnAddressBusiness
    {
        private readonly HttpClient _httpClient;

        public VnAddressBusiness(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetProvincesResponse> GetProvincesAsync()
        {
            var response = await _httpClient.GetAsync("https://vapi.vnappmob.com/api/province/");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var jsonDocument = JsonDocument.Parse(content);
                var results = jsonDocument.RootElement.GetProperty("results").ToString();

                var provinces = JsonSerializer.Deserialize<List<ProvinceDto>>(results);
                if(provinces != null)
                    return new GetProvincesResponse(HttpStatusCode.OK, "Get Provinces Succeed", provinces);
            }
            return new GetProvincesResponse(HttpStatusCode.BadRequest, "Get Provinces Failed", []);
        }

        public async Task<GetDistrictsResponse> GetDisTrictsAsync(int provinceId)
        {
            var response = await _httpClient.GetAsync($"https://vapi.vnappmob.com/api/province/district/{provinceId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var jsonDocument = JsonDocument.Parse(content);
                var results = jsonDocument.RootElement.GetProperty("results").ToString();

                var districts = JsonSerializer.Deserialize<List<DistrictDto>>(results);
                if (districts != null)
                    return new GetDistrictsResponse(HttpStatusCode.OK, "Get Districts Succeed", districts);
            }
            return new GetDistrictsResponse(HttpStatusCode.BadRequest, "Get Districts Failed", []);
        }

        public async Task<GetWardsResponse> GetWardsAsync(int districtId)
        {
            var response = await _httpClient.GetAsync($"https://vapi.vnappmob.com/api/province/ward/{districtId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var jsonDocument = JsonDocument.Parse(content);
                var results = jsonDocument.RootElement.GetProperty("results").ToString();

                var wards = JsonSerializer.Deserialize<List<WardDto>>(results);
                if (wards != null)
                    return new GetWardsResponse(HttpStatusCode.OK, "Get Districts Succeed", wards);
            }
            return new GetWardsResponse(HttpStatusCode.BadRequest, "Get Districts Failed", []);
        }
    }
}
