using System;
using Core.Enums;
using Core.Logging.Logger;
using Xunit;

namespace Core.Test.LoggingRelated
{
    public class TestLog
    {
        [Fact]
        public void BasicTest()
        {
            using (var dumper = new DumpLogTarget {Connected = true})
            {
                var log = new ClassLogger<TestLog>();
                log.Trace("Hello Trace");
                log.Debug("Hello Debug");
                log.Info("Hello Info");
                log.Warning("Hello Warning");
                log.Error("Hello Error", new InvalidOperationException("Bad things sometimes happen"));
                Assert.Equal(5, dumper.EventLog.Count);
                Assert.Equal("Hello Trace", dumper.EventLog[0].Message);
            }
        }

        [Fact]
        public void MaskTest()
        {
            using (var dumper = new DumpLogTarget {Connected = true, LogMask = LogLevel.ProductionMask})
            {
                var log = new ClassLogger<TestLog>();
                log.Trace("Hello Trace");
                log.Debug("Hello Debug");
                log.Info("Hello Info");
                log.Warning("Hello Warning");
                log.Error("Hello Error", new InvalidOperationException("Bad things sometimes happen"));
                Assert.Equal(3, dumper.EventLog.Count);
                Assert.Equal(LogLevel.Info, dumper.EventLog[0].Level);
            }
        }
    }
}
