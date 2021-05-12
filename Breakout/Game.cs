using System;
using System.IO;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;

namespace Breakout
{
    public class Game : DIKUGame {
        private Player player;
        private Level Levels;
         
        public Game() : base(new WindowArgs {Title = "Breakout", Width = 500, Height = 500}) {
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.15f/2.0f, 0.027f/2.0f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));
            window.SetKeyEventHandler(KeyHandler);

            Levels = new Level(Path.Combine("Assets","Levels", "Level3.txt"));
        }
        public override void Update() {
            player.Move();
        }
        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            player.HandleKeyEvent(action, key);
            if (action==KeyboardAction.KeyPress && key==KeyboardKey.Escape) {
                window.CloseWindow();
            }

        }
        public override void Render() {
            player.Render();
            Levels.render();
        }

    }
}
