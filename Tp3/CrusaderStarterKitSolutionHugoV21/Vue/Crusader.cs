using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using HugoWorldServiceRef;
using Vue;
namespace HugoWorld
{
    public partial class HugoWorld : Form
    {
        private Stopwatch _timer = new Stopwatch();
        private double _lastTime;
        private long _frameCounter;
        private GameState _gameState;

        public HugoWorld()
        {
            //Setup the form
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            //Startup the game state
            _gameState = new GameState(ClientSize);
            
            initialize();
        }

        private void initialize()
        {
            _gameState.Initialize(false);

            //Initialise and start the timer
            _lastTime = 0.0;
            _timer.Reset();
            _timer.Start();
        }

        private void Crusader_Paint(object sender, PaintEventArgs e)
        {
            //Work out how long since we were last here in seconds
            double gameTime = _timer.ElapsedMilliseconds / 1000.0;
            double elapsedTime = gameTime - _lastTime;
            _lastTime = gameTime;
            _frameCounter++;

            //Perform any animation and updates
            _gameState.Update(gameTime, elapsedTime);

            //Draw everything
            _gameState.Draw(e.Graphics);

            //Force the next Paint()
            this.Invalidate();
        }

        private void Crusader_KeyDown(object sender, KeyEventArgs e)
        {
            _gameState.KeyDown(e.KeyCode);
        }

        private void Crusader_Shown(object sender, EventArgs e)
        {
            Form help = new helpform();
            help.Show();
            help.Focus();
        }

        private void HugoWorld_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Data.HeroController.DeconnectHero(Data.CurrentHeroId);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }
    }
}