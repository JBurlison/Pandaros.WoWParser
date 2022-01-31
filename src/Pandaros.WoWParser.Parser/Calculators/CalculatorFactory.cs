using Autofac;
using MongoDB.Driver;
using Pandaros.WoWParser.Parser.FightMonitor;
using Pandaros.WoWParser.Parser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Pandaros.WoWParser.Parser.Calculators
{
    public class CalculatorFactory : ICalculatorFactory
    {
        public Dictionary<string, List<ICalculator>> Calculators { get; set; } = new Dictionary<string, List<ICalculator>>();
        public List<ICalculator> CalculatorFlatList { get; set; } = new List<ICalculator>();

        public ICombatState State { get; set; }
        public MonitoredFight Fight { get; set; }
        Dictionary<string, int> _eventCount = new Dictionary<string, int>();
        IPandaLogger _logger;
        IStatsLogger _reporter;

        public CalculatorFactory(IPandaLogger logger, IStatsLogger reporter, ICombatState state, MonitoredFight fight)
        {
            var assem = Assembly.GetExecutingAssembly();
            _logger = logger;
            _reporter = reporter;
            State = state;
            Fight = fight;
            var typeArray = assem.GetTypes();
            var builder = new ContainerBuilder();
            //builder.SetupDataProviders(logger, mongoClient);
            builder.RegisterInstance(logger).As<IPandaLogger>().SingleInstance();
            builder.RegisterInstance(reporter).As<IStatsLogger>().SingleInstance();
            builder.RegisterInstance(state).As<ICombatState>().SingleInstance();
            builder.RegisterInstance(fight).As<MonitoredFight>().SingleInstance();

            List<Type> foundCalculators = new List<Type>();

            foreach (var type in typeArray)
            {
                if (type.GetInterfaces().Any(i => i == typeof(ICalculator)) && !type.IsAbstract)
                {
                    builder.RegisterType(type);
                    foundCalculators.Add(type);
                }
            }
            var container = builder.Build();

            foreach (var type in foundCalculators)
            {
                CalculatorFlatList.Add(container.Resolve(type) as ICalculator);
            }

            foreach (var calc in CalculatorFlatList)
                foreach(var evnt in calc.ApplicableEvents)
                {
                    if (Calculators.TryGetValue(evnt, out var list))
                        list.Add(calc);
                    else
                        Calculators.Add(evnt, new List<ICalculator>() { calc });
                }
        }

        public void CalculateEvent(ICombatEvent combatEvent)
        {
            if (State.InFight)
            {
                if (!_eventCount.TryGetValue(combatEvent.EventName, out int val))
                    _eventCount[combatEvent.EventName] = 1;
                else
                    _eventCount[combatEvent.EventName] = val + 1;
            }

            if (Calculators.TryGetValue(combatEvent.EventName, out var calcList))
                foreach (var calc in calcList)
                    calc.CalculateEvent(combatEvent);
           }


        public void StartFight(ICombatEvent combatEvent)
        {
            _eventCount.Clear();
            _logger.Log("---------------------------------------------");
            _reporter.Output($"```\nFight Start: {Fight.BossName}\n```");
            _logger.Log("---------------------------------------------");
            foreach (var calc in CalculatorFlatList)
                calc.StartFight(combatEvent);
        }

        public void FinalizeFight(ICombatEvent combatEvent)
        {
            foreach (var calc in CalculatorFlatList)
                calc.FinalizeFight(combatEvent);

            _logger.Log("---------------------------------------------");
            _reporter.Output($"```\nFight End: {Fight.BossName} ({Fight.FightEnd.Subtract(Fight.FightStart)})\n```");
            foreach (var ev in _eventCount)
                _logger.Log($"{ev.Key}: {ev.Value}");
            _logger.Log("---------------------------------------------");
        }
        private bool _disposed = false;

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                Calculators = null;
                CalculatorFlatList = null;
                _eventCount = null;
                Fight.Dispose();
                State.Dispose();
            }
        }
    }
}
