using Business.Contracts;
using Business.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenityController : ControllerBase
    {
        private readonly IAmenityRepository _amenityRepository;

        public AmenityController(IAmenityRepository amenityRepository)
        {
            _amenityRepository = amenityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAminities()
        {
            var amenities = await _amenityRepository.GetAll();
            return Ok(amenities);
        }
    }

}
