using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Kokoava
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        
        public MainWindow()
        {
            //Kieliasetus lokalisoinnin testausta varten
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            InitializeComponent();
        }

        private void DrawButton_Click(object sender, RoutedEventArgs e)
        {
            
            var radiobutton = sender as RadioButton;
            string radioBPressed = radiobutton.Name.ToString();
            


            if (radioBPressed == "Piirrä")
            {
                MyCanvas.EditingMode = InkCanvasEditingMode.Ink;
                MyCanvas.UseCustomCursor = true;
                MyCanvas.Cursor = Cursors.Pen;

            }
            else if (radioBPressed == "Pyyhi")
            {
                this.MyCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                MyCanvas.UseCustomCursor = false;
            }
            else if (radioBPressed == "Valitse")
            {
                this.MyCanvas.EditingMode = InkCanvasEditingMode.Select;
                MyCanvas.UseCustomCursor = false;
            }
        }

        private void Puhdista_Ruutu_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MyCanvas.Strokes.Count != 0)
            {
                while (MyCanvas.Strokes.Count > 0)
                {
                    MyCanvas.Strokes.RemoveAt(MyCanvas.Strokes.Count - 1);
                }
            }
            else
            {

            }
        }

        private void MenuItem_Save_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "isf files (*.isf)|*.isf";

            if (saveFileDialog1.ShowDialog() == true)
            {
                FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                MyCanvas.Strokes.Save(fs);
                fs.Close();
            }
        }

        private void MenuItem_Open_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "isf files (*.isf)|*.isf";

            if (openFileDialog1.ShowDialog() == true)
            {
                FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open);
                MyCanvas.Strokes = new StrokeCollection(fs);
                fs.Close();
            }

        }

        private void MyCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            strokeAttr.Color = (Color)Colorpicker.SelectedColor;
            strokeAttr.Width = Convert.ToDouble(siveltimen_koko.SelectionBoxItem);
            strokeAttr.Height = Convert.ToDouble(siveltimen_koko.SelectionBoxItem);
            
        }

        private void MenuItem_ExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        
    }
}
