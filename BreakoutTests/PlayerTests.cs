using System;
using NUnit.Framework;
using Breakout;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;

namespace BreakoutTests
{
    public class PlayerTests
    {
    private Player Casper;
    [SetUp]
    public void init(){
        Window.CreateOpenGLContext();
        Player Casper = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
        }
    [Test]
    public void testMove(){
        Casper.shape.Position.X = -1.0;
        Casper.Move();
        Assert.AreEqual(Casper.shape.Position.X, 0.0f);
        }
    }
}