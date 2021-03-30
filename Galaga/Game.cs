using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;
using DIKUArcade.EventBus;
using DIKUArcade.Physics;


namespace Galaga
{
    public class Game : IGameEventProcessor<object> {
        private GameEventBus<object> eventBus;
        private Player player;
        private Window window;
        private GameTimer gameTimer;
        private EntityContainer<Enemy> enemies;
        private EntityContainer<PlayerShot> playerShots;
        private IBaseImage playerShotImage;
        private AnimationContainer enemyExplosions;
        private List<Image> explosionStrides;
        private const int EXPLOSION_LENGTH_MS = 500;
        private List<Image> enemyStridesRed;

        private Squadron.ISquadron formation;

        private MovementStrategy.IMovementStrategy movement;

        private Enemy testEnemy;

        private bool EndGame;

        private Score score;



        public Game(){
            window = new Window("Galaga", 500, 500);
            gameTimer = new GameTimer(60, 60);
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));
            eventBus = new GameEventBus<object>();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.
            InputEvent });
            window.RegisterEventBus(eventBus);
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.InputEvent, player);
            var images = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            const int numEnemies = 8;
            /*enemies = new EntityContainer<Enemy>(numEnemies);
            for (int i = 0; i < numEnemies; i++){
                enemies.AddEntity(new Enemy(new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)), new ImageStride(80, images)));
            }*/
            testEnemy = new Enemy(new DynamicShape(new Vec2F(0.0f, 0.9f), new Vec2F(0.1f, 0.1f)), new ImageStride(80, images));
            formation = new LineFormation();
            movement = new MovementStrategy.MoveDown();
            formation.CreateEnemies(images, enemyStridesRed);
            playerShots = new EntityContainer<PlayerShot>();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));  
            enemyExplosions = new AnimationContainer(numEnemies);   
            explosionStrides = ImageStride.CreateStrides(8,   
            Path.Combine("Assets", "Images", "Explosion.png")); 
            enemyStridesRed = ImageStride.CreateStrides(2,
                Path.Combine("Assets", "Images", "RedMonster.png"));
            EndGame = false;
            score = new Score(new Vec2F(0.75f, 0.6f), new Vec2F(0.4f, 0.4f));
        }
        public void KeyPress(string key) {
            switch (key){
                case "KEY_LEFT":

                    break;
                case "KEY_RIGHT":

                    break;
                case "KEY_ESCAPE":
                    window.CloseWindow();
                    break;
                case "KEY_SPACE":
                    Vec2F temp = new Vec2F(player.GetPosition().X + 0.05f - 0.004f, 0.2f);
                    playerShots.AddEntity(new PlayerShot(temp, playerShotImage));
                    break;
                    
              }
        }   
        public void ProcessEvent(GameEventType type, GameEvent<object> gameEvent) {
            switch (gameEvent.Parameter1) {
                case "KEY_PRESS":
                    KeyPress(gameEvent.Message);
                    break;
                default:
                break;
            }
        }
        public void Run() {
            while (window.IsRunning()){
                gameTimer.MeasureTime();

                while (gameTimer.ShouldUpdate()) {
                    window.PollEvents();
                    eventBus.ProcessEvents();
                    IterateShots();
                    movement.MoveEnemies(formation.Enemies);
                    if (formation.Enemies.CountEntities() == 0){
                        var images = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
                        newFormation();
                        formation.CreateEnemies(images, enemyStridesRed);
                        NewMovement();
                    }
                    ShouldEnd();
                }

                if (gameTimer.ShouldRender()) {
                    window.Clear();

                    if (!EndGame)
                    {
                        player.Move();

                        player.Render();

                    //enemies.RenderEntities();
                        formation.Enemies.RenderEntities();
                    //testEnemy.RenderEntity();
                    
                        playerShots.RenderEntities();

                        enemyExplosions.RenderAnimations();
                    }

                    score.RenderScore();

                    window.SwapBuffers();

                }

                if (gameTimer.ShouldReset())
                {
                    window.Title = $"Galaga | (UPS, FPS): ({gameTimer.CapturedUpdates}, {gameTimer.CapturedFrames})";
                }
            }
        }
        private void IterateShots(){
            playerShots.Iterate(shot =>{
                shot.Shape.Position.Y +=0.025f;
                if(shot.Shape.Position.Y > 1.0){
                    shot.DeleteEntity();
                }
                else {
                    formation.Enemies.Iterate(enemy =>{
                        if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape).Collision){
                            shot.DeleteEntity();
                            enemy.hitpoints--;
                            if(enemy.hitpoints==5) {
                                enemy.Image= new ImageStride (80,enemyStridesRed);
                                enemy.EnragedSpeed();
                            }

                            if(enemy.hitpoints<=0) {
                                AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                                enemy.DeleteEntity();
                                score.AddPoint();
                            }                        
                        }

                    });
                }
            });
        }
        public void AddExplosion(Vec2F position, Vec2F extent) {
            StationaryShape ExplodeMonster = new StationaryShape (position, extent);
            enemyExplosions.AddAnimation(ExplodeMonster, EXPLOSION_LENGTH_MS, new ImageStride (EXPLOSION_LENGTH_MS/8, explosionStrides));
        }

        void newFormation()
        {
            var rand = new System.Random();
            int f = rand.Next(1, 4); // random number for formation
            switch(f)
            {
                case 1:
                    formation = new LineFormation();
                    return;
                case 2:
                    formation = new VFormation();
                    return;
                case 3:
                    formation = new ReverseVFormation();
                    return;
            }
        }

        void NewMovement()
        {
            var rand = new System.Random();
            int m = rand.Next(1, 3); // random number for movement
            switch (m)
            {
                case 1:
                    movement = new MovementStrategy.MoveDown();
                    return;
                case 2:
                    movement = new MovementStrategy.ZigZagMove();
                    return;
            }
        }

        void ShouldEnd()
        {
            formation.Enemies.Iterate(enemy => {
                if (enemy.Shape.Position.Y <= 0.1f) {
                    EndGame = true;
                }
            });
        }
    }
}