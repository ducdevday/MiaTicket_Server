using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Business
{
    public interface IVnAddressBusiness
    {
        Task<string> GetProvincesAsync();
        Task<string> GetDisTrictsAsync(int provinceId);
        Task<string> GetWardsAsync(int districtId);
    }

    public class VnAddressBusiness : IVnAddressBusiness
    {
        private readonly HttpClient _httpClient;

        public VnAddressBusiness(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetProvincesAsync()
        {
            var response = await _httpClient.GetAsync("https://vapi.vnappmob.com/api/province/");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }

            return string.Empty;
        }

        public async Task<string> GetDisTrictsAsync(int provinceId)
        {
            var response = await _httpClient.GetAsync($"https://vapi.vnappmob.com/api/province/district/{provinceId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }

            return string.Empty;
        }

        public async Task<string> GetWardsAsync(int districtId)
        {
            var response = await _httpClient.GetAsync($"https://vapi.vnappmob.com/api/province/ward/{districtId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }

            return string.Empty;
        }
    }
}
