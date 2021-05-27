using System;
using NUnit.Framework;
using Breakout;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using DIKUArcade.GUI;

namespace BreakoutTests
{
    public class PlayerTests
    {
        private Player Casper;

        [SetUp]
        public void init(){
            Window.CreateOpenGLContext();
            Casper = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));
        }

        [Test]
        public void testMoveOutOfWindowLeftSide(){
            Casper.GetShape().Position.X = -1.0f;
            Casper.Move();
            Assert.AreEqual(Casper.GetShape().Position.X, 0.0f);
        }

        [Test]
        public void testMoveOutOfWindowRightSide(){
            Casper.GetShape().Position.X = 1.2f;
            Casper.Move();
            Assert.AreEqual(Casper.GetShape().Position.X, 1.0f - Casper.GetShape().Extent.X);
        }

        [Test]
        public void TestMoveLeft()
        {
            float PrevXPosition = Casper.GetShape().Position.X;
            Casper.GetShape().Position.X -= 0.2f;
            Casper.Move();
            Assert.Less(Casper.GetShape().Position.X, PrevXPosition);
        }

        [Test]
        public void TestMoveRight(){
            float PrevXPosition = Casper.GetShape().Position.X;
            Casper.GetShape().Position.X += 0.2f;
            Casper.Move();
            Assert.Less(PrevXPosition, Casper.GetShape().Position.X);
        }
        [Test]
        public void TestLoseHealth(){
           Casper.HealthDown();
            Assert.AreEqual(Casper.Health, 2);
        }
    }
}