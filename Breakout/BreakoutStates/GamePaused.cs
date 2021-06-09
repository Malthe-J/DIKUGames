using System;
using DIKUArcade.State;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using System.IO;
namespace Breakout.BreakoutStates {
    public class GamePaused : IGameState {

        private static GamePaused instance = null;
        private Text Title;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;

        public GamePaused() {
            InitializeGameState();
        }
        /// <summary>
        /// Gets an instance of the GamePaused window
        /// </summary>
        /// <returns> GamePaused state</returns>
        public static GamePaused GetInstance() {
            return GamePaused.instance ?? (GamePaused.instance = new GamePaused());
        }

        public void ResetState() {
        }

        public void UpdateState() {

        }
        /// <summary>
        /// Renders the GamePaused state
        /// </summary>
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
        /// <summary>
        /// Handles the changing of menu buttons in the GamePaused state
        /// </summary>
        /// <param name="action"></param>
        /// <param name="key"> keyboard key</param>
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
                            BreakoutBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.GameStateEvent, 
                                                                            Message = "GameRunning", StringArg1 = "CHANGE_STATE"});
                        }
                        else if (activeMenuButton == 1)
                        {   
                            BreakoutBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.GameStateEvent, 
                                                                            Message = "MainMenu", StringArg1 = "CHANGE_STATE"});
                        }
                        break;
                }
            }
        }
        /// <summary>
        /// Initializes to the GamePaused game state
        /// </summary>
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