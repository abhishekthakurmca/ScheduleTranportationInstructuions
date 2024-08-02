using Application.Common.CQRS.Instructions.Commands;
using Application.Common.CQRS.Instructions.Query;
using Application.Common.Fluent_Validations.Instruction_Validations;
using Application.Common.Models;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ScheduleTransportationInstructions.API.Endpoints.Instruction
{
    [Route("api/instructions")]
    [Tags("Instructions")]
    public class GetAll : EndpointBaseAsync.WithoutRequest.WithResult<IEnumerable<GetInstructionDTO>>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GetAll> _logger; // Add logger

        public GetAll(IMediator mediator, ILogger<GetAll> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get the all data of Instructions and product.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async override Task<IEnumerable<GetInstructionDTO>> HandleAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                // Log a message when the Get All method is called.
                _logger.LogInformation("Get All method is called to get all Instructions");

                // Execute the query and return the result.
                return await _mediator.Send(new GetInstructionListQuery());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing GetAll Instructions.");
                throw; // Re-throw the exception
            }
        }
    }

    [Route("api/instructions")]
    [Tags("Instructions")]
    public class GetById : EndpointBaseAsync.WithRequest<int>.WithResult<GetInstructionDTO>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GetById> _logger; // Add logger

        public GetById(IMediator mediator, ILogger<GetById> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get data By Id to retrieve Instructions and product.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async override Task<GetInstructionDTO> HandleAsync([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            try
            {
                // Log a message when GetById method is called.
                _logger.LogInformation($"GetById method is called to retrieve Instruction with ID {id}");

                // Execute a query by ID and return the result.
                return await _mediator.Send(new GetInstructionsById() { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing GetById Instruction with Id: {Id}", id);
                throw; // Re-throw the exception
            }
        }
    }

    [Route("api/instructions")]
    [Tags("Instructions")]
    public class Post : EndpointBaseAsync.WithRequest<CreateInstructionCommand>.WithResult<IActionResult>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<Post> _logger; // Add logger

        public Post(IMediator mediator, ILogger<Post> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Execute post method to create a new Instructions and Product.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async override Task<IActionResult> HandleAsync([FromBody] CreateInstructionCommand request, CancellationToken cancellationToken = default)
        {
            try
            {
                // Log a message indicating that a new Instruction is being created.
                _logger.LogInformation("Post method is called to create a new Instruction");
                InsteructionValidator validations = new InsteructionValidator();
                var addValidation = validations.Validate(request);
                if (!addValidation.IsValid)
                {
                    return BadRequest(addValidation);
                }

                // Execute the command to create a new Instructions and return the result.
                var addInstruction = await _mediator.Send(request);
                return Ok(addInstruction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the Post request.");
                throw; // Re-throw the exception
            }
        }
    }

    [Route("api/instructions")]
    [Tags("Instructions")]
    public class Put : EndpointBaseAsync.WithRequest<UpdateInstructionCommand>.WithResult<IActionResult>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<Put> _logger; // Add logger

        public Put(IMediator mediator, ILogger<Put> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Execute method for update the Instructions and Product.
        /// </summary>
        /// <param name="requests"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        public async override Task<IActionResult> HandleAsync([FromBody] UpdateInstructionCommand requests, CancellationToken cancellationToken = default)
        {
            try
            {
                // Log a message indicating that an item is being updated.
                _logger.LogInformation("Put method is called to update a Instruction");
                InstructionValid validations = new ();
                var updateValidation =  validations.Validate(requests);
                if (!updateValidation.IsValid)
                {
                    return BadRequest(updateValidation);
                }

                // Execute the command to update a Instructions and return the result.
                var updateInstruction = await _mediator.Send(requests);
                return Ok(updateInstruction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the Put request.");
                throw; // Re-throw the exception
            }
        }
    }

    [Route("api/instructions")]
    [Tags("Instructions")]
    public class Delete : EndpointBaseAsync.WithRequest<int>.WithResult<InstructionDTO>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<Delete> _logger; // Add logger

        public Delete(IMediator mediator, ILogger<Delete> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Execute this method for delete an Instruction and her Product.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async override Task<InstructionDTO> HandleAsync([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            try
            {
                // Log a message indicating that an item is being Deleted.
                _logger.LogInformation("Delete method is called to delete a Instruction with her product");

                // Execute a query by ID for delete an Instruction and related Product and return the result.
                return await _mediator.Send(new DeleteInstructionCommand() { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing Delete Instruction with Id: {Id}", id);
                throw; // Re-throw the exception
            }
        }
    }
}
