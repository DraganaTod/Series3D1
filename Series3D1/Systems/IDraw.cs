using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series3D1.Systems
{
    public interface IDraw : ISystem
    {
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
