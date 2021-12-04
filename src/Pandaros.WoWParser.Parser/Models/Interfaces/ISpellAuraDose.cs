namespace Pandaros.WoWParser.Parser.Models
{
    public interface ISpellAuraDose : ISpellAura
    {
        int AuraDoeseAdded { get; set; }
    }
}