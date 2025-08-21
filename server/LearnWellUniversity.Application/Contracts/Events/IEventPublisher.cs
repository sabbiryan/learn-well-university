using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Contracts.Events
{
    public interface IEventPublisher
    {
        Task PublishAsync<T>(string topicOrQueue, T payload, CancellationToken ct);
    }
}
