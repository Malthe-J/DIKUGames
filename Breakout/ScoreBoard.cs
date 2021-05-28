using DIKUArcade.Math;
using DIKUArcade.Graphics;

namespace Breakout
{
    public class ScoreBoard
    {
        private static int score = 0;
        private Text display;

        public int Score{
            get{return score;}
        }

        public ScoreBoard(Vec2F position, Vec2F extent) {
            display = new Text("Score: " + score.ToString(), position, extent);
            display.SetColor(System.Drawing.Color.HotPink);
        }

        public void AddPoint()
        {
            score += 1;
            display.SetText("Score: " + score.ToString());
        }

        public static void AddPoint(int point){
            score += point;
        }

        public void RenderScore() {
            display.SetText("Score: " + score.ToString());
            display.RenderText();
        }
    }
}