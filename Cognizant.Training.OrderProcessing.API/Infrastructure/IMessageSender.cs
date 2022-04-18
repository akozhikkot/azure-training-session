
namespace Cognizant.Training.OrderProcessing.API.Infrastructure
{
    public interface IMessageSender
    {
        Task Send<T>(T message);
    }
}