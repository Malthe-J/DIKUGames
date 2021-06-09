using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;

namespace Breakout {
    public class Player {
        private Entity entity;
        private DynamicShape shape;

        private float moveLeft, moveRight;
        private Text displayHealth;

        private int health;
        public int Health{
            get{return health;}
        }
        
        private const float MOVEMENT_SPEED = 0.03f;
        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
            moveLeft = 0.0f; 
            moveRight = 0.0f;
            health = 3;
            
        }
        public void HealthDown() {
            health--;
        }

        public void Render() {
            entity.RenderEntity();
        }

        /// <summary>
        /// Makes the player able to move and checks if the player is on its way
        /// to be out of bounds, and if so, makes movement in the given direction
        /// impossible
        /// </summary>
        public void Move() {
            UpdateDirection();
            if(shape.Position.X + shape.Direction.X < 0.0)
            {
                shape.Position.X = 0.0f;     
            }
          else if (shape.Position.X + shape.Direction.X > 1.0f - shape.Extent.X)
          {
                shape.Position.X = 1.0f - shape.Extent.X;
          }
          else {
                shape.Move();
          }
        }

        private void SetMoveLeft(bool val) {
            if (val && shape.Position.X + MOVEMENT_SPEED > 0.0f) {
                moveLeft = MOVEMENT_SPEED;
                UpdateDirection();
            }else {
                moveLeft = 0;
            }

            
        }
        /// <summary>
        /// Checks if the given key has been released
        /// </summary>
        /// <param name="key"></param>

        public void KeyRelease(KeyboardKey key) {
                switch (key) {
                    case KeyboardKey.Left:
                    case KeyboardKey.A:
                        this.SetMoveLeft(false);
                        break;
                    case KeyboardKey.Right:
                    case KeyboardKey.D:
                        this.SetMoveRight(false);
                        break;
                }
        }
            /// <summary>
            /// Checks if the given key has been pressed
            /// </summary>
            /// <param name="key"></param>
           public void KeyPress(KeyboardKey key) {
            switch (key){
                case KeyboardKey.Left:
                case KeyboardKey.A:
                    this.SetMoveLeft(true);
                    break;
                case KeyboardKey.Right:
                case KeyboardKey.D:
                    this.SetMoveRight(true);
                    break;
              }
        }
            /// <summary>
            /// Checks if the given key has been pressed, which key and if the key
            /// has been released
            /// </summary>
            /// <param name="action"></param>
            /// <param name="key"></param>
            public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            switch (action) {
                case KeyboardAction.KeyPress:
                    KeyPress(key);
                    break;
                case KeyboardAction.KeyRelease:
                    KeyRelease(key);
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

        public Vec2F GetExtent(){
            return shape.Extent;
        }

        public Shape GetShape(){
            return shape;
        }
        
    }
}