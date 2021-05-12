using System;
using System.IO;


namespace Breakout{
    public class Loader
    {

        private string file;
        private string[] SplitData;
        public Loader(string FilePath){
            try
            {
                file = File.ReadAllText(FilePath);
                if (string.IsNullOrEmpty(file))
                {
                    file = File.ReadAllText(Path.Combine("Assets", "Levels", "wall.txt"));
                }
            }
            catch (FileNotFoundException)
            {
                file = File.ReadAllText(Path.Combine("Assets", "Levels", "wall.txt"));
            }
            SplitData = file.Split('/');
        }

        public string[] GetMetaData()
        {
            return  SplitData[1].Split('\n');
        }

        public string[] GetLegendData()
        {
            return SplitData[2].Split('\n');
        }

        public string[] GetMapData()
        {
            return SplitData[0].Split('\n');
        }
    }
}