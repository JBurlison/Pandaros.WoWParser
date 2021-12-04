namespace Pandaros.WoWParser.Parser.Models
{
    public interface ISpell
    {
        SpellSchool School { get; set; }
        int SpellId { get; set; }
        string SpellName { get; set; }
    }
}