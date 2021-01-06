using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Logic.Extension;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoggerService _loggerService;
        private readonly IAddressService _addressService;

        public HomeController(ILoggerService loggerService, IAddressService addressService)
        {
            _loggerService = loggerService;
            _addressService = addressService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetNearestAddressesByAddressAsync(BaseViewModel request)
        {
            if (request?.Addresses is null || request.Address is null)
                return View("Error");

            var addresses = request.Addresses.Split('\n').Select(x => x.Trim()).Take(100).ToArray(); // исскуственное ограничение на 100 записей максимум
            
            _loggerService.Info($"new request from UI with params: {request.SerializeObject()}");
            
            var result = await _addressService.GetNearestAddressByAddressAsync(addresses, request.Address);

            return View("Index", new BaseViewModel {Results = string.Join('\n', result.Select(x => $"{x.Key} - {x.Value}"))});
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}