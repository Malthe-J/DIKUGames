using NUnit.Framework;
using Breakout;

namespace BreakoutTests
{
    public class LevelLoadingTests
    {
        [SetUp]
        public void Setup() {
        
        }

        [Test]
        public void TestLevel1()
        {
            Level leveltest = new Level("..\\..\\..\\Assets\\Levels\\level1.txt");
            
            Assert.AreEqual("LEVEL 1", leveltest.MetaData.Name);
            Assert.AreEqual(300, leveltest.MetaData.Time);
            Assert.AreEqual('#', leveltest.MetaData.Hardened);
            Assert.AreEqual('2', leveltest.MetaData.PowerUp);
        }
        [Test]
        public void TestLevel2()
        {
            Level leveltest = new Level("..\\..\\..\\Assets\\Levels\\level2.txt");

            Assert.AreEqual("LEVEL 2", leveltest.MetaData.Name);
            Assert.AreEqual(180, leveltest.MetaData.Time);
            Assert.AreEqual('d', leveltest.MetaData.PowerUp);
        }
        [Test]
        public void TestLevel3()
        {
            Level leveltest = new Level("..\\..\\..\\Assets\\Levels\\level3.txt");
            Assert.AreEqual("LEVEL 3", leveltest.MetaData.Name);
            Assert.AreEqual(180, leveltest.MetaData.Time);
            Assert.AreEqual('%', leveltest.MetaData.PowerUp);
            Assert.AreEqual('X', leveltest.MetaData.Unbreakable);
        }
        [Test]
         public void Wall()
        {
            Level leveltest = new Level("..\\..\\..\\Assets\\Levels\\wall.txt");
            Assert.AreEqual("Central Mass", leveltest.MetaData.Name);
        }
        [Test]
        public void Columns()
        {
            Level leveltest = new Level("..\\..\\..\\Assets\\Levels\\columns.txt");
            Assert.AreEqual("Columns", leveltest.MetaData.Name);
        }
        [Test]
        public void Central_Mass()
        {
            Level leveltest = new Level("..\\..\\..\\Assets\\Levels\\central-mass.txt");
            Assert.AreEqual("Central Mass", leveltest.MetaData.Name);
        }
    }
}