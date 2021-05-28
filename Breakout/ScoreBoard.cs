using DIKUArcade.Math;
using DIKUArcade.Graphics;

namespace Breakout
{
    public class ScoreBoard
    {
        private int score;
        private Text display;

        public int Score{
            get{return score;}
        }

        public ScoreBoard(Vec2F position, Vec2F extent) {
            score = 0;
            display = new Text("Score: " + score.ToString(), position, extent);
            display.SetColor(System.Drawing.Color.HotPink);
        }

        public void AddPoint()
        {
            score += 1;
            display.SetText("Score: " + score.ToString());
        }

        public void RenderScore() {
            display.RenderText();
        }
    }
}