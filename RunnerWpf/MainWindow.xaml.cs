using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Diagnostics;
namespace RunnerWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int speed = 20;
        int overclocking = 0;
        int score = 0;

        DispatcherTimer gameTimer = new DispatcherTimer();
        Rect Playerhitbox;
        Rect Groundhitbox;
        Rect obstaclehitbox;

        bool jumping;
        int fors = 20;
        Random random = new Random();
        bool gameover = false;
        double spriteInt = 0;
        ImageBrush PlayerSprite = new ImageBrush();
        ImageBrush BackgroundSprite = new ImageBrush();
        ImageBrush ObstacleSprite = new ImageBrush();

        int[] obstacleposition = {320,310,300,305,315};

        TimeSpan previosTime;
        Stopwatch stopwatch = Stopwatch.StartNew();

        public MainWindow()
        {
            InitializeComponent();
            MyCanvas.Focus();
            previosTime = stopwatch.Elapsed;
            CompositionTarget.Rendering += GameEngine; 
            BackgroundSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/background.gif"));
            Backround.Fill = BackgroundSprite;
            Backround2.Fill = BackgroundSprite;
            StarteGame();

        }

        private void MyCanvas_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void MyCanvas_KeyUp(object sender, KeyEventArgs e)
        {

        }
        private void StarteGame()
        {
            RunSprite(1);
            ObstacleSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/obstacle.png"));
            obstale.Fill = ObstacleSprite;
            //gameTimer.Start();
        }
        private void RunSprite(double index)
        {
            if ((int)index!= 0 && (int)index<8)
            {
                PlayerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pic" + (int)index + ".gif"));
                Player.Fill = PlayerSprite;
            }
        
        }
        private void GameEngine(object sender,EventArgs e)
        {
            Canvas.SetTop(Player, Canvas.GetTop(Player) + speed);
         
            Canvas.SetLeft(Backround, Canvas.GetLeft(Backround)-3);
            Canvas.SetLeft(Backround2, Canvas.GetLeft(Backround2) - 3);
            Canvas.SetLeft(obstale, Canvas.GetLeft(obstale) - 10);
            ScoreText.Content = "Score: " + score;
            Playerhitbox = new Rect(Canvas.GetLeft(Player), Canvas.GetTop(Player), Player.Width, Player.Height);
            obstaclehitbox = new Rect(Canvas.GetLeft(obstale), Canvas.GetTop(obstale), obstale.Width, obstale.Height);
            Groundhitbox = new Rect(Canvas.GetLeft(Ground), Canvas.GetTop(Ground), Ground.Width, Ground.Height);
            if (Playerhitbox.IntersectsWith(Groundhitbox))
            {
                speed = 0;
                overclocking = 0;
                Canvas.SetTop(Player, Canvas.GetTop(Ground) - Player.Height);
                jumping = false;
                spriteInt+=0.2;
                if (spriteInt>8)
                {
                    spriteInt = 0.2;
                }
                RunSprite(spriteInt);
            }
        }
    }
}
