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

    public override async Task ProcessStream(PaymentRequest request, IServerStreamWriter<PaymentStage> responseStream, ServerCallContext context)
    {
        var status = request.Amount < 1000 ? PaymentStatus.Accepted : PaymentStatus.Decliced;
        var reason = status == PaymentStatus.Decliced ? "Limit exceed" : string.Empty;

        var stage1 = new PaymentStage { Stage = "Initialized", Description = "Initializing..." };
        var stage2 = new PaymentStage { Stage = "Processing", Description = $"Payment {request.Amount:C2}..." };        
        var stage3 = new PaymentStage { Stage = "Done", Description = reason };

        await Task.Delay(Random.Shared.Next(1000, 3000) * 1); // symulacja opoznienia
        await responseStream.WriteAsync(stage1);

        await Task.Delay(Random.Shared.Next(1000, 3000) * 1); // symulacja opoznienia
        await responseStream.WriteAsync(stage2);

        await Task.Delay(Random.Shared.Next(1000, 3000) * 1); // symulacja opoznienia
        await responseStream.WriteAsync(stage3);
    }

    
}
