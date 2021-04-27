using NUnit.Framework;

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
            assert.AreEqual("LEVEL 1", Level.GetName());
            assert.AreEqual(300, Level.GetTime());
            assert.AreEqual("#", Level.GetHardened());
            assert.AreEqual(2, Level.GetPowerUp());
        }
        public void TestLevel2()
        {
            assert.AreEqual("LEVEL 2", Level.GetName());
            assert.AreEqual(180, Level.GetTime());
            assert.AreEqual(2, Level.GetPowerUp());
        }
        public void TestLevel3()
        {
            assert.AreEqual("LEVEL 3", Level.GetName());
            assert.AreEqual(180, Level.GetTime());
            assert.AreEqual("%", Level.GetPowerUp());
            assert.AreEqual("X", Level.GetUnbreakable());
        }
         public void Wall()
        {
            assert.AreEqual("Central Mass", Level.GetName());
        }
        public void Columns()
        {
            assert.AreEqual("Columns", Level.GetName());
        }
        public void Cemtral_Mass()
        {
            assert.AreEqual("Central Mass", Level.GetName());
        }
    }
}