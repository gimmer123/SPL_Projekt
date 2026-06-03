


using System.Data;
using GMDCore;
using GMDCore.Graphics;
using GMDCore.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SPL2;
using SPL2.Commands;
using SPL2.States.GameStates;

namespace SPL2.Entities;

public class Player : IEntity
{
    public Vector2 Position {get; set;} = new(Game1.VIRTUAL_WIDTH / 2, Game1.VIRTUAL_HEIGHT / 2);

    public float Speed => 40;

    public int Health { get; private set; }
    public int MaxHealth { get; private set; }

    public bool Remove {get; set;} = false;

    private ICommand _wKey, _aKey, _sKey, _dKey, _spaceKey;
    
    private Sprite _sprite;
    private PlayState _playState;
    public Circle Collider {get; set;}
    public Player(Sprite sprite, PlayState playState)
    {
        _sprite = sprite;
        _sprite.CenterOrigin();
        _wKey = new MoveUpCommand();
        _sKey = new MoveDownCommand();
        _aKey = new MoveLeftCommand();
        _dKey = new MoveRightCommand();

        _spaceKey = new PlayerShootCommand();
        
        _playState = playState;

        MaxHealth = 5;
        Health = MaxHealth;

        Collider = new Circle(new Point((int)Position.X, (int)Position.Y), (int)_sprite.Height / 2);
    }

    public void Update(GameTime gameTime)
    {
        HandleInput(gameTime);
        Collider = new Circle(new Point((int)Position.X, (int)Position.Y), (int)_sprite.Height / 2);
    }

    private void HandleInput(GameTime gameTime)
    {
        bool isWKeyDown = Core.Input.Keyboard.CurrentState.IsKeyDown(Keys.W);
        bool isSKeyDown = Core.Input.Keyboard.CurrentState.IsKeyDown(Keys.S);
        bool isAKeyDown = Core.Input.Keyboard.CurrentState.IsKeyDown(Keys.A);
        bool isDKeyDown = Core.Input.Keyboard.CurrentState.IsKeyDown(Keys.D);
        bool wasspacePressed = Core.Input.Keyboard.WasKeyJustPressed(Keys.Space);
            
        
        if (isWKeyDown)
        {
            _wKey.Execute(this, gameTime, _playState);
        }
        

        if (isAKeyDown)
        {
            _aKey.Execute(this, gameTime, _playState);
        }

        if (isSKeyDown)
        {
            _sKey.Execute(this, gameTime, _playState);
        }

        if (isDKeyDown)
        {
            _dKey.Execute(this, gameTime, _playState);
        }

        if (wasspacePressed)
        {
            _spaceKey.Execute(this, gameTime, _playState);
        }
        
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _sprite.Color = _playState.PlayerColor;
        _sprite.Draw(spriteBatch, Position);
    }

    public void TakeDamage()
    {
        Health--;
        if (Health <= 0)
        {
            Remove = true;
        }
    }

}