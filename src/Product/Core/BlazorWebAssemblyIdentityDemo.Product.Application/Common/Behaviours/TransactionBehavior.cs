using BlazorWebAssemblyIdentityDemo.Product.Application.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Product.Application.Common.Behaviours
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
    {
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
        private readonly IDemoContext _demoContext;

        public TransactionBehavior(ILogger<TransactionBehavior<TRequest, TResponse>> logger, IDemoContext demoContext)
        {
            this._logger = logger;
            this._demoContext = demoContext;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response = default;

            try
            {
                await _demoContext.RetryOnExceptionAsync(async () =>
                {
                    _logger.LogInformation($"Begin Transaction : {typeof(TRequest).Name}");
                    await _demoContext.BeginTransactionAsync(cancellationToken);

                    response = await next();

                    await _demoContext.CommitTransactionAsync(cancellationToken);
                    _logger.LogInformation($"End transaction : {typeof(TRequest).Name}");
                });
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Rollback transaction executed {typeof(TRequest).Name}");
                await _demoContext.RollbackTransactionAsync(cancellationToken);
                _logger.LogError(ex.Message, ex.StackTrace);

                throw;
            }

            return response;
        }
    }
}
