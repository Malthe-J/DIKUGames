using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Timers;

namespace Breakout {
    public class Level{
        private Loader loader;
        private LegendData legend;
        private Map map;
        private MetaData metaData;


        public MetaData MetaData{
            get{return metaData;}
        }

        public Level(string FilePath){
            loader = new Loader(FilePath);
            metaData = new MetaData(loader.GetMetaData());
            legend = new LegendData(loader.GetLegendData());
            map = new Map(loader.GetMapData(), legend.GetDic(), metaData);
        }

        public void Render(){
            map.Render();
        }

        public EntityContainer<Block> GetBlocks(){
            return map.GetBlocks();
        }
        public EntityContainer<Block> GetDestroyableBlocks(){
            return map.GetDestroyableBlocks();
        }
    }
}