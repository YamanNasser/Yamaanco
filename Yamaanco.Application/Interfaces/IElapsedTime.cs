using System;
using Yamaanco.Application.Structs;

namespace Yamaanco.Application.Interfaces
{
    public interface IElapsedTime
    {
        ElapsedTimeValue Calculate(DateTime date);
    }
}