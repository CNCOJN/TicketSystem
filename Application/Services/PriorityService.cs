using Application.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PriorityService : IPriorityService
    {
        private readonly IPriorityRepository _priorityRepository;

        public PriorityService(IPriorityRepository priorityRepository)
        {
            _priorityRepository = priorityRepository;
        }

        public async Task<IEnumerable<Priority>> GetAllPriorities(CancellationToken cancellationToken = default)
        {
            return await _priorityRepository.GetAllPriorities(cancellationToken);
        }
    }
}
