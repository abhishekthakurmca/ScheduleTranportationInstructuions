
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class InstructionRepository : IInstructionRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<InstructionRepository> _logger; // Logger instance

        public InstructionRepository(ApplicationDbContext applicationDbContext, ILogger<InstructionRepository> logger)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }

        /// <summary>
        /// This method is used to add a new instruction in the database.
        /// </summary>
        /// <param name="instruction">Object containing values</param>
        /// <returns>The added instruction</returns>
        public async Task<Instruction> AddInstruction(Instruction instruction)
        {
            instruction.Status = "Pending";
            instruction.InstructionDate= DateTime.UtcNow;
            var result = await _applicationDbContext.Instructions.AddAsync(instruction);
            await saveChanges();

            _logger.LogInformation("New instruction added to the database."); // Log information

            return result.Entity;
        }

        /// <summary>
        /// This method is used to delete an instruction from the database.
        /// </summary>
        /// <param name="id">The ID of the instruction to be deleted</param>
        /// <returns>The deleted instruction</returns>
        public async Task<Instruction> DeleteInstruction(int id)
        {
            var result = await _applicationDbContext.Instructions.FirstOrDefaultAsync(e => e.Id == id);
            var results = _applicationDbContext.Instructions.Remove(result);
            _logger.LogInformation("Deleted an Instruction");
            await saveChanges();
            return results.Entity;
        }

        /// <summary>
        /// This method is used to get an instruction by its ID from the database.
        /// </summary>
        /// <param name="instructionId">The ID of the instruction to retrieve</param>
        /// <returns>The retrieved instruction</returns>
        public async Task<Instruction> GetInstructionById(int instructionId)
        {
            var data = await _applicationDbContext.Instructions.Include(x => x.Products).FirstOrDefaultAsync(a => a.Id == instructionId);
            _logger.LogInformation("Retrieved an Instruction by ID");

            return data;
        }


        /// <summary>
        /// This method is used to get all instructions from the database.
        /// </summary>
        /// <returns>A list of all instructions</returns>
        public async Task<IEnumerable<Instruction>> GetInstructions()
        {
            _logger.LogInformation("GetInstructions called to Get all instructions from database");
            return await _applicationDbContext.Instructions.Include(x => x.Products).ToListAsync();
        }


        /// <summary>
        /// This method is used to save changes made to the database.
        /// </summary>
        public async Task saveChanges()
        {
            await _applicationDbContext.SaveChangesAsync();
            _logger.LogInformation("Saved changes to the database");
        }

        /// <summary>
        /// This method is used to update an existing instruction in the database.
        /// </summary>
        /// <param name="instruction">The updated instruction object</param>
        /// <returns>The updated instruction</returns>
        public async Task<Instruction> UpdateInstruction(Instruction instruction)
        {
            foreach (var item in instruction.Products)
            {
                item.InstructionId = instruction.Id;
            }
            var result = await _applicationDbContext.Instructions.Include(x => x.Products).FirstOrDefaultAsync(e => e.Id == instruction.Id);
            if (result == null)
            {
                return null;
            }
            result.InstructionDate = instruction.InstructionDate;
            result.ClientName = instruction.ClientName;
            result.PickupAddress = instruction.PickupAddress;
            result.DeliveryAddress = instruction.DeliveryAddress;
            result.ClientRef = instruction.ClientRef;
            result.BillingRef = instruction.BillingRef;

            // Logging
            _logger.LogInformation("Updating an existing Instruction");

            IEnumerable<Product> existingProducts = result.Products;
            foreach (var item in existingProducts)
            {
                item.Instruction = null;
            }

            if (existingProducts == null) { return null; }
            if (existingProducts.Count() > 0)
            {
                foreach (var product in existingProducts)
                {
                    _applicationDbContext.Product.Remove(product);
                    await saveChanges();
                }
            }

            foreach (var product in instruction.Products)
            {
                _applicationDbContext.Product.Add(product);
                await saveChanges();
            }

            return result;
        }

    }
}
