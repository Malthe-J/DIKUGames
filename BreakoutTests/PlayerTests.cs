using System;
using NUnit.Framework;
using Breakout;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using DIKUArcade.GUI;

namespace BreakoutTests
{
    public class PlayerTests
    {
    private Player Casper;
    [SetUp]
    public void init(){
        Window.CreateOpenGLContext();
        Casper = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
        }
    [Test]
    public void testMove(){
        Casper.GetShape().Position.X = -1.0f;
        Casper.Move();
        Assert.AreEqual(Casper.GetShape().Position.X, 0.0f);
        }
    }
}