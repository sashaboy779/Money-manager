using AutoMapper;
using BusinessLogicLayer.Dto.ReportDtos;
using BusinessLogicLayer.Dto.OperationDtos;
using BusinessLogicLayer.Dto.WalletDtos;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.UnitOfWork;
using System;
using System.Linq;
using System.Collections.Generic;
using BusinessLogicLayer.Resources;

namespace BusinessLogicLayer.Services
{
    public class ReportService : Service, IReportService
    {
        public ReportService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public ReportDto CreateReport(int userId, ReportRequestDto request)
        {
            Func<OperationDto, bool> searchPredicate;

            if (request.TimeRange == TimeRangeDto.Day)
            {
                searchPredicate = x => x.OperationDate.Date == request.DateInRange.Date;
            }
            else if (request.TimeRange == TimeRangeDto.Month)
            {
                searchPredicate = x => x.OperationDate.Year == request.DateInRange.Year
                                    && x.OperationDate.Month == request.DateInRange.Month;
            }
            else if (request.TimeRange == TimeRangeDto.Year)
            {
                searchPredicate = x => x.OperationDate.Year == request.DateInRange.Year;
            }
            else
            {
                searchPredicate = x => DateTime.Compare(x.OperationDate, request.StartingDate) >= 0
                                    && DateTime.Compare(x.OperationDate, request.EndingDate) <= 0;
            }

            return CreateReport(userId, request, searchPredicate);
        }

        private ReportDto CreateReport(int userId, ReportRequestDto request, 
                                       Func<OperationDto, bool> searchPredicate)
        {
            var wallet = VerifyUserWallet(userId, request.WalletId);

            var filteredOperations = 
                wallet.Operations.Where(searchPredicate).OrderBy(x => x.OperationDate).ToList();
            var lastOperation = filteredOperations.LastOrDefault();

            return new ReportDto
            {
                Name = GetReportName(request.TimeRange, request),
                Currency = wallet.Currency,
                OpeningBalance = filteredOperations.FirstOrDefault()?.CurrentBalance,
                EndingBalance = lastOperation?.CurrentBalance + lastOperation?.Amount,
                Income = SumAmount(filteredOperations, x => x.Amount > 0),
                Expense = SumAmount(filteredOperations, x => x.Amount < 0)
            };
        }

        private WalletDto VerifyUserWallet(int userId, int walletId)
        {
            var userWallet = GetUserWallets(userId).SingleOrDefault(x => x.WalletId == walletId);

            if (userWallet == null)
            {
                throw new WalletNotFoundException(String.Format(ServiceMessages.WalletNotFound, walletId));
            }

            return Mapper.Map<WalletDto>(userWallet);
        }

        private decimal SumAmount(IEnumerable<OperationDto> operations, Func<OperationDto, bool> predicate)
        {
            return operations.Where(predicate).Sum(y => y.Amount);
        }

        private string GetReportName(TimeRangeDto timeRange, ReportRequestDto request)
        {
            return timeRange switch
            {
                TimeRangeDto.Day => String.Format(ServiceMessages.DailyReport , request.DateInRange.Date.ToShortDateString()),
                TimeRangeDto.Month => String.Format(ServiceMessages.MonthlyReport , request.DateInRange.Month),
                TimeRangeDto.Year => String.Format(ServiceMessages.YearlyReport , request.DateInRange.Year),
                _ => String.Format(ServiceMessages.CustomReport , request.StartingDate.Date.ToShortDateString(), 
                                   request.EndingDate.Date.ToShortDateString())
            };
        }
    }
}
