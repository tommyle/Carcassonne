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
    public partial class Portrait : UserControl
    {
        // Mouse stuff
        private Boolean isRectMouseCapture = false;
        private Point clickPosition;
        private PortraitType portraitType;

        public PortraitSourceImages portraitSourceImages = new PortraitSourceImages();

        public Portrait(PortraitType inPortraitType)
        {
            portraitType = inPortraitType;

            InitializeComponent();

            Uri uri = new Uri(portraitSourceImages.list[portraitType], UriKind.Relative);
            BitmapImage bitmapImage = new BitmapImage(uri);
            portraitImage.ImageSource = bitmapImage;

            this.MouseLeftButtonUp += new MouseButtonEventHandler(rect_MouseLeftButtonUp);
            this.MouseLeftButtonDown += new MouseButtonEventHandler(rect_MouseLeftButtonDown);
        }

        public void Angle(int angle)
        {
            rotation.Angle = angle;
        }

        void rect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Portrait portrait = (Portrait)sender;

            portrait.ReleaseMouseCapture();
            isRectMouseCapture = false;


            portraitType = EnumHelper.GetNext(portraitType);

            Uri uri = new Uri(portraitSourceImages.list[portraitType], UriKind.Relative);
            BitmapImage bitmapImage = new BitmapImage(uri);
            portraitImage.ImageSource = bitmapImage;
        }

        void rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Portrait target = (Portrait)sender;
            target.CaptureMouse();
            isRectMouseCapture = true;
        }
    }

    public enum PortraitType { Count = 1, Countess = 2, FortuneTeller = 3, Hans = 4, Juggler = 5, Maid = 6, Servant = 7, Warlock = 8, Witch = 9};

    public class EnumHelper
    {
        public static PortraitType GetNext(PortraitType pt)
        {
            int portraitTypeAsInt = (int)pt;

            // 'Witch' is the last element
            if (portraitTypeAsInt >= (int)PortraitType.Witch)
            {
                return (PortraitType)1;
            }
            else
            {
                portraitTypeAsInt++;
                return (PortraitType)portraitTypeAsInt;
            }
        }
    }

    public class PortraitSourceImages
    {
        public Dictionary<PortraitType, string> list = new Dictionary<PortraitType, string>();

        public PortraitSourceImages()
        {
            list.Add(PortraitType.Count, "Media/Icons/Count.png");
            list.Add(PortraitType.Countess, "Media/Icons/Countess.png");
            list.Add(PortraitType.FortuneTeller, "Media/Icons/Fortune_Teller.png");
            list.Add(PortraitType.Hans, "Media/Icons/Hans.png");
            list.Add(PortraitType.Juggler, "Media/Icons/Juggler.png");
            list.Add(PortraitType.Maid, "Media/Icons/Maid.png");
            list.Add(PortraitType.Servant, "Media/Icons/Servant.png");
            list.Add(PortraitType.Warlock, "Media/Icons/Warlock.png");
            list.Add(PortraitType.Witch, "Media/Icons/Witch.png");
        }
    }
}
