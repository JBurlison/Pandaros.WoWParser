namespace Pandaros.WoWLogParser.Parser.Models
{
    public interface ISpellAuraDose : ISpellAura
    {
        int AuraDoeseAdded { get; set; }
    }
}