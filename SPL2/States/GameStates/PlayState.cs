using System;
using GMDCore.Graphics;
using GMDCore.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SPL2.Commands;
using SPL2.World;
using SPL2.Entities;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace SPL2.States.GameStates;

public class PlayState(Game1 game) : GameStateBase(game)
{
    public Player _player { get; private set; }
    public Floor Floor;
    public List<IEntity> Entities = new();
    public List<IEntity> PendingAdd= new();

    public Sprite ProjectileSprite;

    public override void Enter()
    {
        TextureAtlas atlas = TextureAtlas.FromFile(Game.Content, "images/atlas-definitions.xml");

        // load floor tileset from atlas region

        TextureRegion floorRegion = atlas.GetRegion("floor-tiles");
        Tileset floorTileset = new(floorRegion, 16, 16); 

        int columns = Game1.VIRTUAL_WIDTH / floorTileset.TileWidth;
        int rows = Game1.VIRTUAL_HEIGHT / floorTileset.TileHeight;
        Floor = new Floor(floorTileset, columns, rows);

        Sprite playerSprite = atlas.CreateSprite("snake");
        ProjectileSprite = playerSprite;
        _player = new Player(playerSprite, this);
        Entities.Add(_player);
    }

    public override void Update(GameTime gameTime)
    {
        Floor.Update(gameTime);

        foreach (IEntity entity in PendingAdd)
        {
            Entities.Add(entity);
        }
        PendingAdd.Clear();
        Entities.ForEach(entity => entity.Update(gameTime));
        Entities.RemoveAll(entity => entity.Remove);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(transformMatrix: Game.ScreenScaleMatrix, samplerState: SamplerState.PointClamp);
        Floor.Draw(spriteBatch);
        Entities.ForEach (entity => entity.Draw(spriteBatch));
        spriteBatch.End();
    }
}
