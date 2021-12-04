namespace Pandaros.WoWParser.Parser.Models
{
    public interface ISpellDispel
    {
        string AuraType { get; set; }
        SpellSchool ExtraSchool { get; set; }
        int ExtraSpellId { get; set; }
        string ExtraSpellName { get; set; }
    }
}