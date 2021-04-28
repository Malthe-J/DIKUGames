using NUnit.Framework;
using Breakout;

namespace BreakoutTests
{
    public class Tests
    {
        [SetUp]
        public void Setup() {
        
        }

        [Test]
        public void TestLevel1()
        {
            Level leveltest = new Level("..\\..\\..\\..\\Breakout\\Assets\\Levels\\level1.txt");
            
            Assert.AreEqual("LEVEL 1\r", leveltest.Name);
            Assert.AreEqual(300, leveltest.Time);
            Assert.AreEqual('#', leveltest.Hardened);
            Assert.AreEqual('2', leveltest.PowerUp);
        }
        [Test]
        public void TestLevel2()
        {
            Level leveltest = new Level("..\\..\\..\\..\\Breakout\\Assets\\Levels\\level2.txt");

            Assert.AreEqual("LEVEL 2", leveltest.Name);
            Assert.AreEqual(180, leveltest.Time);
            Assert.AreEqual('d', leveltest.PowerUp);
        }
        [Test]
        public void TestLevel3()
        {
            Level leveltest = new Level("..\\..\\..\\..\\Breakout\\Assets\\Levels\\level3.txt");
            Assert.AreEqual("LEVEL 3", leveltest.Name);
            Assert.AreEqual(180, leveltest.Time);
            Assert.AreEqual('%', leveltest.PowerUp);
            Assert.AreEqual('X', leveltest.Unbreakable);
        }
        [Test]
         public void Wall()
        {
            Level leveltest = new Level("..\\..\\..\\..\\Breakout\\Assets\\Levels\\wall.txt");
            Assert.AreEqual("Central Mass", leveltest.Name);
        }
        [Test]
        public void Columns()
        {
            Level leveltest = new Level("..\\..\\..\\..\\Breakout\\Assets\\Levels\\columns.txt");
            Assert.AreEqual("Columns", leveltest.Name);
        }
        [Test]
        public void Central_Mass()
        {
            Level leveltest = new Level("..\\..\\..\\..\\Breakout\\Assets\\Levels\\central-mass.txt");
            Assert.AreEqual("Central Mass", leveltest.Name);
        }
    }
}