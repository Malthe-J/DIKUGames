using DIKUArcade.Input;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using DIKUArcade.State;
using DIKUArcade.GUI;

namespace Breakout {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;
        private Player player;
        private Level level;
        private List<Level> levels;

        public GameRunning(){
            InitializeGameState();
        }
        public static GameRunning GetInstance() {
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }

        public void GameLoop() {
           

        }
        public void UpdateState(){
            GameLoop();
        }
        public void RenderState() {
            player.Move();
            level.Render();
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
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.15f/2.0f, 0.027f/2.0f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));
            //level = new Level(Path.Combine("Assets", "Levels" , "Level1.txt"));
            levels = new List<Level>();
            string[] File = Directory.GetFiles(Path.Combine("Assets", "Levels"));
            foreach(var i in File) {
                levels.Add(new Level (i));
            }
        }




        public void ResetState()
        {

        }
    }
}