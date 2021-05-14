using NUnit.Framework;
using Breakout;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using DIKUArcade.GUI;

namespace BreakoutTests {
    public class BallTests{

        private Ball ballTest;

        [SetUp]
        public void init(){
            Window.CreateOpenGLContext();
            ballTest = new Ball(
                new DynamicShape(new Vec2F(0.15f, 0.25f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "ball.png")));
        }

        [Test]
        public void LeaveWindowLeftSide()
        {
            ballTest.GetShape().Position.X = 0.0f;
            ballTest.GetShape().Direction.X = -0.01f;
            ballTest.Move();
            ballTest.Move();
            Assert.AreEqual(ballTest.GetShape().Position.X, 0.0f);
        }

        [Test]
        public void LeaveWindowRightSide()
        {
            ballTest.GetShape().Position.X = 1.0f;
            ballTest.GetShape().Direction.X = 0.01f;
            ballTest.Move();
            Assert.Less(ballTest.GetShape().Position.X, 1.0f);
            Assert.Greater(ballTest.GetShape().Position.X, 0.0f);
        }

        [Test]
        public void LeaveWindowTop()
        {
            ballTest.GetShape().Position.Y = 1.0f;
            ballTest.GetShape().Direction.Y = 0.01f;
            ballTest.Move();
            Assert.Less(ballTest.GetShape().Position.Y, 1.0f);
            Assert.Greater(ballTest.GetShape().Position.Y, 0.0f);
        }

        public void LeaveWindowButtom()
        {
            EntityContainer<Ball> temp = new EntityContainer<Ball>();
            temp.AddEntity(ballTest);
            temp.Iterate(ball => { 
                ball.GetShape().Position.Y = 0.0f;
                ball.GetShape().Direction.Y = -0.01f;
                ball.Move();
            });
            Assert.AreEqual(0, temp.CountEntities());
        }
    }
}