namespace Pandaros.WoWParser.API.DomainModels.Stats
{
    public interface IStats
    {
        string CharacterId { get; set; }
        string FightId { get; set; }
        string InstanceId { get; set; }
    }
}