using System.Collections.Generic;
using MatchAgregationServiceTests;

namespace MatchAgregationService.Services
{
    internal interface IResultParser
    {
        IEnumerable<TeamResult> Parse(string resultString);
    }
}