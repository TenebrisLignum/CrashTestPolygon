using MediatR;

namespace Application.Messaging
{
    public interface ICommand: IRequest
    {
    }

    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
