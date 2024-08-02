using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Repository
{
    public class ScheduleTransportRepository : IScheduleTransportRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<ScheduleTransportRepository> _logger;
        public ScheduleTransportRepository(ApplicationDbContext applicationDbContext, ILogger<ScheduleTransportRepository> logger)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;

        }
        /// <summary>
        /// Adds a schedule transport to the database.
        /// </summary>
        /// <param name="scheduleTransport">The schedule transport object to be added.</param>
        /// <returns>The added schedule transport object.</returns>
        public async Task<ScheduleTransport> AddScheduleTransport(ScheduleTransport scheduleTransport)
        {
            // Add logging statement
            _logger.LogInformation("Adding a new schedule transport...");

            var result = await _applicationDbContext.ScheduleTransport.AddAsync(scheduleTransport);
            await _applicationDbContext.SaveChangesAsync();
            var instruction = await _applicationDbContext.Instructions.Include(x => x.Products).Include(x => x.Transporter).FirstOrDefaultAsync(x => x.Id == scheduleTransport.InstructionId);
            if (instruction.Products.Count() == instruction.Transporter.Count())
            {
                instruction.Status = "Scheduled";
            }
            await _applicationDbContext.SaveChangesAsync();
            // Add logging statement
            _logger.LogInformation("Schedule transport added successfully.");

            return result.Entity;
        }

        /// <summary>
        /// Deletes a schedule transport from the database.
        /// </summary>
        /// <param name="scheduleTransportId">The ID of the schedule transport to be deleted.</param>
        /// <returns>The deleted schedule transport object, or null if not found.</returns>
        public async Task<ScheduleTransport> DeleteScheduleTransport(int scheduleTransportId)
        {
            // Add logging statement
            _logger.LogInformation($"Deleting schedule transport with ID: {scheduleTransportId}...");

            var result = await _applicationDbContext.ScheduleTransport.FirstOrDefaultAsync(e => e.ScheduleTransportID == scheduleTransportId);

            if (result != null)
            {
                _applicationDbContext.ScheduleTransport.Remove(result);
                await _applicationDbContext.SaveChangesAsync();

                // Add logging statement
                _logger.LogInformation("Schedule transport deleted successfully.");

                return result;
            }

            // Add logging statement
            _logger.LogInformation("Schedule transport not found.");

            return null;
        }


        /// <summary>
        /// Retrieves a scheduled transport by its ID.
        /// </summary>
        /// <param name="id">The ID of the scheduled transport.</param>
        /// <returns>A Task representing the asynchronous operation that returns a GetScheduledInstructionDTO object.</returns>
        public async Task<GetScheduledInstructionDTO> GetScheduleTransportById(int id)
        {

            var query = await (from i in _applicationDbContext.Instructions
                               join p in _applicationDbContext.Product
                               on i.Id equals p.InstructionId
                               from t in _applicationDbContext.ScheduleTransport.Where(x => x.ProductId == p.ProductId).DefaultIfEmpty()
                               where t.ScheduleTransportID == id
                               select new GetScheduledInstructionDTO()
                               {
                                   Id = i.Id,
                                   InstructionDate = i.InstructionDate,
                                   ClientName = i.ClientName,
                                   ClientRef = i.ClientRef,
                                   BillingRef = i.BillingRef,
                                   PickupAddress = i.PickupAddress,
                                   DeliveryAddress = i.DeliveryAddress,
                                   Status = i.Status,
                                   Products = i.Products.Select(x => new GetInstructionProductDTO()
                                   {
                                       ProductId = x.ProductId,
                                       ProductCode = x.ProductCode,
                                       ProductDescription = x.ProductDescription,
                                       Qty = x.Qty,
                                       ScheduleTransportID = x.Transporter.Select(transport => transport.ScheduleTransportID).FirstOrDefault(),
                                       Transporter = x.Transporter.Select(transport => transport.Transporter).FirstOrDefault(),
                                       DateScheduled = x.Transporter.Select(transport => transport.DateScheduled).FirstOrDefault(),
                                   }).ToList(),
                               }).FirstOrDefaultAsync();
            _logger.LogError(" retrieved a scheduled transport by ID");
            return query;



        }

        /// <summary>
        /// Retrieves all scheduled transports.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation that returns a collection of GetScheduledInstructionDTO objects.</returns>
        public async Task<IEnumerable<GetScheduledInstructionDTO>> GetScheduleTransports()
        {
            var query = await (from i in _applicationDbContext.Instructions
                               join p in _applicationDbContext.Product
                               on i.Id equals p.InstructionId
                               from t in _applicationDbContext.ScheduleTransport.Where(x => x.ProductId == p.ProductId).DefaultIfEmpty()
                               select new GetScheduledInstructionDTO()
                               {
                                   Id = i.Id,
                                   InstructionDate = i.InstructionDate,
                                   ClientName = i.ClientName,
                                   ClientRef = i.ClientRef,
                                   BillingRef = i.BillingRef,
                                   PickupAddress = i.PickupAddress,
                                   DeliveryAddress = i.DeliveryAddress,
                                   Status = i.Status,
                                   Products = i.Products.Select(x => new GetInstructionProductDTO()
                                   {
                                       ProductId = x.ProductId,
                                       ProductCode = x.ProductCode,
                                       ProductDescription = x.ProductDescription,
                                       Qty = x.Qty,
                                       ScheduleTransportID = x.Transporter.Select(transport => transport.ScheduleTransportID).FirstOrDefault(),
                                       Transporter = x.Transporter.Select(transport => transport.ScheduleTransportID).FirstOrDefault() == 0 ? null : x.Transporter.Select(transport => transport.Transporter).FirstOrDefault(),
                                       DateScheduled = x.Transporter.Select(transport => transport.ScheduleTransportID).FirstOrDefault() == 0 ? null : x.Transporter.Select(transport => transport.DateScheduled).FirstOrDefault(),
                                   }).ToList(),
                               }).ToListAsync();
            var finalQuery = query.DistinctBy(x => x.Id);
            _logger.LogError("retrieved all scheduled transports");
            return finalQuery;

        }

        /// <summary>
        /// Checks if an instruction exists in the database.
        /// </summary>
        /// <param name="instructionId">The ID of the instruction.</param>
        /// <returns>A Task representing the asynchronous operation that returns a ScheduleTransport object if it exists, otherwise null.</returns>
        public async Task<ScheduleTransport> InstructionExists(int instructionId)
        {
            try
            {
                var dataExist = await _applicationDbContext.ScheduleTransport.Where(x => x.InstructionId == instructionId && x.ProductId == instructionId).FirstOrDefaultAsync();
                return dataExist;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking if an instruction exists");
                throw;
            }
        }

        /// <summary>
        /// Updates an existing ScheduleTransport in the database.
        /// </summary>
        /// <param name="scheduleTransport">The updated schedule transport object.</param>
        /// <returns>The updated ScheduleTransport object.</returns>
        public async Task<ScheduleTransport> UpdateScheduleTransport(ScheduleTransport scheduleTransport)
        {
            // Retrieve the existing record from the database based on its ID
            var result = await _applicationDbContext.ScheduleTransport.FirstOrDefaultAsync(e => e.ScheduleTransportID == scheduleTransport.ScheduleTransportID);

            if (result != null)
            {
                // Update the properties of the existing record with values from the provided schedule transport object
                result.DateScheduled = scheduleTransport.DateScheduled;
                result.Transporter = scheduleTransport.Transporter;

                // Log that an existing ScheduleTransport is being updated
                _logger.LogInformation("Updating an existing ScheduleTransport in the database");

                // Update the record in the database and save changes
                _applicationDbContext.Update(result);
                await _applicationDbContext.SaveChangesAsync();

                return result;
            }

            return null; // Return null if no matching record is found
        }
    }
}
