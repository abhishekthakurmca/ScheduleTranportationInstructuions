using Application.Common.CQRS.ScheduleTransports.Commands;
using Application.Common.CQRS.ScheduleTransports.Querys;
using Application.Common.Fluent_Validations.ScheduleTransporterValidations;
using Application.Common.Models;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ScheduleTransportationInstructions.API.Endpoints.ScheduleTransporters
{
    [Route("api/schedule")]
    [Tags("schedule")]
    public class GetAll : EndpointBaseAsync.WithoutRequest.WithResult<IEnumerable<GetScheduledInstructionDTO>>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GetAll> _logger;

        public GetAll(IMediator mediator, ILogger<GetAll> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get the all data of Schedule Transportation Instructions.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async override Task<IEnumerable<GetScheduledInstructionDTO>> HandleAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                // Log a message when the Get method is called.
                _logger.LogInformation("GetAll method is called to get all Schedule Transportation Instructions");

                // Execute the query and return the result.
                return await _mediator.Send(new GetScheduleTransportListQuery());
            }
            catch (Exception ex)
            {
                // Log an error message if an exception is thrown.
                _logger.LogError($"Error in Get method: {ex.Message}");
                throw; // Re-throw the exception for further handling, or use custom error handling here.
            }
        }
    }

    [Route("api/schedule")]
    [Tags("schedule")]
    public class GetById : EndpointBaseAsync.WithRequest<int>.WithResult<GetScheduledInstructionDTO>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GetById> _logger;

        public GetById(IMediator mediator, ILogger<GetById> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get data By Id to retrieve Schedule Transportation Instruction.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async override Task<GetScheduledInstructionDTO> HandleAsync([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            try
            {
                // Log a message when GetById method is called.
                _logger.LogInformation($"GetById method is called to retrieve Schedule Transportation Instruction with ID {id}");

                // Execute a query by ID and return the result.
                return await _mediator.Send(new GetScheduleTransportById() { ScheduleTransportID = id });
            }
            catch (Exception ex)
            {
                // Log an error message if an exception is thrown.
                _logger.LogError($"Error in GetById method: {ex.Message}");
                throw; // Re-throw the exception for further handling, or use custom error handling here.
            }
        }
    }

    [Route("api/schedule")]
    [Tags("schedule")]
    public class Post : EndpointBaseAsync.WithRequest<CreateScheduleTransportCommand>.WithResult<IActionResult>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<Post> _logger;

        public Post(IMediator mediator, ILogger<Post> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Execute post method to create a new Schedule Transportation. 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async override Task<IActionResult> HandleAsync([FromBody] CreateScheduleTransportCommand request, CancellationToken cancellationToken = default)
        {
            try
            {
                // Log a message indicating that a new Schedule is being created.
                _logger.LogInformation("Post method is called to create a new Schedule Transportation Instruction");
                ScheduleValidations validations = new ();
                var addValidation = validations.Validate(request);
                if (!addValidation.IsValid)
                {
                    return BadRequest(addValidation);
                }

                // Execute the command to create a new Schedule and return the result.
                var createSchedule = await _mediator.Send(request);
                return Ok(createSchedule);
            }
            catch (Exception ex)
            {
                // Log an error message if an exception is thrown.
                _logger.LogError($"Error in Post method: {ex.Message}");
                throw; // Re-throw the exception for further handling, or use custom error handling here.
            }
        }
    }

    [Route("api/schedule")]
    [Tags("schedule")]
    public class Put : EndpointBaseAsync.WithRequest<UpdateScheduleTransportCommand>.WithResult<IActionResult>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<Put> _logger;

        public Put(IMediator mediator, ILogger<Put> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Execute method for update the Schedule Transportation.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        public async override Task<IActionResult> HandleAsync([FromBody] UpdateScheduleTransportCommand request, CancellationToken cancellationToken = default)
        {
            try
            {
                // Log a message indicating that an Schedule is being updated.
                _logger.LogInformation("Put method is called to update a Schedule Transportation Instruction");
                ScheduleValidations validations = new();
                var updateValidation = validations.Validate(request);
                if (!updateValidation.IsValid)
                {
                    return BadRequest(updateValidation);
                }

                // Execute the command to create a new Schedule and return the result.
                var updateSchedule = await _mediator.Send(request);
                return Ok(updateSchedule);
            }
            catch (Exception ex)
            {
                // Log an error message if an exception is thrown.
                _logger.LogError($"Error in Put method: {ex.Message}");
                throw; // Re-throw the exception for further handling, or use custom error handling here.
            }
        }
    }
}
