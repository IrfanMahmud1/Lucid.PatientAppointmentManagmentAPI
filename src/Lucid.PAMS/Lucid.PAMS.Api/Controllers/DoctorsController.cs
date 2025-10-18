using Lucid.PAMS.Domain.Dtos;
using Lucid.PAMS.Domain.Entities;
using Lucid.PAMS.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lucid.PAMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly ILogger<DoctorsController> _logger;

        public DoctorsController(IDoctorService doctorService, ILogger<DoctorsController> logger)
        {
            _doctorService = doctorService ?? throw new ArgumentNullException(nameof(doctorService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/Doctors
        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            _logger.LogInformation("Handling GET all doctors request");
            var res = await _doctorService.GetAllDoctorsAsync();

            if (res.Success)
            {
                // expect Data to be IEnumerable<Doctor>
                return Ok(res.Data);
            }

            _logger.LogError("GetAllDoctors failed: {Message}", res.Message);
            return Problem(res.Message);
        }

        // GET: api/Doctors/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetDoctor(Guid id)
        {
            _logger.LogInformation("Handling GET doctor by id");

            if (id == Guid.Empty)
            {
                _logger.LogWarning("GetDoctor called with empty Guid");
                return BadRequest(new { Message = "Invalid doctor id" });
            }

            var res = await _doctorService.GetDoctorByIdAsync(id);

            if (res.Success)
            {
                return Ok(res.Data);
            }

            _logger.LogError("GetDoctor failed : {Message}", res.Message);
            return Problem(res.Message);
        }

        // POST: api/Doctors
        [HttpPost]
        public async Task<IActionResult> PostDoctor([FromBody] CreateDoctorDto doctor)
        {
            _logger.LogInformation("Handling POST create doctor request");

            if (doctor == null)
            {
                _logger.LogWarning("PostDoctor called with null body");
                return BadRequest(new { Message = "Doctor is required" });
            }

            var res = await _doctorService.CreateDoctorAsync(doctor);

            if (res.Success)
            {
                // Created resource should include location header
                if (res.Data is DoctorDto created)
                {
                    return CreatedAtAction(nameof(GetDoctor), new { id = created.Id }, created);
                }

                // Fallback if Data not returned
                return StatusCode(StatusCodes.Status201Created);
            }

            _logger.LogError("CreateDoctor failed: {Message}", res.Message);
            return Problem(res.Message);
        }
    }
}