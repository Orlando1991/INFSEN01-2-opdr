using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Security.Cryptography.X509Certificates;

namespace GUIapp
{
    public interface Option<T>
    {
        U Visit<U>(Func<U> onNone, Func<T, U> onSome);
        void Visit(Action onNone, Action<T> onSome);
    }
    public class None<T> : Option<T>
    {
        public U Visit<U>(Func<U> onNone, Func<T, U> onSome)
        {
            return onNone();
        }
        public void Visit(Action onNone, Action<T> onSome)
        {
            onNone();
        }
    }
    public class Some<T> : Option<T>
    {
        T value;
        public Some(T value)
        {
            this.value = value;
        }
        public U Visit<U>(Func<U> onNone, Func<T, U> onSome)
        {
            return onSome(value);
        }
        public void Visit(Action onNone, Action<T> onSome)
        {
            onSome(value);
        }
    }

    public class Point
    {
        public Point(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
        public float X { get; set; }
        public float Y { get; set; }
    }






    // drawable and updateable used in gui.cs
    public interface Updateable
    {
        //MISSING CODE HERE
        void Update(UpdateVisitor visitor, float dt);
    }
    public interface Drawable { void Draw(DrawVisitor visitor); }


    public interface DrawVisitor
    {
        void DrawButton(Button element);
        void DrawLabel(Label element);
        void DrawGui(GuiManager element);
    }



    public class DefaultDrawVisitor : DrawVisitor
    {
        SpriteBatch sprite_batch;
        ContentManager content_manager;
        Texture2D white_pixel;
        SpriteFont default_font;

        public DefaultDrawVisitor(SpriteBatch sprite_batch, ContentManager content_manager)
        {
            this.sprite_batch = sprite_batch;
            this.content_manager = content_manager;
            white_pixel = content_manager.Load<Texture2D>("white_pixel");
            default_font = content_manager.Load<SpriteFont>("arial");
        }
        public void DrawButton(Button element)
        {
            //MISSING CODE HERE
            sprite_batch.Draw(white_pixel, new Rectangle((int)element.top_left_corner.X, (int)element.top_left_corner.Y, (int)element.width, (int)element.height), element.color);
            element.label.Draw(this);
        }
        public void DrawLabel(Label element)
        {
            sprite_batch.DrawString(default_font, element.content, new Vector2(element.top_left_corner.X, element.top_left_corner.Y), element.color);
        }
        public void DrawGui(GuiManager gui_manager)
        {

            //MISSING CODE HERE
            foreach (var item in gui_manager.elements)
            {
                item.Draw(this);
            }
        }
    }


    public interface UpdateVisitor
    {
        void UpdateButton(Button element, float dt);
        void UpdateLabel(Label element, float ft);
        void UpdateGui(GuiManager gui_manager, float dt);
        //MISSING CODE HERE
    }


    public class DefaultUpdateVisitor : UpdateVisitor
    {
        public void UpdateButton(Button element, float dt)
        {
            var mouse = Mouse.GetState();
            //MISSING CODE HERE
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                if (element.is_intersecting(new Point(mouse.X, mouse.Y))) { element.color = Color.Blue; element.action(); }
            }
            else
            {
                element.color = Color.White;
            }
        }
        public void UpdateLabel(Label element, float dt)
        {

        }
        public void UpdateGui(GuiManager gui_manager, float dt)
        {
            //MISSING CODE HERE
            foreach (var item in gui_manager.elements)
            {
                item.Update(this, dt);
            }

        }
    }


}
