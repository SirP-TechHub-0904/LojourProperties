using LojourProperties.Domain.Dtos.NotifyDtos;

namespace JurayUniversal.Application.Repository.NotifyRegister
{
    public interface INotifyRegisterService
    {
        Task RegisterNotification(NotifyDto obj);

    }
}
