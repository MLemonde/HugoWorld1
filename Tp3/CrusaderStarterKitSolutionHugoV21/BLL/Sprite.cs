using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;


namespace HugoWorld
{
    public class Sprite : GameObject
    {

        public PointF Velocity;
        public PointF Acceleration;
        public PointF Location;
        public SizeF Size;
        public bool Flip;
        public int CurrentFrame;
        
        private Color _colorKey;
        private ImageAttributes _attributes;

        protected List<Bitmap> _frames = new List<Bitmap>();
        private List<Rectangle> _rectangle = new List<Rectangle>();
        protected GameState _gameState;

        public Color ColorKey
        {
            get { return _colorKey; }
            set 
            {
                _colorKey = value;
                //Set the color key for this sprite;
                _attributes= new ImageAttributes();
                _attributes.SetColorKey(_colorKey, _colorKey);
            }
        }

        public int NumberOfFrames
        {
            get { return _frames.Count; }
        }
	
        

        private Sprite()
        {
            //No default constructor
        }

        public Sprite(GameState gameState, float x, float y, string filename)
        {
            //Load the bitmap
            _frames.Add(new Bitmap(filename));

            //Set the location and use the height and width from the 1st frame
            initialize(gameState, x, y, _frames[0].Width, _frames[0].Height);
        }

        public Sprite(GameState gameState, float x, float y, string filename1, string filename2)
        {
            //Load the 2 animation frames
            _frames.Add(new Bitmap(filename1));
            _frames.Add(new Bitmap(filename2));

            //Set the location and use the height and width from the 1st frame
            initialize(gameState, x, y, _frames[0].Width, _frames[0].Height);
        }



        public Sprite(GameState gameState, float x, float y, Bitmap bitmap, Rectangle rectangle, int numberAnimationFrames)
        {
            for (int i = 0; i < numberAnimationFrames; i++)
            {
                _frames.Add(bitmap);

                //Set the location and use the height and width from the 1st frame
                initialize(gameState, x, y, rectangle.Width / numberAnimationFrames, rectangle.Height);

                _rectangle.Add(new Rectangle(rectangle.X + i * rectangle.Width / numberAnimationFrames, rectangle.Y, 
                                              rectangle.Width / numberAnimationFrames, rectangle.Height));
            }
        }

        private void initialize(GameState gameState, float x, float y, float width, float height)
        {
            _gameState = gameState;
            Location.X = x;
            Location.Y = y;
            Size.Width = width;
            Size.Height = height;
            CurrentFrame = 0;
        }

        public override void Update(double gameTime, double elapsedTime)
        {
            //Move the sprite
            Location.X += Velocity.X * (float)elapsedTime;
            Location.Y += Velocity.Y * (float)elapsedTime;

            //Add in any acceleration
            Velocity.X += Math.Sign(Velocity.X) * Acceleration.X * (float)elapsedTime;
            Velocity.Y += Math.Sign(Velocity.Y) * Acceleration.Y * (float)elapsedTime;
        }

        public override void Draw(Graphics graphics)
        {
            //Draw the correct frame at the current point
            if (_rectangle[CurrentFrame] == Rectangle.Empty)
            {
                graphics.DrawImage(_frames[CurrentFrame], Location.X, Location.Y, Size.Width, Size.Height);
            }
            else
            {
                Rectangle outputRect = Rectangle.Empty;
                if (Flip)
                {
                    outputRect = new Rectangle((int)Location.X+(int)Size.Width, (int)Location.Y, -(int)Size.Width, (int)Size.Height);
                }
                else
                {
                    outputRect = new Rectangle((int)Location.X, (int)Location.Y, (int)Size.Width, (int)Size.Height);
                }

                if (_attributes == null)
                {
                    graphics.DrawImage(_frames[CurrentFrame], outputRect,
                                                                _rectangle[CurrentFrame].X, _rectangle[CurrentFrame].Y, 
                                                                _rectangle[CurrentFrame].Width, _rectangle[CurrentFrame].Height, 
                                                                GraphicsUnit.Pixel);
                }
                else
                {
                    graphics.DrawImage(_frames[CurrentFrame], outputRect,
                                                                _rectangle[CurrentFrame].X, _rectangle[CurrentFrame].Y,
                                                                _rectangle[CurrentFrame].Width, _rectangle[CurrentFrame].Height, 
                                                                GraphicsUnit.Pixel, _attributes);
                }
            }
        }

        public static bool Collision(Sprite sprite1, Sprite sprite2)
        {
            //See if the sprite rectangles overlap
            return ! ( sprite1.Location.X > sprite2.Location.X + sprite2.Size.Width
		            || sprite1.Location.X + sprite1.Size.Width < sprite2.Location.X
		            || sprite1.Location.Y > sprite2.Location.Y + sprite2.Size.Height
		            || sprite1.Location.Y + sprite1.Size.Height < sprite2.Location.Y);
        }

    }
}
