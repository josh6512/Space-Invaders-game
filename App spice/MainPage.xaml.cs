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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App_spice
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            GameManger manger = new GameManger(can);
            
            // Enemy Movement & Shoots
            DispatcherTimer timerMoveEnemies = new DispatcherTimer();
            timerMoveEnemies.Interval = TimeSpan.FromMilliseconds(10);
            timerMoveEnemies.Tick += manger.MoveEnemiesAndShoot;
            timerMoveEnemies.Start();

            // Keyboard 
            DispatcherTimer timerReadKeyboard = new DispatcherTimer();
            timerReadKeyboard.Interval = TimeSpan.FromMilliseconds(5);
            timerReadKeyboard.Tick += manger.ReadKeyboard;
            timerReadKeyboard.Start();

            // Check Collision
            DispatcherTimer timerCollision = new DispatcherTimer();
            timerCollision.Interval = TimeSpan.FromMilliseconds(10);
            timerCollision.Tick += manger.ShotCollision;
            timerCollision.Start();

            // Check New Level
            DispatcherTimer timerStartNewLevel = new DispatcherTimer();
            timerStartNewLevel.Interval = TimeSpan.FromSeconds(5);
            timerStartNewLevel.Tick += manger.StartNewLevel;
            timerStartNewLevel.Start();

        }

    }
}
