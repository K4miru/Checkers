using System;
using System.Collections.Generic;

namespace Checkers
{
    public interface IMove : ICloneable
    {
        Direction Direction { get; }

        Position From { get; }

        Position To { get; }

        Position Capture { get; }
    }
}