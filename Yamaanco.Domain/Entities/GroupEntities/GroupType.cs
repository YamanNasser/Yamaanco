namespace Yamaanco.Domain.Entities.GroupEntities
{
    public class GroupType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static readonly int Public = 1;
        public static readonly int Private = 2;
        public static readonly int Hidden = 3;
    }
}