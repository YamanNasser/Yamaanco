using System;
using Yamaanco.Application.Interfaces;

namespace Yamaanco.Infrastructure.Shared.ManageDateTime
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;

        public int CurrentYear => DateTime.Now.Year;
    }
}