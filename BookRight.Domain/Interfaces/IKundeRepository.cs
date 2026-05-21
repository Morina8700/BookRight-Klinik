using BookRight.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Interfaces
{
    public interface IKundeRepository
    {
        Task TilføjAsync(Kunde kunde);
        Task OpdaterAsync(Kunde kunde);
        Task<Kunde?> HentPåIdAsync(Guid kundeID);
        Task<IEnumerable<Kunde>> HentAlleAsync();
    }
}
