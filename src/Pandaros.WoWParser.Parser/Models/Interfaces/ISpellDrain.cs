namespace Pandaros.WoWParser.Parser.Models
{
    public interface ISpellDrain
    {
        int DrainAmount { get; set; }
        int ExtraDrainAmount { get; set; }
        PowerType PowerType { get; set; }
    }
}