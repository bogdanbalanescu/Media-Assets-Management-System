using ApplicationServices.Requests.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Infrastructure.ExceptionHandlers
{
    public class ExceptionHandlerMediatorPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        // TODO: add logger

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch(Exception ex)
            {
                if (ex is RequestException)
                    throw;
                throw new ServerException(ex.Message);
            }
        }
    }
}
