using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace line_numbering
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

        private void textControl1_Loaded(object sender, RoutedEventArgs e)
        {
            textControl1.Focus();
        }

        private System.Drawing.Brush LINE_COLOR = new SolidBrush(System.Drawing.Color.Gray);
        List<Canvas> canvasList = new List<Canvas>();

        private void setLineNumbering()
        {
            //clears grid
            if (canvasList.Count != 0)
            {
                foreach (Canvas canvas in canvasList)
                {
                    grid1.Children.Remove(canvas);
                }
            }
            //iterates all lines
            foreach (TXTextControl.Line line in textControl1.Lines)
            {
                Canvas holder = null;
                TextBlock textBlock = new TextBlock();
                textBlock.Text = line.Number.ToString();

                //left position of current line minus offset so that the line number is displayed next to the current line
                double left = line.TextBounds.X;

                //top position of the current line
                double top = line.Baseline + 567;

                //gets the positon of the current line in client coordinates
                System.Windows.Point relativePoint = textControl1.PointFromDocument(new System.Windows.Point(left, top));

                //new canvas for textblock
                holder = new Canvas();

                canvasList.Add(holder);


                holder.HorizontalAlignment = HorizontalAlignment.Left;

                holder.VerticalAlignment = VerticalAlignment.Top;
                //set position of the canvas to the current line coordinates
                holder.Margin = new Thickness(relativePoint.X, relativePoint.Y, 0, 0);

                holder.Children.Add(textBlock);

                grid1.Children.Add(holder);

                holder.BringIntoView();

            }

        }
        private void textControl1_Changed(object sender, EventArgs e)
        {
            setLineNumbering();

        }

        private void textControl1_InputPositionChanged(object sender, EventArgs e)
        {
            setLineNumbering();

        }

        private void textControl1_VScroll(object sender, EventArgs e)
        {
            setLineNumbering();

        }

        private void textControl1_HScroll(object sender, EventArgs e)
        {
            setLineNumbering();

        }

    }
}
