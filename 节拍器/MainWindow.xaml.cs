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
using System.Threading;
using System.Windows.Threading;
using System.Timers;
using System.Windows.Markup;

namespace 节拍器
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private int i = 0;
        private double Sleeptime = 1000;
        private BrushConverter conv = new BrushConverter();
        private DispatcherTimer timer = new DispatcherTimer();
        private MediaPlayer player = new MediaPlayer();
        private string MusicNmae = @"Music\StartMusic.mp3";
        private int Talsection = 0;
        private int TextSudu_Tal = 0;
        private int Endsection = 0;
        Brush bru_Time_s = null;
        Brush bru_Time_e = null;
        private bool IsSetSpeed = false;

        public MainWindow()
        {
            timer.Tick += Timer_Tick;

            

            InitializeComponent();
            //player.Open(new Uri(MusicNmae, UriKind.Relative));
            player.Open(new Uri(MusicNmae, UriKind.Relative));

            player.Play();
            bru_Time_s = (Brush)conv.ConvertFrom("#fcc307");
            bru_Time_e = (Brush)conv.ConvertFrom("#229453");

            //System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            //player.SoundLocation = @"E:\日志\项目\节拍器\节拍器\Music\M_1.mp3";
            //player.Load();
            //player.Play();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Sleeptime = Convert.ToDouble((Convert.ToDouble(60) / Convert.ToDouble(TextSudu.Text)) * 1000);
            
            if (!string.IsNullOrWhiteSpace(TextSudu_1.Text) && TextSudu_1.Text != "0")
            {
                IsSetSpeed = true;
            }

            if (this.btnStart.Content.ToString() == "开始")
            {
                if(IsSetSpeed)
                {
                    //计算需要停止的小节总数
                    Endsection = Convert.ToInt32(((Convert.ToDecimal((Convert.ToInt32(TextSudu_1.Text) - Convert.ToInt32(TextSudu.Text))) / Convert.ToDecimal(TextSudu_2.Text)) + 1) * Convert.ToDecimal(Textsection.Text));
                    if ((string.IsNullOrWhiteSpace(TextSudu_2.Text) || TextSudu_2.Text == "0") || (string.IsNullOrWhiteSpace(TextSudu.Text) || TextSudu.Text == "0") || (string.IsNullOrWhiteSpace(Textsection.Text) || Textsection.Text == "0")) 
                    {
                        if (string.IsNullOrWhiteSpace(TextSudu.Text) || TextSudu.Text == "0")
                        {
                            MessageBox.Show("增速训练模式下【节拍速度】不能设置为空或0！");
                        }

                        if (string.IsNullOrWhiteSpace(TextSudu_2.Text) || TextSudu_2.Text == "0")
                        {
                            MessageBox.Show("增速训练模式下【增加速度】不能设置为空或0！");
                        }

                        if (string.IsNullOrWhiteSpace(Textsection.Text) || Textsection.Text == "0")
                        {
                            MessageBox.Show("增速训练模式下【增速小节数】不能设置为空或0！");
                        }
                        return;
                    }
                }

                //确认总小节数
                Talsection = Convert.ToInt32(Texttalsection.Text);

                MusicNmae = @"Music\Countdown.mp3";

                player.Open(new Uri(MusicNmae, UriKind.Relative));

                player.Play();

                Thread.Sleep(6500);

                TextSudu_Tal = Convert.ToInt32(Textsection.Text);

               

                //S1.Text = Endsection.ToString();
                //S2.Text = TextSudu_Tal.ToString();

                //TextSudu_1.Text - TextSudu.Text / TextSudu_2.Text + 1 * Textsection.Text

                //Thread.Sleep(3000);

                Timer_Tick(null, null);
                this.btnStart.Content = "结束";
                Brush bru = conv.ConvertFromInvariantString("#BC464F") as Brush;
                btnStart.Background = (System.Windows.Media.Brush)bru;
                timer.Interval = TimeSpan.FromMilliseconds(Sleeptime);
                timer.Start();
                
            }
            else
            {
                this.btnStart.Content = "开始";
                Brush bru = conv.ConvertFromInvariantString("#229453") as Brush;
                btnStart.Background = (System.Windows.Media.Brush)bru;
                
                timer.Stop();
                player.Open(new Uri(@"Music\EndMusic.mp3", UriKind.Relative));
                player.Play();

                //Brush bru_Time_e = conv.ConvertFromInvariantString("#229453") as Brush;

                switch (i)
                {
                    case 1:
                        S1.Background = (System.Windows.Media.Brush)bru_Time_e;
                        break;
                    case 2:
                        S2.Background = (System.Windows.Media.Brush)bru_Time_e;
                        break;
                    case 3:
                        S3.Background = (System.Windows.Media.Brush)bru_Time_e;
                        break;
                    case 0:
                        S4.Background = (System.Windows.Media.Brush)bru_Time_e;
                        break;
                }
                i = 0;
                //Talsection -= 1;
            }
        }

        /// <summary>
        /// 节拍器方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object? sender, EventArgs e)
        {
            //Brush bru_Time_s = (Brush)conv.ConvertFrom("#fcc307");
            //Brush bru_Time_e = (Brush)conv.ConvertFrom("#229453");
            //conv.ConvertFromInvariantString("#fcc307") as Brush;
            //conv.ConvertFromInvariantString("#229453") as Brush;

            i++;

            switch (i)
            {
                case 1:
                    player.Open(new Uri(@"Music\M1.mp3", UriKind.Relative));
                    player.Play();
                    S4.Background = (System.Windows.Media.Brush)bru_Time_e;
                    S1.Background = (System.Windows.Media.Brush)bru_Time_s;
                    break;
                case 2:
                    player.Open(new Uri(@"Music\M2.mp3", UriKind.Relative));
                    player.Play();
                    S1.Background = (System.Windows.Media.Brush)bru_Time_e;
                    S2.Background = (System.Windows.Media.Brush)bru_Time_s;
                    break;
                case 3:
                    player.Open(new Uri(@"Music\M2.mp3", UriKind.Relative));
                    player.Play();
                    S2.Background = (System.Windows.Media.Brush)bru_Time_e;
                    S3.Background = (System.Windows.Media.Brush)bru_Time_s;
                    break;
                case 4:
                    player.Open(new Uri(@"Music\M2.mp3", UriKind.Relative));
                    player.Play();
                    i = 0;
                    Talsection += 1;
                    Texttalsection.Text = Talsection.ToString();
                    S3.Background = (System.Windows.Media.Brush)bru_Time_e;
                    S4.Background = (System.Windows.Media.Brush)bru_Time_s;

                    //目标速度为空则不限制时间和增加速度
                    if (IsSetSpeed)
                    {
                        //总小节数小于结束小节数
                        if (Talsection < Endsection)
                        {
                            //
                            if (Convert.ToInt32(TextSudu_Tal).Equals(Talsection) || TextSudu_Tal <= Talsection)
                            {
                                //YStart();
                                TextSudu_Tal += Convert.ToInt32(Textsection.Text);
                                //S2.Text = TextSudu_Tal.ToString();
                                //TextSudu_Tal
                                //Textsection.Text
                                TextSudu.Text = (Convert.ToInt32(TextSudu.Text) + Convert.ToInt32(TextSudu_2.Text)).ToString();
                            }
                        }
                        else if (Talsection.Equals(Endsection) || Talsection > Endsection)
                        {
                            timer.Stop();
                            player.Open(new Uri(@"Music\EndMusic.mp3", UriKind.Relative));
                            player.Play();

                            this.btnStart.Content = "开始";
                            Brush bru = conv.ConvertFromInvariantString("#229453") as Brush;
                            btnStart.Background = (System.Windows.Media.Brush)bru;

                            TextSudu_Tal = 0;
                            Talsection = 0;
                            Texttalsection.Text = "0";
                        }
                    }

                    break;
            }
        }

        private void TextSudu_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextSudu.Text))
            {
                double Sudu = Convert.ToDouble((Convert.ToDouble(60) / Convert.ToDouble(TextSudu.Text)) * 1000);

                //S1.Text = Sudu.ToString();

                if (Convert.ToInt32(TextSudu.Text) > 0 && Sudu != Sleeptime)
                {
                    Sleeptime = Sudu;
                    timer.Interval = TimeSpan.FromMilliseconds(Sleeptime);
                }
            }
        }

        private void Textsection_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!string.IsNullOrEmpty(Textsection.Text))
            {
                TextSudu_Tal = Convert.ToInt32(Textsection.Text);
            }
            else if(IsSetSpeed && string.IsNullOrEmpty(Textsection.Text))
            {
                MessageBox.Show("增速训练模式下【增速小节数】不能设置为空！");
            }
        }

        private void TextSudu_1_TextChanged(object sender, TextChangedEventArgs e)
        {
            //目标速度为空则不限制时间和增加速度
            if (string.IsNullOrWhiteSpace(TextSudu_1.Text) || TextSudu_1.Text == "0")
            {
                IsSetSpeed = false;
            }

            if (!string.IsNullOrWhiteSpace(TextSudu_1.Text) && (Convert.ToInt32(TextSudu_1.Text) > 420))
            {
                TextSudu_1.Text = "420";
                MessageBox.Show("超出最大可设置范围【1~420 BPM】！！！");
            }
        }
    }
}
