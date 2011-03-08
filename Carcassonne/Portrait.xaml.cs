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
        public PortraitSourceImages portraitSourceImages = new PortraitSourceImages();

        public Portrait(PortraitType inPortraitType)
        {
            InitializeComponent();

            Uri uri = new Uri(portraitSourceImages.list[inPortraitType], UriKind.Relative);
            BitmapImage bitmapImage = new BitmapImage(uri);
            portraitImage.ImageSource = bitmapImage;
        }

        public void Angle(int angle)
        {
            rotation.Angle = angle;
        }
    }

    public enum PortraitType { Count, Countess, FortuneTeller, Hans, Juggler, Maid, Servant, Warlock, Witch};

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
