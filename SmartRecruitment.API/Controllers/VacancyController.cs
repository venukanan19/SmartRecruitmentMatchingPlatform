using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartRecruitment.API.Models.DTOs.Vacancy;
using SmartRecruitment.API.Services.Interfaces;

namespace SmartRecruitment.API.Controllers
{
    [ApiController]
    [Route("api/vacancies")]
    public class VacancyController : ControllerBase
    {
        private readonly IVacancyService _vacancyService;

        private readonly IValidator<CreateVacancyRequestDto>
            _createVacancyValidator;

        private readonly IValidator<UpdateVacancyRequestDto>
            _updateVacancyValidator;

        public VacancyController(
            IVacancyService vacancyService,
            IValidator<CreateVacancyRequestDto> createVacancyValidator,
            IValidator<UpdateVacancyRequestDto> updateVacancyValidator)
        {
            _vacancyService = vacancyService;
            _createVacancyValidator = createVacancyValidator;
            _updateVacancyValidator = updateVacancyValidator;
        }

        // GET: api/vacancies/{vacancyId}
        [HttpGet("{vacancyId:int}")]
        public async Task<IActionResult> GetById(
            int vacancyId)
        {
            var vacancy =
                await _vacancyService.GetByIdAsync(vacancyId);

            if (vacancy == null)
            {
                return NotFound(new
                {
                    message = "Vacancy not found."
                });
            }

            return Ok(vacancy);
        }

        // GET: api/vacancies/search
        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] VacancySearchRequestDto request)
        {
            var vacancies =
                await _vacancyService.SearchAsync(request);

            return Ok(vacancies);
        }

        // GET: api/vacancies/employer/mine
        [Authorize(Roles = "Employer")]
        [HttpGet("employer/mine")]
        public async Task<IActionResult> GetMyVacancies()
        {
            if (!TryGetUserId(out int userId))
            {
                return Unauthorized();
            }

            var vacancies =
                await _vacancyService
                    .GetEmployerVacanciesAsync(userId);

            return Ok(vacancies);
        }

        // POST: api/vacancies
        [Authorize(Roles = "Employer")]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateVacancyRequestDto request)
        {
            var validationResult =
                await _createVacancyValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    errors = validationResult.Errors
                        .Select(error => error.ErrorMessage)
                        .ToList()
                });
            }

            if (!TryGetUserId(out int userId))
            {
                return Unauthorized();
            }

            try
            {
                var vacancy =
                    await _vacancyService.CreateAsync(
                        userId,
                        request);

                return CreatedAtAction(
                    nameof(GetById),
                    new
                    {
                        vacancyId = vacancy.VacancyId
                    },
                    vacancy);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // PUT: api/vacancies/{vacancyId}
        [Authorize(Roles = "Employer")]
        [HttpPut("{vacancyId:int}")]
        public async Task<IActionResult> Update(
            int vacancyId,
            [FromBody] UpdateVacancyRequestDto request)
        {
            var validationResult =
                await _updateVacancyValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    errors = validationResult.Errors
                        .Select(error => error.ErrorMessage)
                        .ToList()
                });
            }

            if (!TryGetUserId(out int userId))
            {
                return Unauthorized();
            }

            try
            {
                var vacancy =
                    await _vacancyService.UpdateAsync(
                        userId,
                        vacancyId,
                        request);

                if (vacancy == null)
                {
                    return NotFound(new
                    {
                        message = "Vacancy not found."
                    });
                }

                return Ok(vacancy);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        private bool TryGetUserId(out int userId)
        {
            string? userIdValue =
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

            return int.TryParse(
                userIdValue,
                out userId);
        }
    }
}