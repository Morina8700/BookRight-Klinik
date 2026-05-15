using BookRight.UseCases.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.UseCases.Queries
{
    public class HentBookingHandler
    {
        private readonly IBookingQueryRepository _bookingQueryRepository;

        public HentBookingHandler(IBookingQueryRepository bookingQueryRepository)
        {
            _bookingQueryRepository = bookingQueryRepository;
        }

        public async Task<List<BookingKalenderDto>> HandleAsync(HentBookingerQuery query)
        {
            return await _bookingQueryRepository.HentBookingerForDatoAsync(query.Dato);
        }
    }
}
