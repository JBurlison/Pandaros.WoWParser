﻿namespace Pandaros.WoWLogParser.Parser.Models
{
    public interface IEnchant
    {
        int ItemID { get; set; }
        string ItemName { get; set; }
        string SpellName { get; set; }
    }
}