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
    public partial class Tile : UserControl
    {
		public TileSlot slot = null;

        public Orientation orientation = Orientation.N;
        public Border borders = new Border();
        public TileType tileType = new TileType();
        public SourceImages sourceImages = new SourceImages();
		
		public bool isSlotted
		{
			get
			{
				return (slot != null);
			}
		}

		public bool canBeMoved
		{
			get
			{
				return !(slot != null && slot.tile != null && slot.tile.Equals(this));
			}
		}

        public Tile(TileType inTileType)
        {
            InitializeComponent();
            tileType = inTileType;

            Uri uri = new Uri(sourceImages.list[tileType], UriKind.Relative);
            BitmapImage bitmapImage = new BitmapImage(uri);
            tileImage.ImageSource = bitmapImage;

            switch (tileType)
            {
               case TileType.A:
                  borders.N = BorderType.Grass;
                  borders.E = BorderType.Grass;
                  borders.S = BorderType.Road;
                  borders.W = BorderType.Grass;
                  break;
               case TileType.B:
                  borders.N = BorderType.Grass;
                  borders.E = BorderType.Grass;
                  borders.S = BorderType.Grass;
                  borders.W = BorderType.Grass;
                  break;
              case TileType.C:
                  borders.N = BorderType.City;
                  borders.E = BorderType.City;
                  borders.S = BorderType.City;
                  borders.W = BorderType.City;
                  break;
              case TileType.D:
                  borders.N = BorderType.City;
                  borders.E = BorderType.Road;
                  borders.S = BorderType.Grass;
                  borders.W = BorderType.Road;
                  break;
              case TileType.E:
                  borders.N = BorderType.City;
                  borders.E = BorderType.Grass;
                  borders.S = BorderType.Grass;
                  borders.W = BorderType.Grass;
                  break;
              case TileType.F:
                  borders.N = BorderType.Grass;
                  borders.E = BorderType.City;
                  borders.S = BorderType.Grass;
                  borders.W = BorderType.City;
                  break;
              case TileType.G:
                  borders.N = BorderType.Grass;
                  borders.E = BorderType.City;
                  borders.S = BorderType.Grass;
                  borders.W = BorderType.City;
                  break;
              case TileType.H:
                  borders.N = BorderType.Grass;
                  borders.E = BorderType.City;
                  borders.S = BorderType.Grass;
                  borders.W = BorderType.City;
                  break;
              case TileType.I:
                  borders.N = BorderType.City;
                  borders.E = BorderType.Grass;
                  borders.S = BorderType.Grass;
                  borders.W = BorderType.City;
                  break;
              case TileType.J:
                  borders.N = BorderType.City;
                  borders.E = BorderType.Road;
                  borders.S = BorderType.Road;
                  borders.W = BorderType.Grass;
                  break;
              case TileType.K:
                  borders.N = BorderType.City;
                  borders.E = BorderType.Grass;
                  borders.S = BorderType.Road;
                  borders.W = BorderType.Road;
                  break;
              case TileType.L:
                  borders.N = BorderType.City;
                  borders.E = BorderType.Road;
                  borders.S = BorderType.Road;
                  borders.W = BorderType.Road;
                  break;
              case TileType.M:
                  borders.N = BorderType.City;
                  borders.E = BorderType.Grass;
                  borders.S = BorderType.Grass;
                  borders.W = BorderType.City;
                  break;
              case TileType.N:
                  borders.N = BorderType.City;
                  borders.E = BorderType.Grass;
                  borders.S = BorderType.Grass;
                  borders.W = BorderType.City;
                  break;
              case TileType.O:
                  borders.N = BorderType.City;
                  borders.E = BorderType.Road;
                  borders.S = BorderType.Road;
                  borders.W = BorderType.City;
                  break;
              case TileType.P:
                  borders.N = BorderType.City;
                  borders.E = BorderType.Road;
                  borders.S = BorderType.Road;
                  borders.W = BorderType.City;
                  break;
              case TileType.Q:
                  borders.N = BorderType.City;
                  borders.E = BorderType.City;
                  borders.S = BorderType.Grass;
                  borders.W = BorderType.City;
                  break;
              case TileType.R:
                  borders.N = BorderType.City;
                  borders.E = BorderType.City;
                  borders.S = BorderType.Grass;
                  borders.W = BorderType.City;
                  break;
              case TileType.S:
                  borders.N = BorderType.City;
                  borders.E = BorderType.City;
                  borders.S = BorderType.Road;
                  borders.W = BorderType.City;
                  break;
              case TileType.T:
                  borders.N = BorderType.City;
                  borders.E = BorderType.City;
                  borders.S = BorderType.Road;
                  borders.W = BorderType.City;
                  break;
              case TileType.U:
                  borders.N = BorderType.Grass;
                  borders.E = BorderType.Road;
                  borders.S = BorderType.Grass;
                  borders.W = BorderType.Road;
                  break;
              case TileType.V:
                  borders.N = BorderType.Grass;
                  borders.E = BorderType.Grass;
                  borders.S = BorderType.Road;
                  borders.W = BorderType.Road;
                  break;
              case TileType.W:
                  borders.N = BorderType.Grass;
                  borders.E = BorderType.Road;
                  borders.S = BorderType.Road;
                  borders.W = BorderType.Road;
                  break;
               case TileType.X:
                  borders.N = BorderType.Road;
                  borders.E = BorderType.Road;
                  borders.S = BorderType.Road;
                  borders.W = BorderType.Road;
                  break;
               default:
                  throw new Exception();
            }
        }

        public void Rotate()
        {
			//if (orientation == Orientation.N)
			//    orientation = Orientation.E;
			//else if (orientation == Orientation.E)
			//    orientation = Orientation.S;
			//else if (orientation == Orientation.S)
			//    orientation = Orientation.W;
			//else
			//    orientation = Orientation.N;

			BorderType tmp = this.borders.N;

			this.borders.N = this.borders.W;
			this.borders.W = this.borders.S;
			this.borders.S = this.borders.E;
			this.borders.E = tmp;

            rotation.Angle += 90;
        }
    }

	public class Border
	{
		public BorderType N;
		public BorderType E;
		public BorderType S;
		public BorderType W;
	}

	public enum BorderType { Road, City, Grass };

	public enum TileType { A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X };

	public enum Orientation { N, E, S, W };

	public class SourceImages
	{
		public Dictionary<TileType, string> list = new Dictionary<TileType, string>();

		public SourceImages()
		{
			list.Add(TileType.A, "Media/Tiles/A.png");
			list.Add(TileType.B, "Media/Tiles/B.png");
			list.Add(TileType.C, "Media/Tiles/C.png");
			list.Add(TileType.D, "Media/Tiles/D.png");
			list.Add(TileType.E, "Media/Tiles/E.png");
			list.Add(TileType.F, "Media/Tiles/F.png");
			list.Add(TileType.G, "Media/Tiles/G.png");
			list.Add(TileType.H, "Media/Tiles/H.png");
			list.Add(TileType.I, "Media/Tiles/I.png");
			list.Add(TileType.J, "Media/Tiles/J.png");
			list.Add(TileType.K, "Media/Tiles/K.png");
			list.Add(TileType.L, "Media/Tiles/L.png");
			list.Add(TileType.M, "Media/Tiles/M.png");
			list.Add(TileType.N, "Media/Tiles/N.png");
			list.Add(TileType.O, "Media/Tiles/O.png");
			list.Add(TileType.P, "Media/Tiles/P.png");
			list.Add(TileType.Q, "Media/Tiles/Q.png");
			list.Add(TileType.R, "Media/Tiles/R.png");
			list.Add(TileType.S, "Media/Tiles/S.png");
			list.Add(TileType.T, "Media/Tiles/T.png");
			list.Add(TileType.U, "Media/Tiles/U.png");
			list.Add(TileType.V, "Media/Tiles/V.png");
			list.Add(TileType.W, "Media/Tiles/W.png");
			list.Add(TileType.X, "Media/Tiles/X.png");
		}
	}

}
