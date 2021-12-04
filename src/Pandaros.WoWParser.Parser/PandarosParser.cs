using Pandaros.WoWParser.Parser.Models;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Pandaros.WoWParser.Parser.Parsers;
using System.Collections.Generic;
using Pandaros.WoWParser.Parser.Calculators;
using Pandaros.WoWParser.Parser.FightMonitor;
using System.Configuration;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Pandaros.WoWParser.Parser.DataAccess;
using Pandaros.WoWParser.Parser.Repositories;

namespace Pandaros.WoWParser.Parser
{
    public static class PandarosParser
    {
        public static void PandarosParserSetup(this IServiceCollection services, IPandaLogger logger, IStatsLogger statsReporter, IMongoClient mongoClient)
        {
            services.AddSingleton<IPandaLogger>(logger);
            services.AddSingleton<IStatsLogger>(statsReporter);
            services.AddSingleton<IMongoClient>(mongoClient);
            services.AddSingleton<ICombatParser<SpellDamage>, SpellDamageParser>();
            services.AddSingleton<ICombatParser<SwingDamage>, SwingDamageParser>();
            services.AddSingleton<ICombatParser<SpellFailed>, SpellFailedParser>();
            services.AddSingleton<ICombatParser<SpellBase>, SpellParser>();
            services.AddSingleton<ICombatParser<SpellEnergize>, SpellEnergizeParser>();
            services.AddSingleton<ICombatParser<SpellAura>, SpellAuraParser>();
            services.AddSingleton<ICombatParser<SpellAuraDose>, SpellAuraDoseParser>();
            services.AddSingleton<ICombatParser<SpellAuraBrokenSpell>, SpellAuraBrokenSpellParser>();
            services.AddSingleton<ICombatParser<SpellMissed>, SpellMissedParser>();
            services.AddSingleton<ICombatParser<SwingMissed>, SwingMissedParser>();
            services.AddSingleton<ICombatParser<SpellHeal>, SpellHealParser>();
            services.AddSingleton<ICombatParser<CombatEventBase>, BaseParser>();
            services.AddSingleton<ICombatParser<EnviormentalDamage>, EnviormentalDamageParser>();
            services.AddSingleton<ICombatParser<SpellInterrupt>, SpellInterruptParser>();
            services.AddSingleton<ICombatParser<SpellDispel>, SpellDispelParser>();
            services.AddSingleton<ICombatParser<SpellDrain>, SpellDrainParser>();
            services.AddSingleton<ICombatParser<Enchant>, EnchantParser>();
            services.AddSingleton<ICombatParser<SpellExtraAttacks>, SpellExtraAttacksParser>();
            services.AddSingleton<IParserFactory, ParserFactory>();
            services.AddScoped<IFightMonitorFactory, FightMonitorFactory>();
            services.AddScoped<CombatLogParser>();
            services.AddSingleton<CombatLogCombiner>();
        }

        public static void PandarosParserSetup(this ContainerBuilder builder, IPandaLogger logger, IStatsLogger statsReporter, IMongoClient mongoClient)
        {
            builder.RegisterInstance(logger).As<IPandaLogger>().SingleInstance();
            builder.RegisterInstance(statsReporter).As<IStatsLogger>().SingleInstance();
            builder.RegisterInstance(mongoClient).As<IMongoClient>().SingleInstance();
            builder.RegisterType<SpellDamageParser>().As<ICombatParser<SpellDamage>>().SingleInstance();
            builder.RegisterType<SwingDamageParser>().As<ICombatParser<SwingDamage>>().SingleInstance();
            builder.RegisterType<SpellFailedParser>().As<ICombatParser<SpellFailed>>().SingleInstance();
            builder.RegisterType<SpellParser>().As<ICombatParser<SpellBase>>().SingleInstance();
            builder.RegisterType<SpellEnergizeParser>().As<ICombatParser<SpellEnergize>>().SingleInstance();
            builder.RegisterType<SpellAuraParser>().As<ICombatParser<SpellAura>>().SingleInstance();
            builder.RegisterType<SpellAuraDoseParser>().As<ICombatParser<SpellAuraDose>>().SingleInstance();
            builder.RegisterType<SpellAuraBrokenSpellParser>().As<ICombatParser<SpellAuraBrokenSpell>>().SingleInstance();
            builder.RegisterType<SpellMissedParser>().As<ICombatParser<SpellMissed>>().SingleInstance();
            builder.RegisterType<SwingMissedParser>().As<ICombatParser<SwingMissed>>().SingleInstance();
            builder.RegisterType<SwingMissedParser>().As<ICombatParser<SwingMissed>>().SingleInstance();
            builder.RegisterType<SpellHealParser>().As<ICombatParser<SpellHeal>>().SingleInstance();
            builder.RegisterType<BaseParser>().As<ICombatParser<CombatEventBase>>().SingleInstance();
            builder.RegisterType<EnviormentalDamageParser>().As<ICombatParser<EnviormentalDamage>>().SingleInstance();
            builder.RegisterType<SpellInterruptParser>().As<ICombatParser<SpellInterrupt>>().SingleInstance();
            builder.RegisterType<SpellDispelParser>().As<ICombatParser<SpellDispel>>().SingleInstance();
            builder.RegisterType<SpellDrainParser>().As<ICombatParser<SpellDrain>>().SingleInstance();
            builder.RegisterType<EnchantParser>().As<ICombatParser<Enchant>>().SingleInstance();
            builder.RegisterType<SpellExtraAttacksParser>().As<ICombatParser<SpellExtraAttacks>>().SingleInstance();
            builder.RegisterType<ParserFactory>().As<IParserFactory>().SingleInstance();
            builder.RegisterType<FightMonitorFactory>().As<IFightMonitorFactory>();
            builder.RegisterType<CombatLogParser>();
            builder.RegisterType<CombatLogCombiner>().SingleInstance();
        }

        internal static void SetupDataProviders(this ContainerBuilder builder, IPandaLogger logger, IMongoClient mongoClient)
        {
            builder.RegisterInstance(logger).As<IPandaLogger>().SingleInstance();
            builder.RegisterInstance(mongoClient).As<IMongoClient>().SingleInstance();
            builder.RegisterType<CharacterData>().SingleInstance();
            builder.RegisterType<FightsData>().SingleInstance();
            builder.RegisterType<GuildsData>().SingleInstance();
            builder.RegisterType<HostData>().SingleInstance();
            builder.RegisterType<InstanceData>().SingleInstance();
            builder.RegisterType<InstanceInfoData>().SingleInstance();
            builder.RegisterType<ServerData>().SingleInstance();
            builder.RegisterType<UserData>().SingleInstance();
            builder.RegisterType<UserRepo>().SingleInstance();
        }
    }
}
