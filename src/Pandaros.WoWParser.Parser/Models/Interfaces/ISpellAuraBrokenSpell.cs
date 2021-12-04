namespace Pandaros.WoWParser.Parser.Models
{
    public interface ISpellAuraBrokenSpell : ISpellAura
    {
        int ExtraSpellID { get; set; }
        string ExtraSpellName { get; set; }
        SpellSchool ExtraSpellSchool { get; set; }
    }
}