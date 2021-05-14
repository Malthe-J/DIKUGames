using NUnit.Framework;
using Breakout;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;

namespace BreakoutTests.EntityTests
{
    [TestFixture]
    public class BlockTests
    {
        Block BlockTest; 
        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();
            BlockTest = new Block(new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(0.05f, 0.05f)), new Image( 
                "Assets\\Images\\BulletRed2.png"));
        }

        [Test]
        public void DecreaseHealthTest()
        {
            BlockTest.HealthDown();
            Assert.AreEqual(0, BlockTest.Health);
        }
        [Test]
        public void DestroyBlock()
        {   
            EntityContainer<Block> temp = new EntityContainer<Block>();
            temp.AddEntity(BlockTest);
            temp.Iterate(block => { 
                block.HealthDown();
            });
            Assert.AreEqual(0, temp.CountEntities());
        }
    }
}