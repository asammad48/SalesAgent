using System;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces
{
    public interface ISalesTaskRunner
    {
        Task ExecuteAsync(Guid taskId);
    }
}
