


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

    public bool Remove {get; set;} = false;

    private ICommand _wKey, _aKey, _sKey, _dKey, _spaceKey;
    
    private Sprite _sprite;
    private PlayState _playState;
    public Player(Sprite sprite, PlayState playState)
    {
        _sprite = sprite;
        _sprite.CenterOrigin();
        _wKey = new MoveUpCommand();
        _sKey = new MoveDownCommand();
        _aKey = new MoveLeftCommand();
        _dKey = new MoveRightCommand();

        _spaceKey = new ShootCommand();
        
        _playState = playState;
    }

    public void Update(GameTime gameTime)
    {
        HandleInput(gameTime);
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
        _sprite.Draw(spriteBatch, Position);
    }

}