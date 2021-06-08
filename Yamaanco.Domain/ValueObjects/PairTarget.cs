using System;
using System.Collections.Generic;
using Yamaanco.Domain.Common;

namespace Yamaanco.Domain.ValueObjects
{
    public class PairTarget : ValueObject
    {
        private string FirstId { get; set; }
        private string SecondId { get; set; }

        public PairTarget(string firstId, string secondId)
        {
            FirstId = firstId;
            SecondId = secondId;
        }

        public override string ToString()
        {
            var arr = new string[] { FirstId, SecondId };
            Array.Sort(arr, StringComparer.InvariantCulture);
            return $"{arr[0]}__{arr[1]}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FirstId;
            yield return SecondId;
        }
    }
}