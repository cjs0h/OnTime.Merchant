using Merchant.Common.Extensions;
using Merchant.Common.Helpers;
using Microsoft.Extensions.Logging;

namespace Merchant.Application;

public static class ServicesHelper
{
    public static void HandleServiceError<T, TP>(ref ServiceResponse<T> serviceResult, ILogger<TP> logger, Exception ex, string uiErrorMessage)
    {
        logger.LogError($"{nameof(HandleServiceError)}: {ex}");
        if (serviceResult.Errors.Any())
        {
            logger.LogError($"Result errors: {string.Join(Environment.NewLine, serviceResult.Errors)}");
        }
        serviceResult
            .Failed()
            .WithMessage(uiErrorMessage)
            .WithException(ex)!
            .WithData(default);
    }

    public static void HandleServiceError<TP>(ref ServiceResponse serviceResult, ILogger<TP> logger, Exception ex, string uiErrorMessage)
    {
        logger.LogError($"{nameof(HandleServiceError)}: {ex}");
        if (serviceResult.Errors.Any())
        {
            logger.LogError($"Result errors: {string.Join(Environment.NewLine, serviceResult.Errors)}");
        }
        serviceResult
            .Failed()
            .WithException(ex)
            .WithMessage(uiErrorMessage);
    }
}