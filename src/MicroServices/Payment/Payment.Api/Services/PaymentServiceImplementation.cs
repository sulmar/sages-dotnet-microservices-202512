using Grpc.Core;
using PaymentService.Grpc;

namespace Payment.Api.Services;

public class PaymentServiceImplementation : PaymentService.Grpc.PaymentService.PaymentServiceBase
{
    public override async Task<PaymentResponse> Proces(PaymentRequest request, ServerCallContext context)
    {
        // Processing...
        // await Task.Delay(2000);

        var status =  request.Amount < 1000 ? PaymentStatus.Accepted : PaymentStatus.Decliced;

        var response = new PaymentResponse
        {
            Status = status,
            Reason = status == PaymentStatus.Decliced ? "Limit exceed" : string.Empty
        };

        return response;

    }
}
