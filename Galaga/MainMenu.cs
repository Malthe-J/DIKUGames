using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;

namespace Galaga.GalagaStates {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;

        public MainMenu () {
            activeMenuButton = 0;
            maxMenuButtons = 2;
            backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f,0.0f),new Vec2F(1.0f,1.0f)),
                new Image(Path.Combine("Assets", "Images", "TitleImage.png")));
            menuButtons = new Text[maxMenuButtons];
        }
        public static MainMenu GetInstance() {
            return MainMenu.instance ?? (MainMenu.instance = new MainMenu());

        }
        public void GameLoop() {

        }
        public void UpdateGameLogic(){

        }
        public void RenderState() {
            
        }
        public void HandleKeyEvent(string press, string release) {

        }
        public void InitializeGameState() {

        }
    }
}