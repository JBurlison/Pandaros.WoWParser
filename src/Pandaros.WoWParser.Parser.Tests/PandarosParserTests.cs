using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pandaros.WoWParser.Parser;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Pandaros.WoWParser.Tests
{
    [TestClass()]
    public class PandarosParserTests
    {
        [TestMethod()]
        public void PandarosParserSetupTest()
        {
            var builder = new ContainerBuilder();
            var logger = new PandaLogger("C:/temp/");
            builder.PandarosParserSetup(logger, logger);

            var Container = builder.Build();

            var clp = Container.Resolve<CombatLogParser>();

            logger.Log("Starting Parse.");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            clp.ParseToEnd("C:/Program Files/Ascension Launcher/resources/client/Logs/WoWCombatLog.txt");
            sw.Stop();
            logger.Log($"Parsed in {sw.Elapsed}.");
            Thread.Sleep(1000);
        }
    }
}