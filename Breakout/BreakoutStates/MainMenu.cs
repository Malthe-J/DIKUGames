using DIKUArcade.Input;
using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using System.IO;

namespace Breakout.BreakoutStates {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;
        private Window window;

        public MainMenu (Window window) {
            this.window = window;
            InitializeGameState();
        }
        public static MainMenu GetInstance(Window window) {
            return MainMenu.instance ?? (MainMenu.instance = new MainMenu(window));
        }
        public void UpdateState(){  
        }
        public void RenderState() {
            backGroundImage.RenderEntity();
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
                                                                            Message = "GameRunning", StringArg1 = "CHANGE_STATE"});
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
            activeMenuButton = 0;
            maxMenuButtons = 2;
            backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f,0.0f),new Vec2F(1.0f,1.0f)),
                new Image(Path.Combine("Assets", "Images", "BreakoutTitleScreen.png")));
            menuButtons = new Text[maxMenuButtons];
            menuButtons[0] = new Text("New Game", new Vec2F(0.4f, 0.4f), new Vec2F(0.4f, 0.4f));
            menuButtons[0].SetFontSize(40);
            menuButtons[1] = new Text("Quit", new Vec2F(0.4f, 0.3f), new Vec2F(0.4f, 0.4f));
            menuButtons[1].SetFontSize(40);
        }

        public void ResetState()
        {

        }
    }
}