using NUnit.Framework;
using Breakout;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;

namespace BreakoutTests.EntityTests
{
    [TestFixture]
    public class SpecialBlockTests
    {
        private HardenedBlock HardenedBlock;
        private UnbreakableBlock UnbreakableBlock;

        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();
            HardenedBlock = new HardenedBlock(new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(0.05f, 0.05f)),
                "Assets\\Images\\BulletRed2.png");
            UnbreakableBlock = new UnbreakableBlock(new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(0.05f, 0.05f)),
                "Assets\\Images\\BulletRed2.png");
        }

        [Test]
        public void DecreaseHealthTest()
        {
            HardenedBlock.HealthDown();
            UnbreakableBlock.HealthDown();
            Assert.AreEqual(HardenedBlock.Health, 1);
            Assert.AreEqual(UnbreakableBlock.Health, 1);
        }
        
        [Test]
        public void CheckDestroyBlock()
        {  
            EntityContainer<Block> temp = new EntityContainer<Block>();
            temp.AddEntity(HardenedBlock);
            temp.AddEntity(UnbreakableBlock);
            temp.Iterate(block => { 
                block.HealthDown();
            });

            temp.Iterate(block => { 
                block.HealthDown();
            });

            Assert.AreEqual(1, temp.CountEntities());
        
        }
    }
}