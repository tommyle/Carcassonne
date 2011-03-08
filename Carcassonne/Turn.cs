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
using System.Collections.Generic;

namespace Carcassonne
{
    public class Turn
    {
        public Tile activeTile;
        public Mode mode;
        public Point originalPos = new Point();

        public void StartTurn(ref List<Tile> tiles, MainPage page)
        {
            mode = Mode.PlaceTile;
            // Deal the tile and remove it from the deck
            activeTile = tiles[0];
            tiles.RemoveAt(0);

            // Add the tile to the game surface
            page.gameSurface.Children.Add(activeTile);

            Canvas.SetLeft(activeTile.rect, 10);
            Canvas.SetTop(activeTile.rect, 10);
        }

        public bool ConfirmTilePlacement(ref List<Tile> tiles)
        {
            if (activeTile.isSlotted && tiles.Count > 0)
            {
                activeTile.slot.tile = activeTile;
                mode = Mode.PlaceMeeple;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void StartMeeplePlacement(MainPage page)
        {
            page.lights.Visibility = Visibility.Visible;

            // Store the orignal positions so we can restore them
            originalPos.X = Canvas.GetLeft(activeTile.rect);
            originalPos.Y = Canvas.GetTop(activeTile.rect);

            Canvas.SetLeft(activeTile.rect, 360);
            Canvas.SetTop(activeTile.rect, 232);
            activeTile.Grow();
        }

        public bool ConfirmMeeplePlacement(MainPage page)
        {
            page.lights.Visibility = Visibility.Collapsed;

            Canvas.SetLeft(activeTile.rect, originalPos.X);
            Canvas.SetTop(activeTile.rect, originalPos.Y);

            activeTile.Shrink();
            return true;
        }
    }

    public enum Mode { PlaceTile, PlaceMeeple, EndTurn };
}
