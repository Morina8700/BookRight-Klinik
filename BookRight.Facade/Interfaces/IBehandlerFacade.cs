using BookRight.Facade.Contracts.Behandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.Interfaces
{
    public interface IBehandlerFacade
    {
        Task <Guid> OpretBehandlerAsync(OpretBehandlerRequest request);
    }
}
