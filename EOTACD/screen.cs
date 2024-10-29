using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Penumbra;

namespace EOTACD
{
    public class screen : Game
    {
       

        

        protected EventHandler ScreenEvent; public screen(EventHandler theScreenEvent)
        {

            ScreenEvent = theScreenEvent;
        }

        public virtual void Initialize()
        {

        }
      
        public virtual void Update(GameTime theTime)
        {

        }
        public virtual void Draw(SpriteBatch theBatch,GameTime gameTime)
        {

        }



    }
}
