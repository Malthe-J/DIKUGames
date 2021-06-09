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
using DIKUArcade.Timers;
using Breakout.PowerUp;
using DIKUArcade.Math;
using System;

namespace Breakout {
    public class GameRunning : IGameState, IGameEventProcessor {
        private static GameRunning instance = null;
        private Player player;
        private Level level;
        private List<Level> levels;
        private Ball ball;
        private ScoreBoard score;
        private StaticTimer timer;

        private int activeLevel = 0;
        private long startTime; 
        private int health;
        private int time;
        private Text displayHealth;
        private Text displayTimer;
        private EntityContainer<Ball> ballContainer;

        public GameRunning(){
            //BreakoutBus.GetBus().Subscribe(GameEventType.TimedEvent, this);
            //BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            health = 3;
            ResetState();
        }
        public static GameRunning GetInstance(BreakoutStates.GameStateType state) {
            if (state == BreakoutStates.GameStateType.MainMenu)
            {
                return new GameRunning();
            }
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }

        public void AddHealth() {
            health++;
            displayHealth.SetText("HP: " + health.ToString());
        }

        private void AddBall() {
            Ball ball = new Ball(new DynamicShape(new Vec2F(player.GetPosition().X + player.GetExtent().X/2, player.GetPosition().Y + player.GetExtent().Y), 
                                new Vec2F(0.03f, 0.03f)), new Image(Path.Combine("Assets", "Images", "ball.png")));
            ballContainer.AddEntity(ball);
            ball.Start();
        }

        public void GameLoop() {
            player.Move();
            PlayerHealthDown();
            ballContainer.Iterate(ball => {ball.Move();});
            ballContainer.Iterate(ball => {ball.CollideWithPlayer(player);});
            levels[activeLevel].GetPowerUps().Iterate(powerUp => {powerUp.Update();});
            PowerUpCollideWithPlayer();
            CollideWithBlock(levels[activeLevel].GetDestroyableBlocks(), levels[activeLevel].GetUndestroyableBlocks());
            if (startTime + 1000 < StaticTimer.GetElapsedMilliseconds()) {
                time -= 1; 
                displayTimer.SetText("Time: " + time.ToString());
                startTime = StaticTimer.GetElapsedMilliseconds();
            }
            ShouldGameEnd();
            ChangeLevel();
        }
        public void UpdateState(){
            GameLoop();
        }
        public void RenderState() {
            levels[activeLevel].Render();
            player.Render();
            ballContainer.RenderEntities();
            score.RenderScore();
            displayHealth.SetText("HP: " + health.ToString());
            displayHealth.RenderText();
            if (levels[activeLevel].MetaData.Time != 0) {
                displayTimer.RenderText();
            }

            levels[activeLevel].GetPowerUps().Iterate(powerUp => {powerUp.Render();});
        }

        public void PlayerHealthDown(){
            if (ballContainer.CountEntities() == 0){
                health--;
                displayHealth.SetText("HP: " + health.ToString());
                ballContainer.AddEntity(new Ball(
                new DynamicShape(new Vec2F(player.GetPosition().X + player.GetExtent().X/2, player.GetPosition().Y + player.GetExtent().Y), 
                                new Vec2F(0.03f, 0.03f)),
                new Image(Path.Combine("Assets", "Images", "ball.png"))));

                ballContainer.Iterate(ball => {ball.Start();});
            }
        }
        public void ShouldGameEnd(){
            if(health == 0 || (levels[activeLevel].MetaData.Time != 0 &&  time < 0)){
                BreakoutBus.GetBus().RegisterEvent(new GameEvent{ EventType = GameEventType.GameStateEvent, 
                                                                Message = "GameLost", StringArg1 = "CHANGE_STATE"});
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
                        levels[activeLevel].GetDestroyableBlocks().ClearContainer();
                        break;
                }
            }
        }

        private void ChangeLevel(){
            if (levels[activeLevel].GetDestroyableBlocks().CountEntities() == 0) {
                 if (activeLevel == levels.Count - 1)
                {
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent{ EventType = GameEventType.GameStateEvent, 
                                                                Message = "GameWon", StringArg1 = "CHANGE_STATE"});
                }
                if (activeLevel < levels.Count - 1)
                {
                    activeLevel++;
                    ResetState(); 
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
            timer = new StaticTimer();

            //Display health
            displayHealth = new Text("HP: " + health.ToString(), new Vec2F(0.75f, 0.5f), new Vec2F(0.4f, 0.4f));
            displayHealth.SetColor(System.Drawing.Color.HotPink);


            //Ball container
            ballContainer = new EntityContainer<Ball>();
            ballContainer.AddEntity(new Ball(
                new DynamicShape(new Vec2F(player.GetPosition().X + player.GetExtent().X/2, player.GetPosition().Y + player.GetExtent().Y), 
                                new Vec2F(0.03f, 0.03f)),
                new Image(Path.Combine("Assets", "Images", "ball.png"))));

            ballContainer.Iterate(ball => {ball.Start();});
            time = levels[activeLevel].MetaData.Time;
            startTime = (int)StaticTimer.GetElapsedMilliseconds();
            //Display timer
            displayTimer = new Text("Time: " + time.ToString(), new Vec2F(0.25f, 0.5f), new Vec2F(0.4f, 0.4f));
            displayTimer.SetColor(System.Drawing.Color.HotPink);
            System.Console.WriteLine(levels[activeLevel].GetPowerUps().CountEntities());
        }



        public void CollideWithBlock(EntityContainer<Block> blocks, EntityContainer<Block> unbreakble)
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

            unbreakble.Iterate(block => {
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
                }});
            });
        }

        public void PowerUpCollideWithPlayer(){
            levels[activeLevel].GetPowerUps().Iterate(powerUp => {
                //if(CollisionDetection.Aabb(powerUp.GetDynamicShape(), player.GetShape()).Collision) 
                if (Math.Abs(player.GetShape().Position.X-powerUp.GetDynamicShape().Position.X)<0.05
                &&   Math.Abs(player.GetShape().Position.Y-powerUp.GetDynamicShape().Position.Y)<0.05)
                {;
                    powerUp.Delete();
                    powerUp.AddEffect();;
                }
            });
        }

        public void ProcessEvent(GameEvent gameEvent) {
            switch (gameEvent.StringArg1) {
                case "EFFECT":
                    switch(gameEvent.Message){
                        case "ExtraLife":
                            AddHealth();
                            break;
                        case "ExtraBall":
                            AddBall();
                            break;
                    }
                    break;
            }
        }
    }
}