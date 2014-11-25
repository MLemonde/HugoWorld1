using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HugoWorld
{
    //Textpop up used to display damage when monsters and players are hit
    struct textPopup
    {
        public int X;
        public int Y;
        public string Text;

        public textPopup(int x, int y, string text)
        {
            X = x;
            Y = y;
            Text = text;
        }
    }

    public class World : GameObject
    {
        private const string _startArea = "start";

        private Dictionary<string, Area> _world = new Dictionary<string, Area>();
        private Area _currentArea;
        private Dictionary<string, Tile> _tiles;
        private Point _heroPosition;
        private Sprite _heroSprite;
        private bool _heroSpriteAnimating;
        private bool _heroSpriteFighting;
        private double _startFightTime = -1.0;
        private PointF _heroDestination;
        private HeroDirection _direction;
        private GameState _gameState;
        private List<textPopup> _popups = new List<textPopup>();

        private static Font _font = new Font("Arial", 18);
        private static Brush _whiteBrush = new SolidBrush(Color.White);
        private static Brush _blackBrush = new SolidBrush(Color.Red);
        private static Random _random = new Random();

        public World(GameState gameState, Dictionary<string, Tile> tiles, string mapFile)
        {
            _gameState = gameState;
            _tiles = tiles;

            //Read in the map file
            readMapfile(mapFile);

            //Find the start point
            _currentArea = _world[_startArea];

            //Create and position the hero character
            _heroPosition = new Point(3, 3);
            _heroSprite = new Sprite(null, _heroPosition.X * Tile.TileSizeX + Area.AreaOffsetX,
                                            _heroPosition.Y * Tile.TileSizeY + Area.AreaOffsetY,
                                            _tiles["her"].Bitmap, _tiles["her"].Rectangle, _tiles["her"].NumberOfFrames);
            _heroSprite.Flip = true;
            _heroSprite.ColorKey = Color.FromArgb(75, 75, 75);
        }

        private void readMapfile(string mapFile)
        {
            using (StreamReader stream = new StreamReader(mapFile))
            {
                while (!stream.EndOfStream)
                {
                    //Each area constructor will consume just one area
                    Area area = new Area(stream, _tiles);
                    _world.Add(area.Name, area);
                }
            }
        }


        public override void Update(double gameTime, double elapsedTime)
        {
            //We only actually update the current area the rest all 'sleep'
            _currentArea.Update(gameTime, elapsedTime);

            _heroSprite.Update(gameTime, elapsedTime);

            //If the hero is moving we need to check if we are there yet
            if (_heroSpriteAnimating)
            {
                if (checkDestination())
                {
                    //We have arrived. Stop moving and animating
                    _heroSprite.Location = _heroDestination;
                    _heroSprite.Velocity = PointF.Empty;
                    _heroSpriteAnimating = false;

                    //Check if there is anything on this square
                    checkObjects();
                }
            }

            //The hero gets animated when moving or fighting
            if (_heroSpriteAnimating || _heroSpriteFighting)
                _heroSprite.CurrentFrame = (int)((gameTime * 8.0) % _heroSprite.NumberOfFrames);
            else
            {
                //Otherwise use frame 0
                _heroSprite.CurrentFrame = 0;
            }

            //If we are fighting then keep animating for a period of time
            if (_heroSpriteFighting)
            {
                if (_startFightTime < 0)
                    _startFightTime = gameTime;
                else
                {
                    if (gameTime - _startFightTime > 1.0)
                        _heroSpriteFighting = false;
                }
            }
        }

        private void checkObjects()
        {
            Tile objectTile = _currentArea.Map[_heroPosition.X, _heroPosition.Y].ObjectTile;
            if (objectTile == null) 
                return;
            switch (objectTile.Category)
            {
                //Most objects change your stats in some way.
                case "armour":
                    _gameState.Armour++;
                    Sounds.Pickup();
                    break;

                case "attack":
                    _gameState.Attack++;
                    Sounds.Pickup();
                    break;

                case "food":
                    _gameState.Health += 10;
                    Sounds.Eat();
                    break;

                case "treasure":
                    _gameState.Treasure += 5;
                    Sounds.Pickup();
                    break;

                case "potion":
                    _gameState.Potions++;
                    Sounds.Pickup();
                    break;

                case "key":
                    if (objectTile.Color == "brown") _gameState.HasBrownKey = true;
                    if (objectTile.Color == "green") _gameState.HasGreenKey = true;
                    if (objectTile.Color == "red") _gameState.HasRedKey = true;
                    Sounds.Pickup();
                    break;

                case "fire":
                    _gameState.Health -= 2;
                    break;
            }
            //Remove the object unless its bones or fire
            if (objectTile.Category != "fire" && objectTile.Category != "bones" && objectTile.Category != "character")
            {
                _currentArea.Map[_heroPosition.X, _heroPosition.Y].ObjectTile = null;
                _currentArea.Map[_heroPosition.X, _heroPosition.Y].ObjectSprite = null;
            }
        }

        private bool checkDestination()
        {
            //Depending on the direction we are moving we check different bounds of the destination
            switch (_direction)
            {
                case HeroDirection.Right:
                    return (_heroSprite.Location.X >= _heroDestination.X);
                case HeroDirection.Left:
                    return (_heroSprite.Location.X <= _heroDestination.X);
                case HeroDirection.Up:
                    return (_heroSprite.Location.Y <= _heroDestination.Y);
                case HeroDirection.Down:
                    return (_heroSprite.Location.Y >= _heroDestination.Y);
            }

            throw new ArgumentException("Direction is not set correctly");
        }

        public override void Draw(Graphics graphics)
        {
            _currentArea.Draw(graphics);
            _heroSprite.Draw(graphics);

            //If we are fighting then draw the damage
            if (_heroSpriteFighting)
            {
                foreach (textPopup popup in _popups)
                {
                    //Draw 4 text offsets to get an outline
                    graphics.DrawString(popup.Text, _font, _whiteBrush, popup.X + 2, popup.Y);
                    graphics.DrawString(popup.Text, _font, _whiteBrush, popup.X - 1, popup.Y);
                    graphics.DrawString(popup.Text, _font, _whiteBrush, popup.X, popup.Y + 2);
                    graphics.DrawString(popup.Text, _font, _whiteBrush, popup.X, popup.Y - 2);

                    //Draw the actual text
                    graphics.DrawString(popup.Text, _font, _blackBrush, popup.X, popup.Y);
                }
            }

        }

        /// <summary>
        /// Gestion du d�placement du h�ro
        /// </summary>
        /// <param name="key"></param>
        public void KeyDown(Keys key)
        {
            //Ignore keypresses while we are animating or fighting
            if (!_heroSpriteAnimating && !_heroSpriteFighting)
            {
                switch (key)
                {
                    case Keys.Right:
                        //Are we at the edge of the map?
                        if (_heroPosition.X < Area.MapSizeX - 1)
                        {
                            //Can we move to the next tile or not (blocking tile or monster)
                            if (checkNextTile(_currentArea.Map[_heroPosition.X + 1, _heroPosition.Y], _heroPosition.X + 1, _heroPosition.Y))
                            {
                                _heroSprite.Velocity = new PointF(100, 0);
                                _heroSprite.Flip = true;
                                _heroSpriteAnimating = true;
                                _direction = HeroDirection.Right;
                                _heroPosition.X++;
                                setDestination();
                            }
                        }
                        else if (_currentArea.EastArea != "-")
                        {
                            //Edge of map - move to next area
                            _currentArea = _world[_currentArea.EastArea];
                            _heroPosition.X = 0;
                            setDestination();
                            _heroSprite.Location = _heroDestination;
                        }
                        break;

                    case Keys.Left:
                        //Are we at the edge of the map?
                        if (_heroPosition.X > 0)
                        {
                            //Can we move to the next tile or not (blocking tile or monster)
                            if (checkNextTile(_currentArea.Map[_heroPosition.X - 1, _heroPosition.Y], _heroPosition.X - 1, _heroPosition.Y))
                            {
                                _heroSprite.Velocity = new PointF(-100, 0);
                                _heroSprite.Flip = false;
                                _heroSpriteAnimating = true;
                                _direction = HeroDirection.Left;
                                _heroPosition.X--;
                                setDestination();
                            }
                        }
                        else if (_currentArea.WestArea != "-")
                        {
                            _currentArea = _world[_currentArea.WestArea];
                            _heroPosition.X = Area.MapSizeX - 1;
                            setDestination();
                            _heroSprite.Location = _heroDestination;
                        }
                        break;

                    case Keys.Up:
                        //Are we at the edge of the map?
                        if (_heroPosition.Y > 0)
                        {
                            //Can we move to the next tile or not (blocking tile or monster)
                            if (checkNextTile(_currentArea.Map[_heroPosition.X, _heroPosition.Y - 1], _heroPosition.X, _heroPosition.Y - 1))
                            {
                                _heroSprite.Velocity = new PointF(0, -100);
                                _heroSpriteAnimating = true;
                                _direction = HeroDirection.Up;
                                _heroPosition.Y--;
                                setDestination();
                            }
                        }
                        else if (_currentArea.NorthArea != "-")
                        {
                            //Edge of map - move to next area
                            _currentArea = _world[_currentArea.NorthArea];
                            _heroPosition.Y = Area.MapSizeY - 1;
                            setDestination();
                            _heroSprite.Location = _heroDestination;
                        }
                        break;

                    case Keys.Down:
                        //Are we at the edge of the map?
                        if (_heroPosition.Y < Area.MapSizeY - 1)
                        {
                            //Can we move to the next tile or not (blocking tile or monster)
                            if (checkNextTile(_currentArea.Map[_heroPosition.X, _heroPosition.Y + 1], _heroPosition.X, _heroPosition.Y + 1))
                            {
                                _heroSprite.Velocity = new PointF(0, 100);
                                _heroSpriteAnimating = true;
                                _direction = HeroDirection.Down;
                                _heroPosition.Y++;
                                setDestination();
                            }
                        }
                        else if (_currentArea.SouthArea != "-")
                        {
                            //Edge of map - move to next area
                            _currentArea = _world[_currentArea.SouthArea];
                            _heroPosition.Y = 0;
                            setDestination();
                            _heroSprite.Location = _heroDestination;
                        }
                        break;

                    case Keys.P:
                        //Potion - if we have any
                        if (_gameState.Potions > 0)
                        {
                            Sounds.Magic();

                            _gameState.Potions--;

                            _heroSpriteFighting = true;
                            _startFightTime = -1;

                            //All monsters on the screen take maximum damage
                            _popups.Clear();
                            for (int i = 0; i < Area.MapSizeX; i++)
                            {
                                for (int j = 0; j < Area.MapSizeY; j++)
                                {
                                    MapTile mapTile = _currentArea.Map[i, j];
                                    if (mapTile.ObjectTile != null && mapTile.ObjectTile.Category == "character")
                                    {
                                        damageMonster(_gameState.Attack * 2, mapTile, i, j);
                                    }
                                }
                            }
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// V�rifie la nouvelle position h�ro
        /// </summary>
        /// <param name="mapTile"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool checkNextTile(MapTile mapTile, int x, int y)
        {
            //See if there is a door we need to open
            checkDoors(mapTile, x, y);

            //See if there is character to fight
            if (mapTile.ObjectTile != null && mapTile.ObjectTile.Category == "character")
            {
                if (mapTile.ObjectTile.Shortcut == "pri")
                {
                    //Game is won
                    Sounds.Kiss();
                    _gameState.GameIsWon = true;
                    return false; //Don't want to walk on her
                }

                Sounds.Fight();

                _heroSpriteFighting = true;
                _startFightTime = -1;

                int heroDamage = 0;
                //A monsters attack ability is 1/2 their max health. Compare that to your armour
                //If you outclass them then there is still a chance of a lucky hit
                if (_random.Next((mapTile.ObjectTile.Health / 2) + 1) >= _gameState.Armour
                    || (mapTile.ObjectTile.Health / 2 < _gameState.Armour && _random.Next(10) == 0))
                {
                    //Monsters do damage up to their max health - if they hit you.
                    heroDamage = _random.Next(mapTile.ObjectTile.Health) + 1;
                    _gameState.Health -= heroDamage;

                    if (_gameState.Health <= 0)
                    {
                        _gameState.Health = 0;
                        _heroSprite = new Sprite(null, _heroPosition.X * Tile.TileSizeX + Area.AreaOffsetX,
                                _heroPosition.Y * Tile.TileSizeY + Area.AreaOffsetY,
                                _tiles["bon"].Bitmap, _tiles["bon"].Rectangle, _tiles["bon"].NumberOfFrames);
                        _heroSprite.ColorKey = Color.FromArgb(75, 75, 75);
                    }

                }
                //Hero
                _popups.Clear();
                _popups.Add(new textPopup((int)_heroSprite.Location.X + 40, (int)_heroSprite.Location.Y + 20, (heroDamage != 0) ? heroDamage.ToString() : "miss"));

                //A monsters armour is 1/5 of their max health
                if (_random.Next(_gameState.Attack + 1) >= (mapTile.ObjectTile.Health / 5))
                {
                    //Hero damage is up to twice the attack rating
                    if (damageMonster(_random.Next(_gameState.Attack * 2) + 1, mapTile, x, y))
                    {
                        //Monster is dead now
                        return true;
                    }
                }
                else
                {
                    _popups.Add(new textPopup((int)mapTile.Sprite.Location.X + 40, (int)mapTile.Sprite.Location.Y + 20, "miss"));
                }
                //Monster not dead
                return false;
            }

            //If the next tile is a blocker then we can't move
            if (mapTile.Tile.IsBlock) return false;


            return true;
        }

        private bool damageMonster(int damage, MapTile mapTile, int x, int y)
        {
            //Do some damage, and remove the monster if its dead
            bool returnValue = false; //monster not dead

            //Set the monster health if its not already set
            if (mapTile.ObjectHealth == 0)
            {
                mapTile.ObjectHealth = mapTile.ObjectTile.Health;
            }


            mapTile.ObjectHealth -= damage;

            if (mapTile.ObjectHealth <= 0)
            {
                mapTile.ObjectHealth = 0;
                //Experience is the monsters max health
                _gameState.Experience += mapTile.ObjectTile.Health;

                //Remove the monster and replace with bones
                mapTile.ObjectTile = _tiles["bon"];
                mapTile.SetObjectSprite(x, y);
                returnValue = true; //monster is dead
            }

            _popups.Add(new textPopup((int)mapTile.Sprite.Location.X + 40, (int)mapTile.Sprite.Location.Y + 20, (damage != 0) ? damage.ToString() : "miss"));

            return returnValue;


        }

        private void checkDoors(MapTile mapTile, int x, int y)
        {
            //If the next tile is a closed door then check if we have the key
            if (mapTile.Tile.Category == "door" && mapTile.Tile.IsBlock)
            {
                //For each key if it matches then open the door by switching the sprite & sprite to its matching open version
                if (mapTile.Tile.Color == "brown" && _gameState.HasBrownKey)
                {
                    //Open the door
                    mapTile.Tile = _tiles["E"];
                    mapTile.SetSprite(x, y);
                }

                if (mapTile.Tile.Color == "red" && _gameState.HasRedKey)
                {
                    //Open the door
                    mapTile.Tile = _tiles["I"];
                    mapTile.SetSprite(x, y);
                }

                if (mapTile.Tile.Color == "green" && _gameState.HasGreenKey)
                {
                    //Open the door
                    mapTile.Tile = _tiles["G"];
                    mapTile.SetSprite(x, y);
                }
            }
        }

        private void setDestination()
        {
            //Calculate the eventual sprite destination based on the area grid coordinates
            _heroDestination = new PointF(_heroPosition.X * Tile.TileSizeX + Area.AreaOffsetX,
                                            _heroPosition.Y * Tile.TileSizeY + Area.AreaOffsetY);
        }

        private enum HeroDirection
        {
            Left,
            Right,
            Up,
            Down
        }
    }
}
