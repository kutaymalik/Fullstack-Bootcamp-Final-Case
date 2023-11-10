using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Operation.Validation;
using System.ComponentModel.DataAnnotations;

namespace OrderAutomationsProject.Operation.Operations.CardOperations.Commands;

public class UpdateCardCommandHandler : IRequestHandler<UpdateCardCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public UpdateCardCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse> Handle(UpdateCardCommand request, CancellationToken cancellationToken)
    {
        // Finding required Card to Card Table
        Card entity = await unitOfWork.CardRepository.GetByIdAsync(request.Id, cancellationToken);

        if (entity == null || entity.DealerId != sessionService.CheckSession().sessionId)
        {
            var res = new ApiResponse("Record not found!");
            res.Success = false;
            return res;
        }

        entity.ExpenseLimit = request.Model.ExpenseLimit != 0 ? request.Model.ExpenseLimit : entity.ExpenseLimit;

        unitOfWork.CardRepository.Update(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        // Returning response as ApiResponse type
        return new ApiResponse();
    }
}
