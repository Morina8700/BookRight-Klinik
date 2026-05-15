using BookRight.UseCases.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.UseCases.Queries
{
    public interface IBookingQueryRepository
    {
        Task<List<BookingKalenderDto>> HentBookingerForDatoAsync(DateOnly dato);
    }
}
