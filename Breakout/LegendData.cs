using System.Collections.Generic;
using DIKUArcade.Graphics;
using System;
using System.IO;

namespace Breakout
{
    class LegendData
    {
        static Dictionary<char, IBaseImage> textures;
        public LegendData(string[] LegendFile)
        {
            textures = new Dictionary<char, IBaseImage>();
            LoadData(LegendFile);
        }

        public void LoadData(string[] LegendSplitData)
        {
            Console.WriteLine("-------------------LegendData-------------------");
            for(int i = 3; i < LegendSplitData.Length-1;  i++)
            {
                if(!textures.ContainsKey(LegendSplitData[i][0]))
                {
                    int lastIndex = LegendSplitData[i].LastIndexOf("\r");
                    string temp = LegendSplitData[i].Substring(3, lastIndex - 3);
                    textures.Add(LegendSplitData[i][0], new Image(Path.Combine("Assets", "Images", temp)));
                    Console.WriteLine($"Line: " + LegendSplitData[i]);
                    Console.WriteLine($"char key: " + LegendSplitData[i][0]);
                    Console.WriteLine($"Picture file: " + temp);
                }
            }
        }
    }
}