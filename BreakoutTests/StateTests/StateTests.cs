using NUnit.Framework;
using Breakout;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using DIKUArcade.GUI;
using Breakout.BreakoutStates;

namespace BreakoutTests {
    public class StateTests{

        private StateMachine state;

        [SetUp]
        public void init()
        {
        }

        [Test]
        public void TestTransformStateToString()
        {
            string MainMenu = StateTransformer.TransformStateToString(GameStateType.MainMenu);
            string GameRunning = StateTransformer.TransformStateToString(GameStateType.GameRunning);
            string GamePaused = StateTransformer.TransformStateToString(GameStateType.GamePaused);
            Assert.AreEqual(MainMenu, "MainMenu");
            Assert.AreEqual(GameRunning, "GameRunning");
            Assert.AreEqual(GamePaused, "GamePaused");
        }

        [Test]
        public void TestTransformStringToState()
        {
            GameStateType mainMenu = StateTransformer.TransformStringToState("MainMenu");
            GameStateType gameRunning = StateTransformer.TransformStringToState("GameRunning");
            GameStateType gamePaused = StateTransformer.TransformStringToState("GamePaused");
            Assert.AreEqual(mainMenu, GameStateType.MainMenu);
            Assert.AreEqual(gameRunning, GameStateType.GameRunning);
            Assert.AreEqual(gamePaused, GameStateType.GamePaused);
        }
    }
}