
namespace Pandaros.WoWLogParser.Parser.Models
{
    public class SpellAura : SpellBase, ISpellAura
    {
        public BuffType AuraType { get; set; }
    }
}
