namespace Merchant.Common.Helpers;

public interface IResponseError
{
    string Message { get; set; }
    string DetailMessage { get; set; }
    int ErrorCode { get; set; }
}