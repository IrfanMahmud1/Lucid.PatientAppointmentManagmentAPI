using Lucid.PAMS.Domain.Dtos;
using Lucid.PAMS.Domain.Entities;
using Lucid.PAMS.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lucid.PAMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(IPatientService patientService, ILogger<PatientsController> logger)
        {
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            _logger.LogInformation("Handling GET all patients request");
            var res = await _patientService.GetAllPatientsAsync();

            if (res.Success)
            {
                // expect Data to be IEnumerable<Patient>
                return Ok(res.Data);
            }

            _logger.LogError("GetAllPatients failed: {Message}", res.Message);
            return Problem(res.Message);
        }

        // GET: api/Patients/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPatient(Guid id)
        {
            _logger.LogInformation("Handling GET patient by id");

            if (id == Guid.Empty)
            {
                _logger.LogWarning("GetPatient called with empty Guid");
                return BadRequest(new { Message = "Invalid patient id" });
            }

            var res = await _patientService.GetPatientByIdAsync(id);

            if (res.Success)
            {
                return Ok(res.Data);
            }

            if (res.Message?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
            {
                _logger.LogInformation("Patient not found");
                return NotFound(new { Message = res.Message });
            }

            _logger.LogError("GetPatient failed : {Message}", res.Message);
            return Problem(res.Message);
        }

        // POST: api/Patients
        [HttpPost]
        public async Task<IActionResult> PostPatient([FromBody] CreatePatientDto patient)
        {
            _logger.LogInformation("Handling POST create patient request");

            if (patient == null)
            {
                _logger.LogWarning("PostPatient called with null body");
                return BadRequest(new { Message = "Patient is required" });
            }

            var res = await _patientService.CreatePatientAsync(patient);

            if (res.Success)
            {
                // Created resource should include location header
                if (res.Data is PatientDto created)
                {
                    return CreatedAtAction(nameof(GetPatient), new { id = created.Id }, created);
                }

                // Fallback if Data not returned
                return StatusCode(StatusCodes.Status201Created);
            }

            if (res.Message?.Contains("Duplicate", StringComparison.OrdinalIgnoreCase) == true)
            {
                _logger.LogInformation("Duplicate patient attempt");
                return Conflict(new { Message = res.Message });
            }

            _logger.LogError("CreatePatient failed: {Message}", res.Message);
            return Problem(res.Message);
        }

        // PUT: api/Patients/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutPatient(Guid id, [FromBody] UpdatePatientDto patient)
        {
            _logger.LogInformation("Handling PUT update patient");

            if (patient == null)
            {
                _logger.LogWarning("PutPatient called with null body");
                return BadRequest(new { Message = "Patient is required" });
            }

            if (id == Guid.Empty || patient.Id == Guid.Empty)
            {
                _logger.LogWarning("PutPatient called with invalid id");
                return BadRequest(new { Message = "Invalid patient id" });
            }

            if (id != patient.Id)
            {
                _logger.LogWarning("PutPatient id mismatch");
                return BadRequest(new { Message = "Route id and patient id do not match" });
            }

            var res = await _patientService.UpdatePatientAsync(patient);

            if (res.Success)
            {
                return Ok(res.Data != null ? res.Data : patient);
            }

            if (res.Message?.Contains("Duplicate", StringComparison.OrdinalIgnoreCase) == true)
            {
                _logger.LogInformation("Duplicate patient update attempt");
                return Conflict(new { Message = res.Message });
            }

            if (res.Message?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
            {
                _logger.LogInformation("Update target not found");
                return NotFound(new { Message = res.Message });
            }

            _logger.LogError("UpdatePatient failed : {Message}", res.Message);
            return Problem(res.Message);
        }

        // DELETE: api/Patients/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePatient(Guid id)
        {
            _logger.LogInformation("Handling DELETE patient");

            if (id == Guid.Empty)
            {
                _logger.LogWarning("DeletePatient called with empty Guid");
                return BadRequest(new { Message = "Invalid patient id" });
            }

            var res = await _patientService.DeletePatientAsync(id);

            if (res.Success)
            {
                return NoContent();
            }

            if (res.Message?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
            {
                _logger.LogInformation("Delete target not found");
                return NotFound(new { Message = res.Message });
            }

            _logger.LogError("DeletePatient failed {Message}", res.Message);
            return Problem(res.Message);
        }
    }
}