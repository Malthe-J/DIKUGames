using DIKUArcade.Input;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using DIKUArcade.State;
namespace Galaga {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;
        private Player player;
        private EntityContainer<PlayerShot> playerShots;
        private IBaseImage playerShotImage;
        private List<Image> enemyStridesRed;
        private Squadron.ISquadron formation;
        private MovementStrategy.IMovementStrategy movement;
        private List<Image> explosionStrides;
        private const int EXPLOSION_LENGTH_MS = 500;
        private AnimationContainer enemyExplosions;

        private Score score;
        public GameRunning(){
            InitializeGameState();
        }
        public static GameRunning GetInstance(GalagaStates.GameStateType prevtype) {
            if(prevtype == GalagaStates.GameStateType.MainMenu) {
                return GameRunning.instance = new GameRunning();
            } 
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }

        public void GameLoop() {
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

                if (formation.Enemies.CountEntities() == 0){
                    var images = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
                    newFormation();
                    formation.CreateEnemies(images, enemyStridesRed);
                    NewMovement();
                }
            });

            formation.Enemies.Iterate(enemy => {
                if (enemy.Shape.Position.Y <= 0.1f) {
                   GalagaBus.GetBus().RegisterEvent(new GameEvent{ EventType = GameEventType.GameStateEvent, 
                                                                Message = "MainMenu", StringArg1 = "CHANGE_STATE"});
                }
            });

        }
        public void UpdateState(){
            movement.MoveEnemies(formation.Enemies);
            GameLoop();
        }
        public void RenderState() {
            player.Move();

            player.Render();

            formation.Enemies.RenderEntities();

            playerShots.RenderEntities();

            enemyExplosions.RenderAnimations();

            score.RenderScore();
        }
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            player.HandleKeyEvent(action, key);
            if (action == KeyboardAction.KeyPress) {
                switch (key){
                    case KeyboardKey.Space:
                        Vec2F temp = new Vec2F(player.GetPosition().X + player.GetExtent().X / 2, 0.2f);
                        playerShots.AddEntity(new PlayerShot(temp, playerShotImage));
                        break;
                    case KeyboardKey.Escape:
                        GalagaBus.GetBus().RegisterEvent(new GameEvent{ EventType = GameEventType.GameStateEvent, 
                                                                Message = "GamePaused", StringArg1 = "CHANGE_STATE"});
                        break;
                }
            }
        }
        public void InitializeGameState() {
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));
            var images = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            enemyStridesRed = ImageStride.CreateStrides(2,
                Path.Combine("Assets", "Images", "RedMonster.png"));
            explosionStrides = ImageStride.CreateStrides(8,   
            Path.Combine("Assets", "Images", "Explosion.png"));
            formation = new LineFormation();
            formation.CreateEnemies(images, enemyStridesRed);
            movement = new MovementStrategy.MoveDown();
            playerShots = new EntityContainer<PlayerShot>();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
            enemyExplosions = new AnimationContainer(formation.MaxEnemies);
            score = new Score(new Vec2F(0.75f, 0.6f), new Vec2F(0.4f, 0.4f));
        }

        private void AddExplosion(Vec2F position, Vec2F extent) {
            StationaryShape ExplodeMonster = new StationaryShape (position, extent);
            enemyExplosions.AddAnimation(ExplodeMonster, EXPLOSION_LENGTH_MS, new ImageStride (EXPLOSION_LENGTH_MS/8, explosionStrides));
        }

        private void newFormation()
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

        private void NewMovement()
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

        public void ResetState()
        {

        }
    }
}