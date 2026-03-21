using System;
using GMDCore.Graphics;
using GMDCore.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SPL2.Commands;

namespace SPL2.States.GameStates;

public class PlayState(Game1 game) : GameStateBase(game)
{
    private Player _player;

    public override void Enter()
    {
        TextureAtlas atlas = TextureAtlas.FromFile(Game.Content, "images/atlas-definitions.xml");
        Sprite playerSprite = atlas.CreateSprite("snake");
        _player = new Player(playerSprite);
    }

    public override void Update(GameTime gameTime)
    {
        _player.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(transformMatrix: Game.ScreenScaleMatrix, samplerState: SamplerState.PointClamp);
        _player.Draw(spriteBatch);
        spriteBatch.End();
    }
}
