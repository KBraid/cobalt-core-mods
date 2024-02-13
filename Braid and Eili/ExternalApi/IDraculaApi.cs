using Nickel;
using System.Collections.Generic;
using System;

namespace KBraid.BraidEili;
public interface IDraculaApi
{
    IDeckEntry DraculaDeck { get; }

    void RegisterBloodTapOptionProvider(Status status, Func<State, Combat, Status, List<CardAction>> provider);
}