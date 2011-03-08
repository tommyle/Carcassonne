using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

namespace Carcassonne
{
    public partial class Meeple : UserControl
    {
        public MeepleType meepleType = new MeepleType();
        public MeepleSourceImages meepleSourceImages = new MeepleSourceImages();

        // Mouse stuff
        private Boolean isRectMouseCapture = false;
        private Point clickPosition;

        public Meeple(MeepleType inMeepleType)
        {
            InitializeComponent();
            meepleType = inMeepleType;

            Uri uri = new Uri(meepleSourceImages.list[meepleType], UriKind.Relative);
            BitmapImage bitmapImage = new BitmapImage(uri);
            meepleImage.ImageSource = bitmapImage;

            this.MouseLeftButtonUp += new MouseButtonEventHandler(rect_MouseLeftButtonUp);
            this.MouseLeftButtonDown += new MouseButtonEventHandler(rect_MouseLeftButtonDown);
            this.MouseMove += new MouseEventHandler(rect_MouseMove);
        }

        public void Angle(int angle)
        {
            rotation.Angle = angle;
        }

        #region meeple mouse events

        void rect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Meeple meeple = (Meeple)sender;

            meeple.ReleaseMouseCapture();
            isRectMouseCapture = false;
        }

        void rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Meeple target = (Meeple)sender;
            clickPosition = e.GetPosition(target.rect as UIElement);
            target.CaptureMouse();
            isRectMouseCapture = true;
        }

        void rect_MouseMove(object sender, MouseEventArgs e)
        {
            if (isRectMouseCapture)
            {
                Meeple meeple = (Meeple)sender;
                Canvas.SetLeft(meeple.rect, e.GetPosition(this).X - clickPosition.X);
                Canvas.SetTop(meeple.rect, e.GetPosition(this).Y - clickPosition.Y);
                Canvas.SetZIndex(meeple, 99);
            }
        }

        #endregion
    }

    public enum MeepleType { Yellow, Red, Green, Blue };

    public class MeepleSourceImages
    {
        public Dictionary<MeepleType, string> list = new Dictionary<MeepleType, string>();

        public MeepleSourceImages()
        {
            list.Add(MeepleType.Yellow, "Media/Icons/m_yellow.png");
            list.Add(MeepleType.Red, "Media/Icons/m_red.png");
            list.Add(MeepleType.Green, "Media/Icons/m_green.png");
            list.Add(MeepleType.Blue, "Media/Icons/m_blue.png");
        }
    }
}
