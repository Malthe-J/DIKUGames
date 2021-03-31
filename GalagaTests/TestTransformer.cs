using System;
using NUnit.Framework;
using Galaga.GalagaStates;

namespace GalagaTests{
    public class Test{
        [TestCase("GameRunning")]
        public void testRunningString(string a){
            Assert.AreEqual(StateTransformer.TransFormStringToState(a), GameStateType.GameRunning);
        }
        [TestCase("GamePaused")]
        public void testPausedString(string a){
            Assert.AreEqual(StateTransformer.TransFormStringToState(a), GameStateType.GamePaused);
        }

        [TestCase("MainMenu")]
        public void testMenuString(string a){
            Assert.AreEqual(StateTransformer.TransFormStringToState(a), GameStateType.MainMenu);
        }

        [TestCase(GameStateType.GameRunning)]
        public void testRunningState(GameStateType a){
            Assert.AreEqual(StateTransformer.TransformStateToString(a), "GameRunning");
        }

        [TestCase(GameStateType.GamePaused)]
        public void testPausedState(GameStateType a){
            Assert.AreEqual(StateTransformer.TransformStateToString(a), "GamePaused");
        }

        [TestCase(GameStateType.MainMenu)]
        public void testMenuState(GameStateType a){
            Assert.AreEqual(StateTransformer.TransformStateToString(a), "MainMenu");
        }

    }
    
}