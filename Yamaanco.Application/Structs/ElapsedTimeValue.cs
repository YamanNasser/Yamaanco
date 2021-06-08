using Yamaanco.Application.Enums;

namespace Yamaanco.Application.Structs
{
    public struct ElapsedTimeValue
    {
        public ElapsedTimeType Type { get; set; }
        public int Value { get; set; }
        public string Message { get; set; }

        public ElapsedTimeValue(ElapsedTimeType type, int value, string message)
        {
            Type = type;
            Value = value;
            Message = message;
        }
    }
}