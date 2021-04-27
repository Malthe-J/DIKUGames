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

        public void KeyRelease(KeyboardKey key) {
                switch (key) {
                    case KeyboardKey.Left:
                        this.SetMoveLeft(false);
                        break;
                    case KeyboardKey.Right:
                        this.SetMoveRight(false);
                        break;
                }
        }
           public void KeyPress(KeyboardKey key) {
            switch (key){
                case KeyboardKey.Left:
                    this.SetMoveLeft(true);
                    break;
                case KeyboardKey.Right:
                    this.SetMoveRight(true);
                    break;
              }
        }
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
    }
}