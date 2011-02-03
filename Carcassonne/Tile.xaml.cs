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
        public class Border
        {
            public enum BorderType { Road, City, Grass };
            public BorderType N;
            public BorderType E;
            public BorderType S;
            public BorderType W;
        }

        public class SourceImages
        {
            public Dictionary<Tile.TileType, string> list = new Dictionary<Tile.TileType, string>();

            public SourceImages()
            {
                list.Add(Tile.TileType.A, "Media/Tiles/A.png");
                list.Add(Tile.TileType.B, "Media/Tiles/B.png");
                list.Add(Tile.TileType.C, "Media/Tiles/C.png");
                list.Add(Tile.TileType.D, "Media/Tiles/D.png");
                list.Add(Tile.TileType.E, "Media/Tiles/E.png");
                list.Add(Tile.TileType.F, "Media/Tiles/F.png");
                list.Add(Tile.TileType.G, "Media/Tiles/G.png");
                list.Add(Tile.TileType.H, "Media/Tiles/H.png");
                list.Add(Tile.TileType.I, "Media/Tiles/I.png");
                list.Add(Tile.TileType.J, "Media/Tiles/J.png");
                list.Add(Tile.TileType.K, "Media/Tiles/K.png");
                list.Add(Tile.TileType.L, "Media/Tiles/L.png");
                list.Add(Tile.TileType.M, "Media/Tiles/M.png");
                list.Add(Tile.TileType.N, "Media/Tiles/N.png");
                list.Add(Tile.TileType.O, "Media/Tiles/O.png");
                list.Add(Tile.TileType.P, "Media/Tiles/P.png");
                list.Add(Tile.TileType.Q, "Media/Tiles/Q.png");
                list.Add(Tile.TileType.R, "Media/Tiles/R.png");
                list.Add(Tile.TileType.S, "Media/Tiles/S.png");
                list.Add(Tile.TileType.T, "Media/Tiles/T.png");
                list.Add(Tile.TileType.U, "Media/Tiles/U.png");
                list.Add(Tile.TileType.V, "Media/Tiles/V.png");
                list.Add(Tile.TileType.W, "Media/Tiles/W.png");
                list.Add(Tile.TileType.X, "Media/Tiles/X.png");
            }
        }

        public enum TileType { A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X };
        public enum Orientation { N, E, S, W};
        public Orientation orientation = Orientation.N;
        public Border borders = new Border();
        public TileType tileType = new TileType();
        public SourceImages sourceImages = new SourceImages();

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
                  borders.N = Border.BorderType.Grass;
                  borders.E = Border.BorderType.Grass;
                  borders.S = Border.BorderType.Road;
                  borders.N = Border.BorderType.Grass;
                  break;
               case TileType.B:
                  borders.N = Border.BorderType.Grass;
                  borders.E = Border.BorderType.Grass;
                  borders.S = Border.BorderType.Grass;
                  borders.N = Border.BorderType.Grass;
                  break;
              case TileType.C:
                  borders.N = Border.BorderType.City;
                  borders.E = Border.BorderType.City;
                  borders.S = Border.BorderType.City;
                  borders.N = Border.BorderType.City;
                  break;
              case TileType.D:
                  borders.N = Border.BorderType.City;
                  borders.E = Border.BorderType.Road;
                  borders.S = Border.BorderType.Grass;
                  borders.N = Border.BorderType.Road;
                  break;
              case TileType.E:
                  borders.N = Border.BorderType.City;
                  borders.E = Border.BorderType.Grass;
                  borders.S = Border.BorderType.Grass;
                  borders.N = Border.BorderType.Grass;
                  break;
              case TileType.F:
                  borders.N = Border.BorderType.Grass;
                  borders.E = Border.BorderType.City;
                  borders.S = Border.BorderType.Grass;
                  borders.N = Border.BorderType.City;
                  break;
              case TileType.G:
                  borders.N = Border.BorderType.Grass;
                  borders.E = Border.BorderType.City;
                  borders.S = Border.BorderType.Grass;
                  borders.N = Border.BorderType.City;
                  break;
              case TileType.H:
                  borders.N = Border.BorderType.Grass;
                  borders.E = Border.BorderType.City;
                  borders.S = Border.BorderType.Grass;
                  borders.N = Border.BorderType.City;
                  break;
              case TileType.I:
                  borders.N = Border.BorderType.City;
                  borders.E = Border.BorderType.Grass;
                  borders.S = Border.BorderType.Grass;
                  borders.N = Border.BorderType.City;
                  break;
              case TileType.J:
                  borders.N = Border.BorderType.City;
                  borders.E = Border.BorderType.Road;
                  borders.S = Border.BorderType.Road;
                  borders.N = Border.BorderType.Grass;
                  break;
              case TileType.K:
                  borders.N = Border.BorderType.City;
                  borders.E = Border.BorderType.Grass;
                  borders.S = Border.BorderType.Road;
                  borders.N = Border.BorderType.Road;
                  break;
              case TileType.L:
                  borders.N = Border.BorderType.City;
                  borders.E = Border.BorderType.Road;
                  borders.S = Border.BorderType.Road;
                  borders.N = Border.BorderType.Road;
                  break;
              case TileType.M:
                  borders.N = Border.BorderType.City;
                  borders.E = Border.BorderType.Grass;
                  borders.S = Border.BorderType.Grass;
                  borders.N = Border.BorderType.City;
                  break;
              case TileType.N:
                  borders.N = Border.BorderType.City;
                  borders.E = Border.BorderType.Grass;
                  borders.S = Border.BorderType.Grass;
                  borders.N = Border.BorderType.City;
                  break;
              case TileType.O:
                  borders.N = Border.BorderType.City;
                  borders.E = Border.BorderType.Road;
                  borders.S = Border.BorderType.Road;
                  borders.N = Border.BorderType.City;
                  break;
              case TileType.P:
                  borders.N = Border.BorderType.City;
                  borders.E = Border.BorderType.Road;
                  borders.S = Border.BorderType.Road;
                  borders.N = Border.BorderType.City;
                  break;
              case TileType.Q:
                  borders.N = Border.BorderType.City;
                  borders.E = Border.BorderType.City;
                  borders.S = Border.BorderType.Grass;
                  borders.N = Border.BorderType.City;
                  break;
              case TileType.R:
                  borders.N = Border.BorderType.City;
                  borders.E = Border.BorderType.City;
                  borders.S = Border.BorderType.Grass;
                  borders.N = Border.BorderType.City;
                  break;
              case TileType.S:
                  borders.N = Border.BorderType.City;
                  borders.E = Border.BorderType.City;
                  borders.S = Border.BorderType.Road;
                  borders.N = Border.BorderType.City;
                  break;
              case TileType.T:
                  borders.N = Border.BorderType.City;
                  borders.E = Border.BorderType.City;
                  borders.S = Border.BorderType.Road;
                  borders.N = Border.BorderType.City;
                  break;
              case TileType.U:
                  borders.N = Border.BorderType.Grass;
                  borders.E = Border.BorderType.Road;
                  borders.S = Border.BorderType.Grass;
                  borders.N = Border.BorderType.Road;
                  break;
              case TileType.V:
                  borders.N = Border.BorderType.Grass;
                  borders.E = Border.BorderType.Grass;
                  borders.S = Border.BorderType.Road;
                  borders.N = Border.BorderType.Road;
                  break;
              case TileType.W:
                  borders.N = Border.BorderType.Grass;
                  borders.E = Border.BorderType.Road;
                  borders.S = Border.BorderType.Road;
                  borders.N = Border.BorderType.Road;
                  break;
               case TileType.X:
                  borders.N = Border.BorderType.Road;
                  borders.E = Border.BorderType.Road;
                  borders.S = Border.BorderType.Road;
                  borders.N = Border.BorderType.Road;
                  break;
               default:
                  throw new Exception();
            }
        }

        public void Rotate()
        {
            if (orientation == Orientation.N)
                orientation = Orientation.E;
            else if (orientation == Orientation.E)
                orientation = Orientation.S;
            else if (orientation == Orientation.S)
                orientation = Orientation.W;
            else
                orientation = Orientation.N;

            rotation.Angle += 90;
        }
    }
}
