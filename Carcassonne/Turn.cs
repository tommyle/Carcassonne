using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Carcassonne
{
    public class Turn
    {
        public enum Mode { PlaceTile, PlaceMeeple, EndTurn };
        public Tile tile;
        public Mode mode;

        public void StartTurn(Tile inTile, MainPage page)
        {
            tile = inTile;
            mode = Mode.PlaceTile;
            page.gameSurface.Children.Add(tile);

            Canvas.SetLeft(tile.rect, 10);
            Canvas.SetTop(tile.rect, 10);
        }
    }
}
