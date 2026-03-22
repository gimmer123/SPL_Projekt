

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SPL2.Entities;

public interface IEntity
{
    public Vector2 Position {get; set;}
    public float Speed {get;}
    public bool Remove {get; set;}

    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
}