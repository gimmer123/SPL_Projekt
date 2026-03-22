using System;
using GMDCore.Graphics;
using GMDCore.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SPL2.Commands;
using SPL2.World;

namespace SPL2.States.GameStates;

public class PlayState(Game1 game) : GameStateBase(game)
{
    private Player _player;
    private Floor _floor;

    public override void Enter()
    {
        TextureAtlas atlas = TextureAtlas.FromFile(Game.Content, "images/atlas-definitions.xml");

        // load floor tileset from atlas region

        TextureRegion floorRegion = atlas.GetRegion("floor-tiles");
        Tileset floorTileset = new(floorRegion, 16, 16); 

        int columns = Game1.VIRTUAL_WIDTH / floorTileset.TileWidth;
        int rows = Game1.VIRTUAL_HEIGHT / floorTileset.TileHeight;
        _floor = new Floor(floorTileset, columns, rows);

        Sprite playerSprite = atlas.CreateSprite("snake");
        _player = new Player(playerSprite);
    }

    public override void Update(GameTime gameTime)
    {
        _player.Update(gameTime);
        _floor.Update(gameTime); // change this later, we dont need to update this globally just when the player is outside thresholds
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(transformMatrix: Game.ScreenScaleMatrix, samplerState: SamplerState.PointClamp);
        _floor.Tilemap.Draw(spriteBatch);
        _player.Draw(spriteBatch);
        spriteBatch.End();
    }
}
