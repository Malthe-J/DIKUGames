using DIKUArcade.Input;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using DIKUArcade.State;
namespace Breakout {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;
        private Player player;
        private List<Level> level;

        public GameRunning(){
            InitializeGameState();
        }
        public static GameRunning GetInstance(BreakoutStates.GameStateType prevtype) {
            if(prevtype == BreakoutStates.GameStateType.MainMenu) {
                return GameRunning.instance = new GameRunning();
            } 
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }

        public void GameLoop() {
           

        }
        public void UpdateState(){
            GameLoop();
        }
        public void RenderState() {
            player.Move();

            player.Render();

        }
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            player.HandleKeyEvent(action, key);
            if (action == KeyboardAction.KeyPress) {
                switch (key){
                    case KeyboardKey.Escape:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent{ EventType = GameEventType.GameStateEvent, 
                                                                Message = "GamePaused", StringArg1 = "CHANGE_STATE"});
                        break;
                }
            }
        }
        public void InitializeGameState() {
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));
            level = new List<Level>();
            string[] levels = Directory.GetFiles(Path.Combine("Assets", "Levels"));
            foreach(var i in levels) {
                level.Add(new Level (i));
            }
        }




        public void ResetState()
        {

        }
    }
}