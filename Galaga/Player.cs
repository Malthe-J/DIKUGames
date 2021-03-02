using DIKUArcade.Entities;
using DIKUArcade.Graphics;

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
            if(Direction.X>0 && Direction.X<500){
               shape.Move() 
            }  
        }

        public void SetMoveLeft(bool val) {
            if (val == true) {
                moveLeft -= MOVEMENT_SPEED;
            }else {
                moveLeft = 0;
            }
        }

        public void SetMoveRight(bool val) {
            if (val == true) {
                moveRight += MOVEMENT_SPEED;
            } else {
                moveRight = 0;
            }

            UpdateDirection();
        }

        private void UpdateDirection()
        {
            shape.Direction.X = moveRight + moveLeft;
        }
    }
}