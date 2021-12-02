using Application.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SeverityService : ISeverityService
    {
        private readonly ISeverityRepository _severityRepository;

        public SeverityService(ISeverityRepository severityRepository)
        {
            _severityRepository = severityRepository;
        }

        public async Task<IEnumerable<Severity>> GetAllSeverities(CancellationToken cancellationToken = default)
        {
            return await _severityRepository.GetAllSeverities(cancellationToken);
        }
    }
}
