using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.UseCases.Commands
{
    public class OpretBookingResult
    {
        public bool Success { get; set; }
        public decimal PrisUdenRabat { get; set; }
        public decimal PrisMedRabat { get; set; }
        public string? AnvendtRabatType { get; set; }
    }
}
