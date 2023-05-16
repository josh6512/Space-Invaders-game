using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Popups;



namespace App_spice
{
    class GameManger
    {
        private Canvas _myCanvas;
        private Player _player;
        private Shot _playerShot;
        private EnemyGhost[,] _enemies;
        private Shot[,] _enemyShots;
        private  int _level;
        private Random _random;

        public GameManger(Canvas canvas)
        {
            _myCanvas = canvas;

            _random = new Random();
            Image playerImage = new Image();
            Canvas.SetLeft(playerImage, _myCanvas.Width / 2);
            Canvas.SetTop(playerImage, 600);
            playerImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Archer.png"));
            _player = new Player(_myCanvas.Width / 2, 600, playerImage, 100, 100, new Random(), _myCanvas);
            MyCanvas.Children.Add(_player.MyImage);
            _level = 1;
            
            //_enemies = new EnemyGhost[4, 10];
            CreateEnemies();
        }

        public Canvas MyCanvas
        {
            get { return _myCanvas; }
            set { _myCanvas = value; }
        }

        public void MoveEnemiesAndShoot(Object sender, Object e)
        {
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 0; j < _level; j++)
                {
                    if (!_enemies[i - 1, j].IsDead)
                    {
                        _enemies[i-1, j].Move(_enemies[i - 1, j].Speed, 0);
                        if (!_enemies[i-1,j].IsArrow && _random.Next(0,1000) <= 18)
                        {
                            _enemies[i - 1, j].IsArrow = true;
                            _enemyShots[i- 1,j] = _enemies[i - 1, j].Shoot();
                        }
                    }
                }
            }
        }


        public void ReadKeyboard(object sender, object e)
        {

            if (Window.Current.CoreWindow.GetKeyState(VirtualKey.Left).HasFlag(CoreVirtualKeyStates.Down))
                _player.Move(_player.Speed * -1, 0);
            if (Window.Current.CoreWindow.GetKeyState(VirtualKey.Right).HasFlag(CoreVirtualKeyStates.Down))
                _player.Move(_player.Speed, 0);
            if (Window.Current.CoreWindow.GetKeyState(VirtualKey.Space).HasFlag(CoreVirtualKeyStates.Down) && !_player.IsArrow)
                _playerShot = _player.Shoot();
        }


        public void ShotCollision(Object sender, Object e)
        {
            // Player Shot
            if (_playerShot != null && !_playerShot.IsDead)
            {
                for (int i = 0; i < _enemies.GetLength(0); i++)
                {
                    for (int j = 0; j < _enemies.GetLength(1); j++)
                    {
                        if (!_enemies[i, j].IsDead && _playerShot.Collision(_enemies[i, j]))
                        {
                            _enemies[i, j].IsDead = true;
                            MyCanvas.Children.Remove(_enemies[i, j].MyImage);
                        }
                    }
                }
            }
            else
            {
                _player.IsArrow = false;
                if (_playerShot != null)
                    MyCanvas.Children.Remove(_playerShot.MyImage);
            }
                

            // Enemy Shots
            for (int i = 0; i < _enemyShots.GetLength(0); i++)
            {
                for (int j  = 0; j < _level; j++)
                if (_enemyShots[i,j] != null)
                {
                    if (!_enemyShots[i,j].IsDead)
                    {
                        if (_enemyShots[i,j].Collision(_player) && _player.IsDead == false)
                            {
                                MyCanvas.Children.Remove(_player.MyImage);
                                _player.IsDead = true;
                                GameOverMessage();
                            }
                            

                    }
                    else
                    {
                        MyCanvas.Children.Remove(_enemyShots[i,j].MyImage);
                        _enemyShots[i, j].IsDead = true;
                        _enemies[i,j].IsArrow = false;
                    }
                        
                }
            }
        }


        public void StartNewLevel(object sender, object e)
        {
            if(_level <= 10)
            {
                if (CheckAllDead())
                {
                    _level++;
                    CreateEnemies();
                }
            }
            else
            {
                GameWonMessage();
            }
        }


        public bool CheckAllDead()
        {
            bool noEnemies = true;
            bool noBombs = true;

            for (int i = 0; i < _enemies.GetLength(0); i++)
            {
                for (int j = 0; j < _enemies.GetLength(1); j++)
                {
                    if (!_enemies[i, j].IsDead)
                        noEnemies = false;
                    if (!_enemies[i, j].IsDead)
                        noBombs = false;
                }
            }
            if (noEnemies && noBombs)
                return true;
            return false;
        }

        public void CreateEnemies()
        {

            _enemyShots = new Shot[4, _level];

            _enemies = new EnemyGhost[4, _level];

            for (int i = 1; i <= 4; i++)
            {
                for (int j = 0; j < _level; j++)
                {
                    Image eImage = new Image();
                    eImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/goast1.png"));
                    EnemyGhost enemyGhost = new EnemyGhost(_random.Next(0, 1150), i * 50, eImage, 48, 48, _random, _myCanvas);
                    _enemies[i - 1, j] = enemyGhost;
                    _myCanvas.Children.Add(enemyGhost.MyImage);

                }
            }
        }

        public async void GameOverMessage()
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "You lost!",
                Content = "Better luck next time.",
                CloseButtonText = "Exit Game"
            };

            ContentDialogResult result = await dialog.ShowAsync();

            if (!(result == ContentDialogResult.Primary))
                App.Current.Exit();

        }

        public async void GameWonMessage()
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "You won the game!",
                Content = "Good job!",
                CloseButtonText = "Exit Game"
            };

            ContentDialogResult result = await dialog.ShowAsync();

            if (!(result == ContentDialogResult.Primary))
                App.Current.Exit();
        }
    }

}
