using DIKUArcade;
using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.EventBus;
using System.IO;

namespace Galaga.GalagaStates {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;
        private Window window;

        public MainMenu (ref Window window) {
            this.window = window;
            InitializeGameState();
        }
        public static MainMenu GetInstance(ref Window window) {
            return MainMenu.instance ?? (MainMenu.instance = new MainMenu(ref window));
        }
        public void GameLoop() {

        }
        public void UpdateGameLogic(){  
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
        public void HandleKeyEvent(string keyValue, string keyAction) {
            if (keyValue == "KEY_PRESS")
            {
                switch (keyAction) {
                    case "KEY_UP": 
                        activeMenuButton = 0;
                        break;
                    case "KEY_DOWN":
                        activeMenuButton = 1;
                        break;
                    case "KEY_ENTER":
                        if (activeMenuButton == 0)
                        {
                            GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                                                            GameEventType.GameStateEvent,
                                                            this,
                                                            "GAME_RUNNING",
                                                            "CHANGE_STATE", ""));
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
                new Image(Path.Combine("Assets", "Images", "TitleImage.png")));
            menuButtons = new Text[maxMenuButtons];
            menuButtons[0] = new Text("New Game", new Vec2F(0.4f, 0.4f), new Vec2F(0.4f, 0.4f));
            menuButtons[0].SetFontSize(40);
            menuButtons[1] = new Text("Quit", new Vec2F(0.4f, 0.3f), new Vec2F(0.4f, 0.4f));
            menuButtons[1].SetFontSize(40);
        }
    }
}