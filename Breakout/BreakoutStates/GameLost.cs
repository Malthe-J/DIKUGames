using System;
using DIKUArcade.Input;
using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using System.IO;

namespace Breakout {
    public class GameLost : IGameState {
        private static GameLost instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private Text Titel;
        private int activeMenuButton;
        private int maxMenuButtons;
        private Window window;
        public GameLost(Window window) {
            this.window = window;
            ResetState();
        }
        public static GameLost GetInstance(Window window) {
            return GameLost.instance ?? (GameLost.instance = new GameLost(window));
        }
        public void UpdateState(){  
        }
        public void RenderState() {
            backGroundImage.RenderEntity();
            Titel.RenderText();
            for(int i = 0; i < maxMenuButtons; i++) {
                if (i == activeMenuButton) {
                    menuButtons[i].SetColor(System.Drawing.Color.White);
                    menuButtons[i].RenderText();
                } else {
                    menuButtons[i].SetColor(System.Drawing.Color.HotPink);
                    menuButtons[i].RenderText();
                }
            }
        }
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress)
            {
                switch (key) {
                    case KeyboardKey.Up: 
                        activeMenuButton = 0;
                        break;
                    case KeyboardKey.Down:
                        activeMenuButton = 1;
                        break;
                    case KeyboardKey.Enter:
                        if (activeMenuButton == 0)
                        {
                            BreakoutBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.GameStateEvent, 
                                                                            Message = "MainMenu", StringArg1 = "CHANGE_STATE"});
                        }
                        else if (activeMenuButton == 1)
                        {
                            window.CloseWindow();
                        }
                        break;
                }
            }
        }
        public void InitializeGameState() {

        }
        public void ResetState() {
            activeMenuButton = 0;
            maxMenuButtons = 2;
            backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f,0.0f), new Vec2F(1.0f,1.0f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));
            menuButtons = new Text[maxMenuButtons];
            Titel = new Text("GAME LOST", new Vec2F(0.3f, 0.4f), new Vec2F(0.5f, 0.5f));
            Titel.SetColor(System.Drawing.Color.Red);
            menuButtons[0] = new Text("Main Menu", new Vec2F(0.3f, 0.3f), new Vec2F(0.4f, 0.4f));
            menuButtons[0].SetFontSize(40);
            menuButtons[1] = new Text("Quit", new Vec2F(0.3f, 0.2f), new Vec2F(0.4f, 0.4f));
            menuButtons[1].SetFontSize(40);
        }
    }
}