using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IInstructionRepository
    {
        Task<IEnumerable<Instruction>> GetInstructions();
         Task<Instruction> GetInstructionById(int instructionId);
        Task<Instruction> AddInstruction(Instruction instruction);
        Task<Instruction> UpdateInstruction(Instruction instruction);
        Task<Instruction> DeleteInstruction(int id);
        Task saveChanges();
    }
}
