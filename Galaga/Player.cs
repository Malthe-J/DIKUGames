using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.EventBus;

namespace Galaga {
    public class Player : IGameEventProcessor<object> {
        private Entity entity;
        private DynamicShape shape;

        private float moveLeft, moveRight;

        private const float MOVEMENT_SPEED = 0.01f;

        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
            moveLeft = 0.0f; 
            moveRight = 0.0f;
        }

        public void Render() {
            entity.RenderEntity();
        }

        public void Move() {
            if(shape.Position.X < 0.0)
            {
                shape.Position.X = 0.0f;     
            }
          else if (shape.Position.X > 0.9)
          {
              shape.Position.X = 0.9f;
          }
          else {
              shape.Move();
          }
        }

        private void SetMoveLeft(bool val) {
            if (val) {
                moveLeft = MOVEMENT_SPEED;
            }else {
                moveLeft = 0;
            }

            UpdateDirection();
        }

        public void KeyRelease(string key) {
                switch (key) {
                    case "KEY_LEFT" :
                        this.SetMoveLeft(false);
                        break;
                    case "KEY_RIGHT" :
                        this.SetMoveRight(false);
                        break;
                }
        }
           public void KeyPress(string key) {
            switch (key){
                case "KEY_LEFT":
                    this.SetMoveLeft(true);
                    break;
                case "KEY_RIGHT":
                    this.SetMoveRight(true);
                    break;
              }
        }
            public void ProcessEvent(GameEventType type, GameEvent<object> gameEvent) {
            switch (gameEvent.Parameter1) {
                case "KEY_PRESS":
                    KeyPress(gameEvent.Message);
                    break;
                case "KEY_RELEASE":
                    KeyRelease(gameEvent.Message);
                    break;
                default:
                break;
            }
        }  
        private void SetMoveRight(bool val) {
            if (val) {
                moveRight = MOVEMENT_SPEED;
            } else {
                moveRight = 0;
            }

            UpdateDirection();
        }

        private void UpdateDirection()
        {
            shape.Direction.X = moveRight - moveLeft;
        }
        public Vec2F GetPosition(){
            return shape.Position;
        }
    }
}