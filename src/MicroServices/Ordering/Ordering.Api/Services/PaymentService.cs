using Grpc.Core;
using Grpc.Net.Client;
using PaymentService.Grpc;

namespace Ordering.Api.Services;

public class PaymentServiceImplementation(ILogger<PaymentServiceImplementation> logger)
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

        // Subskrypcja
        var streamCall = client.ProcessStream(request);

        // Asynchroniczne przetwarzanie komunikatow w strumieniu
        await foreach(var stage in streamCall.ResponseStream.ReadAllAsync<PaymentStage>())
        {
            logger.LogInformation("State: {stage} {description}", stage.Stage, stage.Description);
        }


        return response;
    }
}
