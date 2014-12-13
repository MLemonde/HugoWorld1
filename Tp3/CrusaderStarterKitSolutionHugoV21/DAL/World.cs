using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HugoWorldServiceRef;

namespace HugoWorld
{
    /// <summary>
    /// Auteurs: Mathew Lemonde, Francis Lussier, Marc-André Landry
    /// </summary>
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
        private double time = 0;
        private Timer _tmrRefresh = new Timer();
        private const string _startArea = "0,0";
        private int _heroid = 0;
        private string _heroClasse;
        private Dictionary<string, Area> _world = new Dictionary<string, Area>();
        private HugoWorldServiceRef.Hero _currentHero;
        private Area _currentArea;
        private Dictionary<string, Tile> _tiles;
        private Point _heroPosition;
        private Sprite _heroSprite;
        private bool _heroSpriteAnimating;
        private bool _heroSpriteFighting;
        private PointF _heroDestination;
        private HeroDirection _direction;
        private Monde _currentWorld;
        private GameState _gameState;
        private List<textPopup> _popups = new List<textPopup>();
        HeroControllerClient HeroClient = new HeroControllerClient();
        private static Font _font = new Font("Arial", 18);
        private static Brush _whiteBrush = new SolidBrush(Color.White);
        private static Brush _blackBrush = new SolidBrush(Color.Red);
        private static Random _random = new Random();

        public World(GameState gameState, Dictionary<string, Tile> tiles, int mondeid, bool dead)
        {
            _gameState = gameState;
            _tmrRefresh.Interval = 2000;
            _tmrRefresh.Tick += _tmrRefresh_Tick;
            _tiles = tiles;
            Classe currentclasse = Data.ClassController.GetListClasses(Data.WorldId).FirstOrDefault(c=> c.Id == Data.ClassId);
            _currentHero = Data.HeroController.GetListHero(Data.UserId).First(c => c.Id == Data.CurrentHeroId);
            _heroClasse = currentclasse.Description;
            //load la map
            CreerAreaDic(mondeid);
            //load les stat du hero
            UpdateGameState();

           

            //Find the start point
            _heroid = _currentHero.Id;
            //trouve l'area du hero
            int xarea = _currentHero.x / 8;
            int yarea = _currentHero.y / 8;

            _currentArea = _world[xarea.ToString() + "," + yarea.ToString()];


            
            _heroPosition = new Point(3, 3);

            //si mort, retourne debut full life
            if (dead)
            {
               _currentArea = _world["0,0"];

                Data.vie = Data.Stam * 10;
            }
            if (!dead)
            {
                //si pas mort, trouve pos
                _heroPosition.X = _currentHero.x % 8;
                _heroPosition.Y = _currentHero.y % 8;
            }


            //set sprite selon classe
            _heroSprite = new Sprite(null, _heroPosition.X * Tile.TileSizeX + Area.AreaOffsetX,
                                            _heroPosition.Y * Tile.TileSizeY + Area.AreaOffsetY,
                                            _tiles[_heroClasse].Bitmap, _tiles[_heroClasse].Rectangle, _tiles[_heroClasse].NumberOfFrames);

            
            //affiche nom
            _popups.Add(new textPopup((int)_heroSprite.Location.X, (int)_heroSprite.Location.Y +100, Data.HeroName));


            _heroSprite.Flip = true;
            _heroSprite.ColorKey = Color.FromArgb(75, 75, 75);
            _tmrRefresh.Enabled = true;

        }

        //useless
        void _tmrRefresh_Tick(object sender, EventArgs e)
        {
           

        }

        //Save pos et stat hero
        private void SaveState()
        {
            //boucle while pour concurrence
            while (true)
            {
                try
                {
                    //save pos
                    HeroClient.SetHeroPos(_heroid, _heroPosition.X, _heroPosition.Y, _currentArea.Name);
                    //save stat
                    HeroClient.EditHero(_heroid, Data.Lvl, Data.Dex, Data.Str, Data.Stam, Data.Intel, Data.Exp, Data.Argent,Data.vie);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
           
        }
        //load la map 
        private void CreerAreaDic(int mapid)
        {
            MondeControllerClient client = new MondeControllerClient();
            List<Monde> lstmonde = client.GetListMonde();
            _currentWorld = lstmonde.FirstOrDefault(c => c.Id == mapid);
            if (_currentWorld == null)
                return;
            int Mondex = int.Parse(_currentWorld.LimiteX) / 8;
            int Mondey = int.Parse(_currentWorld.LimiteY) / 8;

            for (int x = 0; x < Mondex; x++)
            {

                for (int y = 0; y < Mondey; y++)
                {
                    Area area = new Area(mapid, x, y, _tiles);
                    _world.Add(area.Name, area);
                }
            }
        }


        public override void Update(double gameTime, double elapsedTime)
        {
            time += elapsedTime;
            if(time > 2)
            {
                time = 0;
                SaveState();
                _currentArea.Refresh();
            }
            //We only actually update the current area the rest all 'sleep'
            _currentArea.Update(gameTime, elapsedTime*5);

            _heroSprite.Update(gameTime, elapsedTime *5);

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

            _heroSprite.CurrentFrame = 0;




        }

        //Regarde pour des item, ramasse et donne au hero si oui
        private void checkObjects()
        {
            string[] lststr = _currentArea.Name.Split(',');

            int herox = int.Parse(lststr[0]) * 8 + _heroPosition.X;
            int heroy = int.Parse(lststr[1]) * 8 + _heroPosition.Y;
            
            bool pickedup = false;
            Tile objectTile = _currentArea.Map[_heroPosition.X, _heroPosition.Y].ObjectTile;
            if (objectTile == null)
                return;
            switch (objectTile.Name)
            {
                //Most objects change your stats in some way.
                case "Armor":
                    if (Data.HeroController.PickupItem(Data.CurrentHeroId,herox,heroy))
                    {
                    Data.Str++;
                    UpdateGameState();
                    Sounds.Pickup();
                    pickedup = true;
                    break;
                        }
                    else
                    {
                        _currentArea.Refresh();
                        break;
                    }

                case "Dagger":
                    if (Data.HeroController.PickupItem(Data.CurrentHeroId,herox,heroy))
                    {
                        
                    Data.Dex++;
                    UpdateGameState();
                    Sounds.Pickup();
                    pickedup = true;
                    break;
                        }
                    else
                    {
                        _currentArea.Refresh();
                        break;
                    }

                case "Sword":
                    if (Data.HeroController.PickupItem(Data.CurrentHeroId,herox,heroy))
                    {
                        Data.Str++;
                        UpdateGameState();
                        Sounds.Pickup();
                        pickedup = true;
                        break;
                    }
                    else
                    {
                        _currentArea.Refresh();
                        break;
                    }

                case "SpikedMace":
                    if (Data.HeroController.PickupItem(Data.CurrentHeroId,herox,heroy))
                    {
                    Data.Str += 3;
                    UpdateGameState();
                    Sounds.Pickup();
                    pickedup = true;
                    break;
                            }
                    else
                    {
                        _currentArea.Refresh();
                        break;
                    }

                case "Axe":
                    if (Data.HeroController.PickupItem(Data.CurrentHeroId,herox,heroy))
                    {
                    Data.Str += 3;
                    UpdateGameState();
                    pickedup = true;
                    Sounds.Pickup();
                    break;
                                }
                    else
                    {
                        _currentArea.Refresh();
                        break;
                    }

                case "Heart":
                    if (Data.HeroController.PickupItem(Data.CurrentHeroId,herox,heroy))
                    {
                    Data.Stam += 3;
                    UpdateGameState();
                    pickedup = true;
                    Sounds.Eat();
                    break;
                    }
                    else
                    {
                        _currentArea.Refresh();
                        break;
                    }

                case "Food":
                    if (Data.HeroController.PickupItem(Data.CurrentHeroId,herox,heroy))
                    {
                    Data.Stam += 1;
                    pickedup = true;
                    UpdateGameState();
                    Sounds.Eat();
                    break;
                        }
                    else
                    {
                        _currentArea.Refresh();
                        break;
                    }

                case "GoldPile":
                    if (Data.HeroController.PickupItem(Data.CurrentHeroId,herox,heroy))
                    {
                    Data.Argent += 5;
                    pickedup = true;
                    UpdateGameState();
                    Sounds.Pickup();
                    break;
                        }
                    else
                    {
                        _currentArea.Refresh();
                        break;
                    }

                case "GoldBag":
                    if (Data.HeroController.PickupItem(Data.CurrentHeroId,herox,heroy))
                    {
                    Data.Argent += 15;
                    pickedup = true;
                    UpdateGameState();
                    Sounds.Pickup();
                    break;
                        }
                    else
                    {
                        _currentArea.Refresh();
                        break;
                    }

                case "Potion":
                    if (Data.HeroController.PickupItem(Data.CurrentHeroId,herox,heroy))
                    {
                    _gameState.Potions++;
                    pickedup = true;
                    UpdateGameState();
                    Sounds.Pickup();
                    break;
                        }
                    else
                    {
                        _currentArea.Refresh();
                        break;
                    }

                case "Key":
                    if (Data.HeroController.PickupItem(Data.CurrentHeroId,herox,heroy))
                    {
                    _gameState.HasRedKey = true;
                    pickedup = true;
                    UpdateGameState();
                    Sounds.Pickup();
                    break;
                    }
                    else
                    {
                        _currentArea.Refresh();
                        break;
                    }


            }
            //Remove the object unless its bones or fire
            if (pickedup)
            {
                _currentArea.Map[_heroPosition.X, _heroPosition.Y].ObjectTile = null;
                _currentArea.Map[_heroPosition.X, _heroPosition.Y].Sprite = null;
                _currentArea.Map[_heroPosition.X, _heroPosition.Y].ObjectSprite = null;

                _currentArea.Map[_heroPosition.X, _heroPosition.Y].Sprite = _currentArea.MapItem[_heroPosition.X, _heroPosition.Y].Sprite;
                //  _currentArea.Update(0,0);
            }
        }
        //update gamestate avec data
        private void UpdateGameState()
        {
            _gameState.Attack = Data.Str + Data.Dex;
            _gameState.Potions = 0;
            _gameState.Armour = Data.Str / 2 + Data.Dex / 2 + Data.Intel; ;
            _gameState.Experience = Data.Exp;
            _gameState.Level = Data.Lvl;
            _gameState.Health = Data.vie;
            _gameState.Treasure = Data.Argent;
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


        /// <summary>
        /// Gestion du déplacement du héro
        /// fais bouger le nom et save dans la bd lors deplacement
        /// </summary>
        /// <param name="key"></param>
        public void KeyDown(Keys key)
        {


           


            //Ignore keypresses while we are animating or fighting
            if (!_heroSpriteAnimating)
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
                                HeroClient.SetHeroPos(_heroid, _heroPosition.X, _heroPosition.Y, _currentArea.Name);

                                _popups.Clear();
                                _popups.Add(new textPopup((int)_heroSprite.Location.X + 100, (int)_heroSprite.Location.Y+100, Data.HeroName));
                                _heroSprite.Velocity = new PointF(100, 0);
                                _heroSprite.Flip = false ;
                                _heroSpriteAnimating = true;
                                _direction = HeroDirection.Right;
                                _heroPosition.X++;
                                HeroClient.SetHeroPos(_heroid, _heroPosition.X, _heroPosition.Y, _currentArea.Name);

                                setDestination();
                                

                            }
                        }
                        else if (_currentArea.EastArea != "-")
                        {
                            if (checkNextTile(_world[_currentArea.EastArea].Map[0, _heroPosition.Y], 0, _heroPosition.Y))
                            {
                                //Edge of map - move to next area
                                
                                _currentArea = _world[_currentArea.EastArea];
                                _heroPosition.X = 0;
                                setDestination();
                                _heroSprite.Location = _heroDestination;
                                _popups.Clear();
                                _popups.Add(new textPopup((int)_heroSprite.Location.X , (int)_heroSprite.Location.Y + 100, Data.HeroName));
                            }
                        }
                        break;

                    case Keys.Left:
                        //Are we at the edge of the map?
                        if (_heroPosition.X > 0)
                        {
                            //Can we move to the next tile or not (blocking tile or monster)
                            if (checkNextTile(_currentArea.Map[_heroPosition.X - 1, _heroPosition.Y], _heroPosition.X - 1, _heroPosition.Y))
                            {
                                _popups.Clear();
                                _popups.Add(new textPopup((int)_heroSprite.Location.X - 100, (int)_heroSprite.Location.Y+100, Data.HeroName));
                                _heroSprite.Velocity = new PointF(-100, 0);
                                _heroSprite.Flip = true;
                                _heroSpriteAnimating = true;
                                _direction = HeroDirection.Left;
                                _heroPosition.X--;
                                HeroClient.SetHeroPos(_heroid, _heroPosition.X, _heroPosition.Y, _currentArea.Name);

                                setDestination();
                                


                            }
                        }
                        else if (_currentArea.WestArea != "-")
                        {
                            if (checkNextTile(_world[_currentArea.WestArea].Map[7, _heroPosition.Y], 7, _heroPosition.Y))
                            {
                                _currentArea = _world[_currentArea.WestArea];
                                _heroPosition.X = Area.MapSizeX - 1;
                                setDestination();
                                _heroSprite.Location = _heroDestination;
                                _popups.Clear();
                                _popups.Add(new textPopup((int)_heroSprite.Location.X, (int)_heroSprite.Location.Y + 100, Data.HeroName));
                            }
                        }
                        break;

                    case Keys.Up:
                        //Are we at the edge of the map?
                        if (_heroPosition.Y > 0)
                        {
                            //Can we move to the next tile or not (blocking tile or monster)
                            if (checkNextTile(_currentArea.Map[_heroPosition.X, _heroPosition.Y - 1], _heroPosition.X, _heroPosition.Y - 1))
                            {
                                _popups.Clear();
                                _popups.Add(new textPopup((int)_heroSprite.Location.X, (int)_heroSprite.Location.Y, Data.HeroName));
                                _heroSprite.Velocity = new PointF(0, -100);
                                _heroSpriteAnimating = true;
                                _direction = HeroDirection.Up;
                                _heroPosition.Y--;
                                HeroClient.SetHeroPos(_heroid, _heroPosition.X, _heroPosition.Y, _currentArea.Name);

                                setDestination();
                                
                                

                            }
                        }
                        else if (_currentArea.NorthArea != "-")
                        {
                            if (checkNextTile(_world[_currentArea.NorthArea].Map[_heroPosition.X, 7], _heroPosition.X, 7))
                            {
                                //Edge of map - move to next area
                                _currentArea = _world[_currentArea.NorthArea];
                                _heroPosition.Y = Area.MapSizeY - 1;
                                setDestination();
                                _heroSprite.Location = _heroDestination;
                                _popups.Clear();
                                _popups.Add(new textPopup((int)_heroSprite.Location.X, (int)_heroSprite.Location.Y + 100, Data.HeroName));

                            }
                        }
                        break;

                    case Keys.Down:
                        //Are we at the edge of the map?
                        if (_heroPosition.Y < Area.MapSizeY - 1)
                        {

                            //Can we move to the next tile or not (blocking tile or monster)
                            if (checkNextTile(_currentArea.Map[_heroPosition.X, _heroPosition.Y + 1], _heroPosition.X, _heroPosition.Y + 1))
                            {
                                _popups.Clear();
                                _popups.Add(new textPopup((int)_heroSprite.Location.X, (int)_heroSprite.Location.Y +200, Data.HeroName));
                                _heroSprite.Velocity = new PointF(0, 100);
                                _heroSpriteAnimating = true;
                                _direction = HeroDirection.Down;
                                _heroPosition.Y++;
                                HeroClient.SetHeroPos(_heroid, _heroPosition.X, _heroPosition.Y, _currentArea.Name);

                                setDestination();
                                


                            }
                        }
                        else if (_currentArea.SouthArea != "-")
                        {
                            if (checkNextTile(_world[_currentArea.SouthArea].Map[_heroPosition.X, 0], _heroPosition.X, 0))
                            {
                                //Edge of map - move to next area
                                _currentArea = _world[_currentArea.SouthArea];
                                _heroPosition.Y = 0;
                                setDestination();
                                _heroSprite.Location = _heroDestination;
                                _popups.Clear();
                                _popups.Add(new textPopup((int)_heroSprite.Location.X, (int)_heroSprite.Location.Y + 100, Data.HeroName));
                            }
                        }
                        break;

                    case Keys.P:
                        //Potion - if we have any
                        if (_gameState.Potions > 0)
                        {
                            Sounds.Magic();

                            _gameState.Potions--;

                            _heroSpriteFighting = true;


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
        /// Vérifie la nouvelle position héro
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
                if (mapTile.ObjectTile.Name == "Princess")
                {
                    //Game is won
                    Sounds.Kiss();
                    _gameState.GameIsWon = true;
                    return false; //Don't want to walk on her
                }

                Sounds.Fight();

                _heroSpriteFighting = true;


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
                        //_heroSprite = new Sprite(null, _heroPosition.X * Tile.TileSizeX + Area.AreaOffsetX,
                        //        _heroPosition.Y * Tile.TileSizeY + Area.AreaOffsetY,
                        //        _tiles["bon"].Bitmap, _tiles["bon"].Rectangle, _tiles["bon"].NumberOfFrames);
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
                        try
                        {
                            Data.MonstreController.KillMonster(Data.WorldId, x, y);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
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


                mapTile.ObjectTile = _currentArea.MapItem[x, y].Tile;
                //mapTile.Tile = null;
                mapTile.SetObjectSprite(x, y);
                returnValue = true; //monster is dead
            }

            _popups.Add(new textPopup((int)mapTile.Sprite.Location.X + 40, (int)mapTile.Sprite.Location.Y + 20, (damage != 0) ? damage.ToString() : "miss"));

            return returnValue;
        }

        private void checkDoors(MapTile mapTile, int x, int y)
        {
            //If the next tile is a closed door then check if we have the key
            if (mapTile.Tile.Name == "DoorClosed" && mapTile.Tile.IsBlock)
            {
                //For each key if it matches then open the door by switching the sprite & sprite to its matching open version
                if (_gameState.HasRedKey)
                {
                    //Open the door
                    mapTile.Tile = _tiles["DoorOpen"];
                    mapTile.SetSprite(x, y);
                }
            }
        }

        private void setDestination()
        {
            //Calculate the eventual sprite destination based on the area grid coordinates
            _heroDestination = new PointF(_heroPosition.X * Tile.TileSizeX + Area.AreaOffsetX, _heroPosition.Y * Tile.TileSizeY + Area.AreaOffsetY);
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
