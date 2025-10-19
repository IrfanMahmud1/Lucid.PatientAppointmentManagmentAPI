using Lucid.PAMS.Domain.Dtos;
using Lucid.PAMS.Domain.Entities;
using Lucid.PAMS.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lucid.PAMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ILogger<AppointmentsController> _logger;

        public AppointmentsController(IAppointmentService appointmentService, ILogger<AppointmentsController> logger)
        {
            _appointmentService = appointmentService ?? throw new ArgumentNullException(nameof(appointmentService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<IActionResult> GetAppointments()
        {
            _logger.LogInformation("Handling GET all appointments request");
            var res = await _appointmentService.GetAllAppointmentsAsync();

            if (res.Success)
            {
                // expect Data to be IEnumerable<Appointment>
                return Ok(res.Data);
            }

            _logger.LogError("GetAllAppointments failed: {Message}", res.Message);
            return Problem(res.Message);
        }

        // GET: api/Appointments/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAppointment(Guid id)
        {
            _logger.LogInformation("Handling GET appointment by id");

            if (id == Guid.Empty)
            {
                _logger.LogWarning("GetAppointment called with empty Guid");
                return BadRequest(new { Message = "Invalid appointment id" });
            }

            var res = await _appointmentService.GetAppointmentByIdAsync(id);

            if (res.Success)
            {
                return Ok(res.Data);
            }

            if (res.Message?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
            {
                _logger.LogInformation("Appointment not found");
                return NotFound(new { Message = res.Message });
            }

            _logger.LogError("GetAppointment failed : {Message}", res.Message);
            return Problem(res.Message);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<ResponseDto<IEnumerable<AppointmentDto>>>> FilterAppointments([FromQuery] FilterAppointmentDto filter)
        {
            if (filter.DoctorId == null && filter.AppointmentDate == null)
            {
                return BadRequest(ResponseDto<IEnumerable<AppointmentDto>>.Fail("At least one filter parameter (DoctorId or AppointmentDate) is required."));
            }

            var result = await _appointmentService.FilterAppointmentsAsync(filter);

            if (result == null)
            {
                return StatusCode(500, ResponseDto<IEnumerable<AppointmentDto>>.Fail("Failed to filter appointments"));
            }

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        // POST: api/Appointments
        [HttpPost]
        public async Task<IActionResult> PostAppointment([FromBody] BookAppointmentDto appointment)
        {
            _logger.LogInformation("Handling POST create appointment request");

            if (appointment == null)
            {
                _logger.LogWarning("PostAppointment called with null body");
                return BadRequest(new { Message = "Appointment is required" });
            }

            var res = await _appointmentService.BookAppointmentAsync(appointment);

            if (res.Success)
            {
                // Created resource should include location header
                if (res.Data is AppointmentDto created)
                {
                    return CreatedAtAction(nameof(GetAppointment), new { id = created.Id }, created);
                }

                // Fallback if Data not returned
                return StatusCode(StatusCodes.Status201Created);
            }

            if (res.Message?.Contains("Duplicate", StringComparison.OrdinalIgnoreCase) == true)
            {
                _logger.LogInformation("Duplicate appointment attempt");
                return Conflict(new { Message = res.Message });
            }

            _logger.LogError("CreateAppointment failed: {Message}", res.Message);
            return Problem(res.Message);
        }

        // PUT: api/Appointments/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutAppointment(Guid id, [FromBody] UpdateAppointmentDto appointment)
        {
            _logger.LogInformation("Handling PUT update appointment");

            if (appointment == null)
            {
                _logger.LogWarning("PutAppointment called with null body");
                return BadRequest(new { Message = "Appointment is required" });
            }

            if (id == Guid.Empty || appointment.Id == Guid.Empty)
            {
                _logger.LogWarning("PutAppointment called with invalid id");
                return BadRequest(new { Message = "Invalid appointment id" });
            }

            if (id != appointment.Id)
            {
                _logger.LogWarning("PutAppointment id mismatch");
                return BadRequest(new { Message = "Route id and appointment id do not match" });
            }

            var res = await _appointmentService.UpdateAppointmentAsync(appointment);

            if (res.Success)
            {
                return Ok(res.Data != null ? res.Data : appointment);
            }

            if (res.Message?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
            {
                _logger.LogInformation("Update target not found");
                return NotFound(new { Message = res.Message });
            }

            _logger.LogError("UpdateAppointment failed : {Message}", res.Message);
            return Problem(res.Message);
        }

    }
}