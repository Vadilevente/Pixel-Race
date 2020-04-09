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

namespace WPF_Pixel_Race__page_
{
    /// <summary>
    /// Interaction logic for Page_palya.xaml
    /// </summary>
    public partial class Page_palya : Page
    {
        bool vege_a_jateknak = false;
        enum egtalyak { NY = 0, É = 1, K = 2, D = 3 }
        egtalyak P1_irany = egtalyak.NY;
        egtalyak P2_irany = egtalyak.K;
        struct Megjeloles
        {
            public int X_kordinata;
            public int Y_kordinata;
            public Megjeloles(int X, int Y)
            {
                X_kordinata = X;
                Y_kordinata = Y;
            }
        }
        List<Megjeloles> P1_Megjeloltek = new List<Megjeloles>();
        List<Megjeloles> P2_Megjeloltek = new List<Megjeloles>();
        Brush P1_Szine = Brushes.DarkBlue;
        Brush P1_FalaSzine = Brushes.Blue;
        Brush P2_Szine = Brushes.DarkRed;
        Brush P2_FalaSzine = Brushes.Red;
        Brush palya_szine = Brushes.WhiteSmoke;
        // alap: P1_Szine ,P1_FalaSzine,P2_Szine,P2_FalaSzine,palya_szine
        bool olvasas = false;
        int P1_X = 0;
        int P1_Y = 0;
        int P2_X = 0;
        int P2_Y = 0;
        int szamolo = 0;
        Grid maingrid = new Grid();
        
        List<List<Button>> buttons = new List<List<Button>>();
        
        int szamolgato = 0;
        string gyozott;
        int szelesseg,magassag,gyorsasag;
        string p1neve, p2neve;
        public Page_palya(int Szelesseg, int Magassag, string P1neve, string P2neve, int Gyorsasag)
        {
            szelesseg = Szelesseg;
            magassag = Magassag;
            p1neve = P1neve;
            p2neve = P2neve;
            gyorsasag = Gyorsasag;
            InitializeComponent();
            KeyDown += new KeyEventHandler(Window1_KeyDown);
            Start(szelesseg, magassag, p1neve, p2neve);
        }
        
        private void Start(int Sz, int M, string P1, string P2)
        {
            DispatcherTimer timer = new DispatcherTimer();
            DispatcherTimer timer2 = new DispatcherTimer();
            if (szamolo != 1)
            {
                for (int i = 0; i < Sz; i++)
                {
                    maingrid.ColumnDefinitions.Add(new ColumnDefinition());
                }
                for (int i = 0; i < M; i++)
                {
                    maingrid.RowDefinitions.Add(new RowDefinition());
                }
                for (int j = 0; j < Sz; j++)
                {
                    buttons.Add(new List<Button>());
                    for (int i = 0; i < M; i++)
                    {
                        buttons.Last().Add(new Button());
                        maingrid.Children.Add(buttons.Last().Last());
                        Grid.SetRow(buttons.Last().Last(), i);
                        Grid.SetColumn(buttons.Last().Last(), j);
                        //                buttons[j][i].Content = j+" ; "+i;
                    }
                }
            }
            szamolo = 1;
            Content = maingrid;
            ///refresh
            Karakterek_felvétele(Sz, M, P1, P2);
        }
        
        void Karakterek_felvétele(int Sz, int M, string P1, string P2)
        {
            /// P1
            if (M % 2 == 0)
            {
                P2_Y = M / 2;
            }
            else
                P2_Y = (M - 1) / 2;
            /// P2
            P1_X = Sz - 1;
            if (M % 2 == 0)
            {
                P1_Y = M / 2 - 1;
            }
            else
                P1_Y = (M - 1) / 2;
            olvasas = true;
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            if (gyorsasag == 3)
            {
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            }
            if (gyorsasag == 2)
            {
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 50 * 2);
            }
            if (gyorsasag == 1)
            {
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 50 * 4);
            }
            if (gyorsasag == 0)
            {
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 50 * 8);
            }
            dispatcherTimer.Start();
            System.Windows.Threading.DispatcherTimer dispatcherTimer2 = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer2.Tick += dispatcherTimer2_Tick;
            dispatcherTimer2.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer2.Start();
            Kirajzolas(P1_Szine, P1_FalaSzine, P2_Szine, P2_FalaSzine, palya_szine);
        }
        void Kirajzolas(Brush P1szine, Brush P1falaszine, Brush P2szine, Brush P2falaszine, Brush palyaszine)
        {
            /// Kirajzolás
            for (int i = 0; i < szelesseg; i++)
            {
                for (int j = 0; j < magassag; j++)
                {
                    buttons[i][j].Background = palyaszine;
                    buttons[i][j].Content = "";
                    buttons[i][j].IsEnabled = false;
                    for (int k = 0; k < P1_Megjeloltek.Count(); k++)
                    {
                        if (P1_Megjeloltek[k].X_kordinata == i && P1_Megjeloltek[k].Y_kordinata == j)
                        {
                            buttons[i][j].Background = P1falaszine;
                        }
                    }
                    for (int k = 0; k < P2_Megjeloltek.Count(); k++)
                    {
                        if (P2_Megjeloltek[k].X_kordinata == i && P2_Megjeloltek[k].Y_kordinata == j)
                        {
                            buttons[i][j].Background = P2falaszine;
                        }
                    }
                    if (P1_X == i && P1_Y == j)
                    {
                        P1_Megjeloltek.Add(new Megjeloles(i, j));
                        buttons[i][j].Content = p1neve;
                        buttons[i][j].Background = P1szine;
                    }
                    if (P2_X == i && P2_Y == j)
                    {
                        P2_Megjeloltek.Add(new Megjeloles(i, j));
                        buttons[i][j].Content = p2neve;
                        buttons[i][j].Background = P2szine;
                    }
                }
            }
        }
        void vegeredmeny(string gyöztes)
        {
            if (gyöztes == "döntetlen")
            {
                vege_a_jateknak = true;
                Kirajzolas(Brushes.DarkGray, Brushes.LightBlue, Brushes.DarkGray, Brushes.OrangeRed, Brushes.WhiteSmoke);
                //MessageBox.Show("Döntetlen", "Végeredmény");
                gyozott = "döntetlen";

            }
            if (gyöztes == "P2")
            {
                vege_a_jateknak = true;
                Kirajzolas(Brushes.Gray, Brushes.LightBlue, Brushes.DarkRed, Brushes.Red, Brushes.WhiteSmoke);
                //MessageBox.Show("Győzött: "+p2neve,"Végeredmény");
                gyozott = "P2";
            }
            if (gyöztes == "P1")
            {
                vege_a_jateknak = true;
                Kirajzolas(Brushes.DarkBlue, Brushes.Blue, Brushes.DarkGray, Brushes.OrangeRed, Brushes.WhiteSmoke);
                //MessageBox.Show("Győzött: " + p1neve, "Végeredmény");
                gyozott = "P1";
            }
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            /// P1
            if (!vege_a_jateknak)
            {
                bool vesztettP1 = false;
                bool vesztettP2 = false;
                switch (P1_irany)
                {
                    case egtalyak.É:
                        if (P1_Y - 1 == -1 || P2_Megjeloltek.Contains(new Megjeloles(P1_X, P1_Y - 1)) || P1_Megjeloltek.Contains(new Megjeloles(P1_X, P1_Y - 1)))
                        {
                            vesztettP1 = true;
                        }
                        else
                            P1_Y--;
                        break;
                    case egtalyak.D:
                        if (P1_Y + 2 > magassag || P2_Megjeloltek.Contains(new Megjeloles(P1_X, P1_Y + 1)) || P1_Megjeloltek.Contains(new Megjeloles(P1_X, P1_Y + 1)))
                        {
                            vesztettP1 = true;
                        }
                        else
                            P1_Y++;
                        break;
                    case egtalyak.K:
                        if (P1_X + 2 > szelesseg || P2_Megjeloltek.Contains(new Megjeloles(P1_X + 1, P1_Y)) || P1_Megjeloltek.Contains(new Megjeloles(P1_X + 1, P1_Y)))
                        {
                            vesztettP1 = true;
                        }
                        else
                            P1_X++;
                        break;
                    case egtalyak.NY:
                        if (P1_X - 1 == -1 || P2_Megjeloltek.Contains(new Megjeloles(P1_X - 1, P1_Y)) || P1_Megjeloltek.Contains(new Megjeloles(P1_X - 1, P1_Y)))
                        {
                            vesztettP1 = true;
                        }
                        else
                            P1_X--;
                        break;
                    default:
                        break;
                }
                /// P2
                switch (P2_irany)
                {
                    case egtalyak.É:
                        if (P2_Y - 1 == -1 || P1_Megjeloltek.Contains(new Megjeloles(P2_X, P2_Y - 1)) || P2_Megjeloltek.Contains(new Megjeloles(P2_X, P2_Y - 1)))
                        {
                            vesztettP2 = true;
                        }
                        else
                            P2_Y--;
                        break;
                    case egtalyak.D:
                        if (P2_Y + 2 > magassag || P1_Megjeloltek.Contains(new Megjeloles(P2_X, P2_Y + 1)) || P2_Megjeloltek.Contains(new Megjeloles(P2_X, P2_Y + 1)))
                        {
                            vesztettP2 = true;
                        }
                        else
                            P2_Y++;
                        break;
                    case egtalyak.K:
                        if (P2_X + 2 > szelesseg || P1_Megjeloltek.Contains(new Megjeloles(P2_X + 1, P2_Y)) || P2_Megjeloltek.Contains(new Megjeloles(P2_X + 1, P2_Y)))
                        {
                            vesztettP2 = true;
                        }
                        else
                            P2_X++;
                        break;
                    case egtalyak.NY:
                        if (P2_X - 1 == -1 || P1_Megjeloltek.Contains(new Megjeloles(P2_X - 1, P2_Y)) || P2_Megjeloltek.Contains(new Megjeloles(P2_X - 1, P2_Y)))
                        {
                            vesztettP2 = true;
                        }
                        else
                            P2_X--;
                        break;
                    default:
                        break;
                }
                if (vesztettP1 && vesztettP2)
                {
                    vegeredmeny("döntetlen");
                }
                else
                {
                    if (vesztettP1)
                    {
                        vegeredmeny("P2");
                    }
                    if (vesztettP2)
                    {
                        vegeredmeny("P1");
                    }
                }
                if (P1_X == P2_X && P1_Y == P2_Y)
                {
                    vegeredmeny("döntetlen");
                }

                Kirajzolas(P1_Szine, P1_FalaSzine, P2_Szine, P2_FalaSzine, palya_szine);
            }
        }
        private void dispatcherTimer2_Tick(object sender, EventArgs e)
        {
            if (vege_a_jateknak && szamolgato == 1)
            {
                szamolgato++;
                if (gyozott == "döntetlen")
                {
                    Kirajzolas(Brushes.Black, palya_szine, Brushes.Black, palya_szine, palya_szine);
                    MessageBox.Show("Döntetlen", "Végeredmény");
                }
                if (gyozott == "P1")
                {
                    Kirajzolas(P1_Szine, P1_FalaSzine, Brushes.Black, palya_szine, palya_szine);
                    MessageBox.Show("Nyert: " + p1neve, "Végeredmény");
                }
                if (gyozott == "P2")
                {
                    Kirajzolas(Brushes.Black, palya_szine, P2_Szine, P2_FalaSzine, palya_szine);
                    MessageBox.Show("Nyert: " + p2neve, "Végeredmény");
                }
            }
            if (vege_a_jateknak && szamolgato == 0)
            {
                szamolgato++;
                //P1_Szine ,P1_FalaSzine,P2_Szine,P2_FalaSzine,palya_szine
                if (gyozott == "döntetlen")
                {
                    Kirajzolas(Brushes.DarkGray, Brushes.LightBlue, Brushes.DarkGray, Brushes.OrangeRed, palya_szine);
                }
                if (gyozott == "P1")
                {
                    Kirajzolas(P1_Szine, P1_FalaSzine, Brushes.DarkGray, Brushes.OrangeRed, palya_szine);
                }
                if (gyozott == "P2")
                {
                    Kirajzolas(Brushes.DarkGray, Brushes.LightBlue, P2_Szine, P2_FalaSzine, palya_szine);
                }
            }

        }
        private void Window1_KeyDown(object sender, KeyEventArgs e)
        {
            if (olvasas)
            {
                switch (e.Key)
                {
                    /// P1
                    case Key.Right:
                        if ((egtalyak)(int)P1_irany == egtalyak.D)
                        {
                            P1_irany = egtalyak.NY;
                        }
                        else
                            P1_irany = (egtalyak)(int)P1_irany + 1;
                        break;
                    case Key.Left:
                        if ((egtalyak)(int)P1_irany == egtalyak.NY)
                        {
                            P1_irany = egtalyak.D;
                        }
                        else
                            P1_irany = (egtalyak)(int)P1_irany - 1;
                        break;
                    /// P2
                    case Key.D:
                        if ((egtalyak)(int)P2_irany == egtalyak.D)
                        {
                            P2_irany = egtalyak.NY;
                        }
                        else
                            P2_irany = (egtalyak)(int)P2_irany + 1;
                        break;
                    case Key.A:
                        if ((egtalyak)(int)P2_irany == egtalyak.NY)
                        {
                            P2_irany = egtalyak.D;
                        }
                        else
                            P2_irany = (egtalyak)(int)P2_irany - 1;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}