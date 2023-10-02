using Data.Base;
using Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Softtek_Front.Models;

namespace Softtek_Front.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        public LoginController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Ingresar(LoginDTO login)
        {
            var baseAPI = new BaseAPI(_httpClient);
            var token = await baseAPI.PostToApi("Login", login);

            var loginResult = token as OkObjectResult;
            var objectResult = JsonConvert.DeserializeObject<LoginModel>(loginResult.Value.ToString());

            return View("~/Views/Home/Index.cshtml");
        }
    }
}
