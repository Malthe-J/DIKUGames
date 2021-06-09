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
        /// <summary>
        /// Gets an instance of the GameRunning window
        /// </summary>
        /// <param name="state"> enum of BreakoutStateType</param>
        /// <returns> GameRuning state</returns>
        public static GameRunning GetInstance(BreakoutStates.GameStateType state) {
            if (state == BreakoutStates.GameStateType.MainMenu)
            {
                return new GameRunning();
            }
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }
        /// <summary>
        /// Adds health to the player
        /// </summary>
        public void AddHealth() {
            health++;
            displayHealth.SetText("HP: " + health.ToString());
        }
        /// <summary>
        /// The loop containing the functions to be called for each game update
        /// </summary>
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
        /// <summary>
        /// continuously calls the GameLoop function to update the state of the game
        /// </summary>
        public void UpdateState(){
            GameLoop();
        }
        /// <summary>
        /// Renders the level, player, ball, score, health, level and timer for the current level
        /// </summary>
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
        /// <summary>
        /// Decrements the health of the player, and adds a new ball to the game
        /// </summary>
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
        /// <summary>
        /// Checks if the game should end
        /// </summary>
        public void ShouldGameEnd(){
            if(health == 0 || (levels[activeLevel].MetaData.Time != 0 &&  time < 0)){
                BreakoutBus.GetBus().RegisterEvent(new GameEvent{ EventType = GameEventType.GameStateEvent, 
                                                                Message = "GameLost", StringArg1 = "CHANGE_STATE"});
            }
        }
        /// <summary>
        /// Handles the use of the escape and f key during the GameRunning state
        /// </summary>
        /// <param name="action"></param>
        /// <param name="key"> keyboard key</param>
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
        /// <summary>
        /// Changes the level to the GameWon state, or calls ResetState if an error occured
        /// </summary>
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
        /// <summary>
        /// Resets the game to an initial state of GameRunning
        /// </summary>
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
        }


        /// <summary>
        /// Handles the event that the ball collides with a block
        /// </summary>
        /// <param name="blocks"> normal block</param>
        /// <param name="unbreakable"> Block of type unbreakable</param>
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
        /// <summary>
        /// Handles the event that a powerup collides with the player
        /// </summary>
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
        /// <summary>
        /// Handles adding of health to the player and adding extra balls
        /// </summary>
        /// <param name="gameEvent"></param>
        public void ProcessEvent(GameEvent gameEvent) {
            switch (gameEvent.StringArg1) {
                case "EFFECT":
                    switch(gameEvent.Message){
                        case "ExtraLife":
                            AddHealth();
                            break;
                    }
                    break;
            }
        }
    }
}