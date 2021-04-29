using NUnit.Framework;
using Breakout;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;

namespace BreakoutTests.EntityTests
{
    [TestFixture]
    public class Tests
    {
        Block BlockTest; 
        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();
            BlockTest = new Block(new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(0.05f, 0.05f)), new Image( 
                "Assets\\Images\\BulletRed2.png"), 2);
        }

        [Test]
        public void DecreaseHealthTest()
        {
            BlockTest.HealthDown();
            Assert.AreEqual(1, BlockTest.Health);
        }
        [Test]
        public void DestroyBlock()
        {   
            EntityContainer<Block> temp = new EntityContainer<Block>();
            temp.AddEntity(BlockTest);
            BlockTest.HealthDown();
            BlockTest.HealthDown();
            temp.Iterate(block => { 
                if (BlockTest.IsBlockDead()){
                BlockTest.DeleteEntity();
            }});
            
            Assert.AreEqual(0, temp.CountEntities());
        }
    }
}