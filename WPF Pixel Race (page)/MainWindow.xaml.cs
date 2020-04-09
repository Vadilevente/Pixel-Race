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

namespace WPF_Pixel_Race__page_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }
        string szelesseg = "Hiba", magassag = "Hiba", p1neve = "Hiba", p2neve = "Hiba";
        int gyorsasag = 3;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (lb_gyorsasag.SelectedIndex == -1)
            {
                gyorsasag = 0;
            }
            else
            {
                gyorsasag = lb_gyorsasag.SelectedIndex;
            }

            int ideiglenes = 0;
            try
            {
                ideiglenes = int.Parse(tb_szelesseg.Text);
            }
            catch (Exception)
            {
                //MessageBox.Show("Rossz a formátum.", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (ideiglenes == 0 || 80 < int.Parse(tb_szelesseg.Text) || int.Parse(tb_szelesseg.Text) < 3)
            {
                MessageBox.Show("Túl nagy, vagy túl kicsi a megadott szélesség, vagy rossz a formátum.", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                szelesseg = tb_szelesseg.Text;
            }
            ///
            ideiglenes = 0;
            try
            {
                ideiglenes = int.Parse(tb_szelesseg.Text);
            }
            catch (Exception)
            {

            }
            if (ideiglenes == 0 || 80 < int.Parse(tb_magassag.Text) || int.Parse(tb_magassag.Text) < 3)
            {
                MessageBox.Show("Túl nagy, vagy túl kicsi a megadott magasság, vagy rossz a formátum.", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                magassag = tb_magassag.Text;
            }
            ///
            if (6 < tb_p1_neve.Text.Length || tb_p1_neve.Text == "")
            {
                MessageBox.Show("Túl hosszú  a P1 neve, vagy nem adtál meg semmit.", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                p1neve = tb_p1_neve.Text;
            }
            ///
            if (6 < tb_p2_neve.Text.Length || tb_p2_neve.Text == "")
            {
                MessageBox.Show("Túl hosszú  a P2 neve, vagy nem adtál meg semmit.", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                p2neve = tb_p2_neve.Text;
            }
            if (szelesseg == "Hiba" || magassag == "Hiba" || p1neve == "Hiba" || p2neve == "Hiba")
            {
            }
            else
            {
                MessageBox.Show("Kezdődhet a játék!", "Sikeres volt a beolvasás!", MessageBoxButton.OK);
                l_Info.Content = "Szélesség: " + szelesseg + "\nMagasság: " + magassag + "\nP1: " + p1neve + "\nP2: " + p2neve + "\ngyorsaság: " + lb_gyorsasag.SelectedIndex;
                ///int.Parse(szelesseg), int.Parse(magassag), p1neve, p2neve, gyorsasag
                Main.Content = new Page_palya();
               
            }
        }


    }
}

