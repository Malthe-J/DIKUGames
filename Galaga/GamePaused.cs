using DIKUArcade.Input;
using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using System.IO;
namespace Galaga.GalagaStates {
    public class GamePaused : IGameState {

        private static GamePaused instance = null;
        private Text Title;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;
        private Window window;

        public GamePaused(ref Window window) {
            this.window = window;
            InitializeGameState();
        }

        public static GamePaused GetInstance(ref Window window) {
            return GamePaused.instance ?? (GamePaused.instance = new GamePaused(ref window));
        }

        public void ResetState() {
        }

        public void UpdateState() {

        }

        public void RenderState() {
            Title.SetColor(System.Drawing.Color.Yellow);
            Title.RenderText();
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

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key){
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
                            GalagaBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.GameStateEvent, 
                                                                            Message = "GameRunning", StringArg1 = "CHANGE_STATE"});
                        }
                        else if (activeMenuButton == 1)
                        {
                            GalagaBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.GameStateEvent, 
                                                                            Message = "MainMenu", StringArg1 = "CHANGE_STATE"});
                        }
                        break;
                }
            }
        }

        private void InitializeGameState(){
            activeMenuButton = 0;
            maxMenuButtons = 2;
            Title = new Text("Game Paused", new Vec2F(0.3f, 0.5f), new Vec2F(0.4f, 0.4f));
            Title.SetFontSize(60);
            menuButtons = new Text[maxMenuButtons];
            menuButtons[0] = new Text("Continue", new Vec2F(0.37f, 0.2f), new Vec2F(0.4f, 0.4f));
            menuButtons[0].SetFontSize(40);
            menuButtons[1] = new Text("Main Menu", new Vec2F(0.35f, 0.1f), new Vec2F(0.4f, 0.4f));
            menuButtons[1].SetFontSize(40);
        }
    }
}