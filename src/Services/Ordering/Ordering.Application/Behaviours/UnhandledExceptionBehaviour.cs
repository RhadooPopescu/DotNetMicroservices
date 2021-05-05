using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Behaviours
{
    //This class manages the unhandled exceptions.
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        //Fields.
        private readonly ILogger<TRequest> logger;

        //Constructor.
        public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        //This method handles the unhandled exceptions, and logging the exceptions in the Logger class.
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                string requestName = typeof(TRequest).Name;
                logger.LogError(ex, "Application Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);
                throw;
            }
        }
    }
}
