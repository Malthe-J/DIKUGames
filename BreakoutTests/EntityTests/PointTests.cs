using NUnit.Framework;
using Breakout;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using DIKUArcade.GUI;

namespace BreakoutTests {
    public class PointTests{

        private ScoreBoard score;

        [SetUp]
        public void Init() {
            score = new ScoreBoard(new Vec2F(0.75f, 0.6f), new Vec2F(0.4f, 0.4f));
        }

        [Test]
        public void AddPoint()
        {
            score.AddPoint();
            Assert.AreEqual(score.Score, 1);
        }
    }
}