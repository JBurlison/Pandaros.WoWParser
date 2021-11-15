namespace Pandaros.WoWLogParser.Parser.Models
{
    public interface ISpellEnergize
    {
        int EneryAmount { get; set; }
        PowerType PowerType { get; set; }
    }
}