using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga {
    public class Player {
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

        public void SetMoveLeft(bool val) {
            if (val) {
                moveLeft += MOVEMENT_SPEED;
            }else {
                moveLeft = 0;
            }

            UpdateDirection();
        }

        public void SetMoveRight(bool val) {
            if (val) {
                moveRight += MOVEMENT_SPEED;
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