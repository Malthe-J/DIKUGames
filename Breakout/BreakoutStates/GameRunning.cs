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
        private Ball ball;
        private ScoreBoard score;

        private int activeLevel = 0;
        private int health = 3;
        private Text display;
        private EntityContainer<Ball> ballContainer;

        public GameRunning(){
            ResetState();
        }
        public static GameRunning GetInstance(BreakoutStates.GameStateType state) {
            if (state == BreakoutStates.GameStateType.MainMenu)
            {
                return new GameRunning();
            }
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }

        public void GameLoop() {
            player.Move();
            PlayerHealthDown();
            ballContainer.Iterate(ball => {ball.Move();});
            ballContainer.Iterate(ball => {ball.CollideWithPlayer(player);});
            CollideWithBlock(levels[activeLevel].GetBlocks());
            ShouldGameEnd();
        }
        public void UpdateState(){
            GameLoop();
        }
        public void RenderState() {
            levels[activeLevel].Render();
            player.Render();
            ballContainer.RenderEntities();
            score.RenderScore();
            display.RenderText();
        }

        public void PlayerHealthDown(){
            if (ballContainer.CountEntities() == 0){
                health--;
                display.SetText("HP: " + health.ToString());
                ballContainer.AddEntity(new Ball(
                new DynamicShape(new Vec2F(player.GetPosition().X + player.GetExtent().X/2, player.GetPosition().Y + player.GetExtent().Y), 
                                new Vec2F(0.03f, 0.03f)),
                new Image(Path.Combine("Assets", "Images", "ball.png"))));

            ballContainer.Iterate(ball => {ball.Start();});
            }
        }
        public void ShouldGameEnd(){
            if(health == 0){
                BreakoutBus.GetBus().RegisterEvent(new GameEvent{ EventType = GameEventType.GameStateEvent, 
                                                                Message = "MainMenu", StringArg1 = "CHANGE_STATE"});
            }
        }



        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            player.HandleKeyEvent(action, key);
            if (action == KeyboardAction.KeyPress) {
                switch (key){
                    case KeyboardKey.Escape:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent{ EventType = GameEventType.GameStateEvent, 
                                                                Message = "GamePaused", StringArg1 = "CHANGE_STATE"});
                        break;
                    case KeyboardKey.F:
                        if (activeLevel < levels.Count - 1)
                        {
                            activeLevel++;
                            ResetState(); 
                        }
                        else
                        {
                            activeLevel = 0;
                            ResetState();
                        }
                        break;
                }
            }
        }

        public void ResetState()
        {
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.10f, 0.015f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));
            ball = new Ball(
                new DynamicShape(new Vec2F(player.GetPosition().X + player.GetExtent().X/2, player.GetPosition().Y + player.GetExtent().Y), 
                                new Vec2F(0.03f, 0.03f)),
                new Image(Path.Combine("Assets", "Images", "ball.png")));

            level = new Level(Path.Combine("Assets", "Levels" , "Level1.txt"));
            levels = new List<Level>();
            string[] File = Directory.GetFiles(Path.Combine("Assets", "Levels"));
            foreach(var i in File) {
                levels.Add(new Level (i));
            }
            score = new ScoreBoard(new Vec2F(0.75f, 0.6f), new Vec2F(0.4f, 0.4f));

            //Display health
            display = new Text("HP: " + health.ToString(), new Vec2F(0.75f, 0.5f), new Vec2F(0.4f, 0.4f));
            display.SetColor(System.Drawing.Color.HotPink);
            display.SetFontSize(32);

            //Ball container
            ballContainer = new EntityContainer<Ball>();
            ballContainer.AddEntity(new Ball(
                new DynamicShape(new Vec2F(player.GetPosition().X + player.GetExtent().X/2, player.GetPosition().Y + player.GetExtent().Y), 
                                new Vec2F(0.03f, 0.03f)),
                new Image(Path.Combine("Assets", "Images", "ball.png"))));

            ballContainer.Iterate(ball => {ball.Start();});
        }



        public void CollideWithBlock(EntityContainer<Block> blocks)
        {
            blocks.Iterate(block => {
                ballContainer.Iterate(ball => {if (CollisionDetection.Aabb(ball.GetShape(), block.GetShape()).Collision)
                {
                    switch (CollisionDetection.Aabb(ball.GetShape(), block.GetShape()).CollisionDir)
                    {
                        case CollisionDirection.CollisionDirLeft:
                        {
                            ball.GetShape().Direction.X *= -1.0f;
                            break;
                        }
                        case CollisionDirection.CollisionDirRight:
                        {
                            ball.GetShape().Direction.X *= -1.0f;
                            break;
                        }
                        case CollisionDirection.CollisionDirUp:
                        {
                            ball.GetShape().Direction.Y *= -1.0f;
                            break;
                        }
                        case CollisionDirection.CollisionDirDown:
                        {
                            ball.GetShape().Direction.Y *= -1.0f;
                            break;
                        }
                    }
                    block.HealthDown();
                    score.AddPoint();
                }});
                
            });
        }
    }
}