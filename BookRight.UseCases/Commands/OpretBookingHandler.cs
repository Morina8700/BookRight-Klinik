using BookRight.Domain.Aggregates;
using BookRight.Domain.Interfaces;

namespace BookRight.UseCases.Commands
{
    public class OpretBookingHandler
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IKlinikRepository _klinikRepository;
        private readonly IBehandlerRepository _behandlerRepository;
        private readonly IBehandlingstypeRepository _behandlingstypeRepository;

        public OpretBookingHandler(
            IBookingRepository bookingRepository,
            IKlinikRepository klinikRepository,
            IBehandlerRepository behandlerRepository,
            IBehandlingstypeRepository behandlingstypeRepository)
        {
            _bookingRepository = bookingRepository;
            _klinikRepository = klinikRepository;
            _behandlerRepository = behandlerRepository;
            _behandlingstypeRepository = behandlingstypeRepository;
        }

        public async Task<bool> HandleAsync(OpretBookingCommand command)
        {
            var behandler = await _behandlerRepository
                .HentMedDetaljerAsync(command.BehandlerId);

            var behandlingstype = await _behandlingstypeRepository
                .HentAsync(command.BehandlingstypeId);

            if (!behandler.ArbejderPå(command.KlinikId))
                return false;

            if (!behandler.KanUdføre(behandlingstype))
                return false;

            var behandlerLedig = await _bookingRepository
                .ErBehandlerLedigAsync(command.BehandlerId, command.StartTid, command.SlutTid);

            if (!behandlerLedig)
                return false;

            var klinik = await _klinikRepository.HentAsync(command.KlinikId);
            var aktiveBookinger = await _bookingRepository
                .HentAntalAktiveBookingerAsync(command.KlinikId, command.StartTid, command.SlutTid);

            if (aktiveBookinger >= klinik.AntalRum)
                return false;

            var booking = new Booking(
                command.KundeId,
                command.BehandlerId,
                command.KlinikId,
                command.BehandlingstypeId,
                command.StartTid,
                command.SlutTid,
                command.PrisUdenRabat,
                command.PrisMedRabat,
                command.AnvendtRabatType,
                command.KampagneId
            );

            await _bookingRepository.AddAsync(booking);
            return true;
        }
    }
}