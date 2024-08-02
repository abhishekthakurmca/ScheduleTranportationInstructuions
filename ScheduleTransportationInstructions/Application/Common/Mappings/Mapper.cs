using Application.Common.CQRS.Instructions.Commands;
using Application.Common.CQRS.ScheduleTransports.Commands;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<CreateInstructionCommand, Instruction>();
            CreateMap<DeleteInstructionCommand, Instruction>();
            CreateMap<UpdateInstructionCommand, Instruction>();
            CreateMap<ProductDto, Product>();
            CreateMap<Instruction, GetInstructionDTO>();
            CreateMap<Instruction, GetProductDTO>();
            CreateMap<Instruction, GetInstructionDTO>();
            CreateMap<Product, GetInstructionDTO>();
            CreateMap<Product, ProductDto>();
            CreateMap<Product, GetProductDTO>();
            CreateMap<Product, GetInstructionDTO>();
            CreateMap<GetScheduleTransportDTO, GetInstructionDTO>();
            CreateMap<GetScheduleTransportDTO, GetProductDTO>();
            CreateMap<GetScheduleTransportDTO, GetInstructionProductDTO>();
            CreateMap<GetProductDTO, Product>();
            CreateMap<Instruction, InstructionDTO>();
            CreateMap<ScheduleTransport, GetScheduleTransportDTO>();
            CreateMap<ScheduleTransport, ScheduleTransporterDto>();
            CreateMap<CreateScheduleTransportCommand, ScheduleTransport>();
            CreateMap<DeleteScheduleTransportCommand, ScheduleTransport>();
            CreateMap<UpdateScheduleTransportCommand, ScheduleTransport>();
        }
    }
}
