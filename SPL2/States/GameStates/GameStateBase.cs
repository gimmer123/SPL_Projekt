using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SPL2.States.GameStates;


public abstract class GameStateBase(Game1 game)
{
    public Game1 Game { get; private set; } = game;

    public virtual void Enter() { }
    public virtual void Exit() { }
    public abstract void Update(GameTime gameTime);
    public abstract void Draw(SpriteBatch spriteBatch);

}
