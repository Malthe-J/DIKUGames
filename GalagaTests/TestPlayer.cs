using System;
using NUnit.Framework;
using Galaga;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.EventBus;
using System.IO;

namespace GalagaTests{
    public class TestPlayer{
    private Player Malthe;
    [SetUp]
    public void init(){
        DIKUArcade.Window.CreateOpenGLContext();
        Player Malthe = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
        

        eventBus = new GameEventBus<object>();
        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.
        InputEvent });
    }
    [Test]
    public void testMove(){
        Malthe.shape.Position.X = -1.0;
        Malthe.Move();
        Assert.AreEqual(Malthe.shape.Position.X, 0.0f);
    }






    }
}