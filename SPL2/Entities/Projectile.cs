using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SPL2.Commands;
using SPL2.States.GameStates;

namespace SPL2.Entities;

public class Projectile : IEntity
{
    public Vector2 Position {get; set;}
    public float Speed => 60;
    private ICommand _direction;
    private Sprite _sprite;
    private PlayState _playState;
    public bool Remove {get; set;} = false;
    private float _duration => 4;
    private float _spawnTime;

    public Projectile(Sprite sprite, ICommand direction, IEntity origin, PlayState playState, GameTime gameTime)
    {
        _direction = direction;
        Position = new(origin.Position.X, origin.Position.Y);
        _spawnTime = gameTime.ElapsedGameTime.Seconds;
        _sprite = sprite;
    }

    public void Update(GameTime gameTime)
    {
        _direction.Execute(this, gameTime, _playState);

        if (_spawnTime - gameTime.ElapsedGameTime.TotalSeconds >= _duration)
        {
            Remove = true;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _sprite.Draw(spriteBatch, Position);
    }
}