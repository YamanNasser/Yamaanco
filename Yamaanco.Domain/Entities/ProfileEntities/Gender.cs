namespace Yamaanco.Domain.Entities.ProfileEntities
{
    public class Gender
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static readonly int Male = 1;
        public static readonly int Female = 2;
    }
}