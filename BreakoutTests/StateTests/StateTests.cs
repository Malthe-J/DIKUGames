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
        private Player Casper;
        //private Game gaysti;

        [SetUp]
        public void init()
        {
            Window.CreateOpenGLContext();
            Casper = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));
            //gaysti = new Game();

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
        // [Test]
        // public void TestShouldGameEnd()
        // {
        //     for (int i; i<3; i ++) {
        //          Casper.HealthDown();
        //     }
        //  Assert.AreEqual(Gaysti.State.ActiveState.MainMenu, GameStateType.MainMenu);
        // }
    }
}