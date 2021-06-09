using NUnit.Framework;
using Breakout;
using Breakout.PowerUp;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using DIKUArcade.GUI;

namespace BreakoutTests
{
    public class PowerUpTests
    {
        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();
        }
        [Test]
        public void PowerUpShouldMove(){
            PowerUp up = new PowerUp(new DynamicShape (new Vec2F(0.5f, 0.5f), new Vec2F(0.1f, 0.1f)), Path.Combine("Assets", "Images", "ball.png"));
            up.Update();
            up.Update();
            up.ShouldShow();
            up.Update();
            Assert.Less(up.GetShape().Position.Y, 0.5f);
        }

        [Test]
        public void PowerUpShouldntMove() {
            PowerUp up = new PowerUp(new DynamicShape (new Vec2F(0.5f, 0.5f), new Vec2F(0.1f, 0.1f)), Path.Combine("Assets", "Images", "ball.png"));
            up.Update();
            up.Update();
            up.Update();
            Assert.AreEqual(up.GetShape().Position.Y, 0.5f);
        }

        [Test]
        public void TestExtraPointEffect() {
            ExtraPoint powerup = new ExtraPoint(new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.1f, 0.1f)));
            ScoreBoard score = new ScoreBoard(new Vec2F(0.75f, 0.6f), new Vec2F(0.4f, 0.4f));
            int points = score.Score;
            powerup.AddEffect();
            Assert.AreEqual(points + 50, score.Score);
        }
    }
}