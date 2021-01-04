using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Extension;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model.DTO.Request;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly ILoggerService _loggerService;

        public AddressController(IAddressService addressService, ILoggerService loggerService)
        {
            _addressService = addressService;
            _loggerService = loggerService;
        }

        [HttpPost("GetNearestAddressByAddress")]
        public async Task<IDictionary<string, string>> GetNearestAddressesByAddressAsync(GetNearestAddressByAddressRequest request)
        {
            if (request.Addresses is null || request.ForAddress is null)
                return new Dictionary<string, string>();

            request.Addresses = request.Addresses.Take(100).ToArray(); // исскуственное ограничение на 100 записей максимум
            
            _loggerService.Info($"new request with params: {request.SerializeObject()}");
            
            return await _addressService.GetNearestAddressByAddressAsync(request.Addresses, request.ForAddress);
        }
    }
}