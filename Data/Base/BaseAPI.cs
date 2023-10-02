using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Data.Base
{
    public class BaseAPI : ControllerBase
    {
        private readonly IHttpClientFactory _httpClient;

        public BaseAPI(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<IActionResult> PostToApi(string controllerName, object model, string token = "")
        {
            try
            {
                var client = _httpClient.CreateClient("userAPI");

                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                }

                var response = await client.PostAsJsonAsync(controllerName, model);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return Ok(content);
                }

                return Unauthorized();
            }

            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
