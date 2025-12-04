using Grpc.Net.Client;
using PaymentService.Grpc;

namespace Ordering.Api.Services;

public class PaymentServiceImplementation
{
    public async Task<PaymentResponse> MakeAsync(double totalAmount)
    {
        var request = new PaymentRequest
        {
            OrderId = Guid.NewGuid().ToString(),
            PaymentMethod = PaymentMethod.Blik,
            Amount = totalAmount
        };

        var channel = GrpcChannel.ForAddress("https://localhost:7014");

        var client = new PaymentService.Grpc.PaymentService.PaymentServiceClient(channel);
       
        var response = await client.ProcesAsync(request);

        return response;
    }
}
