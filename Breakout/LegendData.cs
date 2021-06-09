using System.Collections.Generic;
using DIKUArcade.Graphics;
using System;
using System.IO;

namespace Breakout
{
    class LegendData
    {
        private Dictionary<char, string> textures;

        public LegendData(string[] LegendFile)
        {
            textures = new Dictionary<char, string>();
            LoadData(LegendFile);
        }

        /// <summary>
        /// Reads the char and image name from the level and
        /// inserts them into a dictionary
        /// </summary>
        /// <param name="LegendSplitData"></param>
        public void LoadData(string[] LegendSplitData)
        {
            for(int i = 3; i < LegendSplitData.Length-1;  i++)
            {
                if (string.IsNullOrWhiteSpace(LegendSplitData[3]))
                {
                    return;
                }
                if(textures.ContainsKey(LegendSplitData[i][0]) == false)
                {
                    int lastIndex = LegendSplitData[i].LastIndexOf("\r");
                    string temp = LegendSplitData[i].Substring(3, lastIndex - 3);
                    textures.Add(LegendSplitData[i][0], Path.Combine("Assets", "Images", temp));
                }
            }
        }

        public Dictionary<char, string> GetDic(){
            return textures;
        }
    }
}