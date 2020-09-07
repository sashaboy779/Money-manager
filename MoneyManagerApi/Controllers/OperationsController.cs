using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.Dto;
using BusinessLogicLayer.Dto.OperationDtos;
using BusinessLogicLayer.Services.Interfaces;
using MoneyManagerApi.Infrastructure.Constants;
using MoneyManagerApi.Models.OperationModels;
using MoneyManagerApi.Models.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MoneyManagerApi.Controllers
{
    [Route(Routes.Controller)]
    [ApiController]
    [Authorize]
    public class OperationsController : BaseController
    {
        private readonly IOperationService operationService;

        public OperationsController(IOperationService operationService, IMapper mapper, IUriService uriService) 
            : base(mapper, uriService)
        {
            this.operationService = operationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOperationAsync(CreateOperationModel operation)
        {
            var userId = GetUserId();
            var operationDto = Mapper.Map<OperationDto>(operation);

            var createdOperationDto = await operationService.CreateOperationAsync(userId, operationDto);
            var createdOperation = Mapper.Map<Operation>(createdOperationDto);

            return CreatedAtRoute(
                Routes.ShowOperation,
                new { id = createdOperation.OperationId },
                createdOperation);
        }

        [HttpGet(Routes.Id, Name = Routes.ShowOperation)]
        public async Task<IActionResult> ShowOperationAsync(int id)
        {
            var operationDto = await operationService.GetOperationAsync(GetUserId(), id);
            return Ok(Mapper.Map<Operation>(operationDto));
        }

        [HttpGet(Routes.WalletOperations)]
        public async Task<IActionResult> ShowAllWalletOperationsAsync([FromQuery]PaginationQuery pagination, 
            int walletId)
        {
            var paginationFilter = Mapper.Map<PaginationFilter>(pagination);
            var operationDto = await operationService.GetWalletOperationsAsync(GetUserId(), walletId, paginationFilter);

            if (!operationDto.Any())
            {
                return NotFound();
            }

            var operations = Mapper.Map<List<WalletOperationModel>>(operationDto);
            return IsPagingSpecified(pagination) ? Ok(CreatePagedResponse(pagination, operations)) : Ok(operations);
        }

        [HttpPut(Routes.Id)]
        public async Task<IActionResult> UpdateOperationAsync(int id, UpdateOperationModel operation)
        {
            operation.OperationId = id;
            var updateOperationDto = Mapper.Map<UpdateOperationDto>(operation);
            await operationService.UpdateOperationAsync(GetUserId(), updateOperationDto);

            return NoContent();
        }

        [HttpDelete(Routes.Id)]
        public async Task<IActionResult> DeleteOperationAsync(int id)
        {
            await operationService.DeleteOperationAsync(GetUserId(), id);
            
            return NoContent();
        }
    }
}
