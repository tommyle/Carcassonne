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

namespace Carcassonne
{
	public partial class MainPage : UserControl
	{
		private Turn turn = new Turn();

		private Boolean isCheckmarkMouseCapture = false;

		private Boolean isRectMouseCapture = false;
		private Point clickPosition;
		private DateTime lastClick = new DateTime();
		private double CLICK_SPEED = 0.15;

		List<Tile> _tiles = new List<Tile>();
		List<Tile> _tilesPlayed = new List<Tile>();
		List<TileSlot> _tileSlots = new List<TileSlot>();
		Tile tileInPlay;

		Random _randomizer = new Random();

		public MainPage()
		{
			InitializeComponent();
			CreateTileSlots();
			CreateTiles();
			InitializeBoard();

			// initialize game
			DealTile();
		}

		#region init

		void InitializeBoard()
		{
			checkmark.MouseLeftButtonDown += new MouseButtonEventHandler(checkmark_MouseLeftButtonDown);
			checkmark.MouseLeftButtonUp += new MouseButtonEventHandler(checkmark_MouseLeftButtonUp);

			// Place a D tile in the centre of the board
			foreach (Tile t in _tiles)
			{
				if (t.tileType == Tile.TileType.D)
				{
					_tilesPlayed.Add(t);
					_tiles.Remove(t);
					t.hasBeenPlaced = true;

					gameSurface.Children.Add(t);
					Canvas.SetLeft(t.rect, Canvas.GetLeft(_tileSlots[38]));
					Canvas.SetTop(t.rect, Canvas.GetTop(_tileSlots[38]));

					break;
				}
			}
		}

		void CreateTiles()
		{
			Dictionary<Tile.TileType, int> tileList = new Dictionary<Tile.TileType, int>();
			tileList.Add(Tile.TileType.A, 2);
			tileList.Add(Tile.TileType.B, 4);
			tileList.Add(Tile.TileType.C, 1);
			tileList.Add(Tile.TileType.D, 4);
			tileList.Add(Tile.TileType.E, 5);
			tileList.Add(Tile.TileType.F, 2);
			tileList.Add(Tile.TileType.G, 1);
			tileList.Add(Tile.TileType.H, 3);
			tileList.Add(Tile.TileType.I, 2);
			tileList.Add(Tile.TileType.J, 3);
			tileList.Add(Tile.TileType.K, 3);
			tileList.Add(Tile.TileType.L, 3);
			tileList.Add(Tile.TileType.M, 2);
			tileList.Add(Tile.TileType.N, 3);
			tileList.Add(Tile.TileType.O, 2);
			tileList.Add(Tile.TileType.P, 3);
			tileList.Add(Tile.TileType.Q, 1);
			tileList.Add(Tile.TileType.R, 3);
			tileList.Add(Tile.TileType.S, 2);
			tileList.Add(Tile.TileType.T, 1);
			tileList.Add(Tile.TileType.U, 8);
			tileList.Add(Tile.TileType.V, 9);
			tileList.Add(Tile.TileType.W, 4);
			tileList.Add(Tile.TileType.X, 1);

			foreach (Key key in tileList.Keys)
			{
				int numTiles = tileList[(Tile.TileType)key];

				for (int i = 0; i < numTiles; i++)
				{
					Tile t = new Tile((Tile.TileType)key);


					t.MouseLeftButtonUp += new MouseButtonEventHandler(rect_MouseLeftButtonUp);
					t.MouseLeftButtonDown += new MouseButtonEventHandler(rect_MouseLeftButtonDown);
					t.MouseMove += new MouseEventHandler(rect_MouseMove);

					_tiles.Add(t);
					//gameSurface.Children.Add(t);

					/*
					double x = (double)_randomizer.Next(1, (int)(this.Width));
					double y = (double)_randomizer.Next(1, (int)(this.Height));

					Canvas.SetLeft(t, 100);
					Canvas.SetTop(t, 100);
					 */
				}
			}
		}

		void CreateTileSlots()
		{
			for (int i = 0; i < 11; i++)
			{
				for (int j = 0; j < 7; j++)
				{
					TileSlot t = new TileSlot();

					_tileSlots.Add(t);
					gameSurface.Children.Add(t);

					Canvas.SetLeft(t, 80 + i * 79);
					Canvas.SetTop(t, 100 + j * 79);
				}
			}
		}

		#endregion

		#region checkmark mouse events

		void checkmark_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			((Rectangle)sender).CaptureMouse();
			isCheckmarkMouseCapture = true;
		}

		void checkmark_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (isCheckmarkMouseCapture && tileInPlay != null)
			{
				if (tileInPlay.isSlotted && _tiles.Count > 0)
				{
					((Rectangle)sender).ReleaseMouseCapture();

					tileInPlay.hasBeenPlaced = true;

					_tilesPlayed.Add(tileInPlay);
					DealTile();

					isCheckmarkMouseCapture = false;
				}
			}
		}

		#endregion

		#region tile mouse events

		void rect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			Tile tile = (Tile)sender;

			tile.ReleaseMouseCapture();
			isRectMouseCapture = false;

			if (tile.Equals(tileInPlay))
			{
				TimeSpan clickSpeed = DateTime.Now - lastClick;

				if (clickSpeed.TotalSeconds < CLICK_SPEED)
				{
					((Tile)sender).Rotate();
				}
				else
				{
					double xCentre = Canvas.GetLeft(tile.rect) + tile.rect.Width / 2;
					double yCentre = Canvas.GetTop(tile.rect) + tile.rect.Height / 2;

					foreach (TileSlot t in _tileSlots)
					{
						// The plus/minus 1/2 accounts for the gap
						double x1Bound = Canvas.GetLeft(t) - 1;
						double x2Bound = x1Bound + t.rect.Width + 2;
						double y1Bound = Canvas.GetTop(t) - 1;
						double y2Bound = y1Bound + t.rect.Height + 2;

						if (xCentre >= x1Bound && xCentre <= x2Bound && yCentre >= y1Bound && yCentre <= y2Bound)
						{
							Canvas.SetLeft(tile.rect, x1Bound);
							Canvas.SetTop(tile.rect, y1Bound);
							tile.isSlotted = true;
						}
					}

				}
			}
		}

		void rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Tile target = (Tile)sender;

			if (!target.hasBeenPlaced)
			{
				clickPosition = e.GetPosition(target.rect as UIElement);
				target.CaptureMouse();

				//Canvas.SetZIndex((Tile)sender, 99);
				//((Tile)sender).SetValue(Canvas.ZIndexProperty, 99);

				isRectMouseCapture = true;
				lastClick = DateTime.Now;
			}
		}

		void rect_MouseMove(object sender, MouseEventArgs e)
		{
			if (isRectMouseCapture)
			{
				TimeSpan clickSpeed = DateTime.Now - lastClick;
				Tile tile = (Tile)sender;

				if (clickSpeed.TotalSeconds >= CLICK_SPEED)
				{
					Canvas.SetLeft(tile.rect, e.GetPosition(this).X - clickPosition.X);
					Canvas.SetTop(tile.rect, e.GetPosition(this).Y - clickPosition.Y);

					double xCentre = Canvas.GetLeft(tile.rect) + tile.rect.Width / 2;
					double yCentre = Canvas.GetTop(tile.rect) + tile.rect.Height / 2;

					Canvas.SetZIndex(tile, 0);

					foreach (TileSlot t in _tileSlots)
					{
						double x1Bound = Canvas.GetLeft(t) - 1;
						double x2Bound = x1Bound + t.rect.Width + 2;
						double y1Bound = Canvas.GetTop(t) - 1;
						double y2Bound = y1Bound + t.rect.Height + 2;

						if (xCentre >= x1Bound && xCentre <= x2Bound && yCentre >= y1Bound && yCentre <= y2Bound)
						{
							SolidColorBrush brush = new SolidColorBrush();
							brush.Color = Color.FromArgb(255, 154, 205, 50);
							t.rect.Fill = brush;
						}
						else
						{
							SolidColorBrush brush = new SolidColorBrush();
							brush.Color = Color.FromArgb(255, 0, 0, 0);
							t.rect.Fill = brush;
						}
					}
				}
			}
		}

		#endregion

		private void DealTile()
		{
			tileInPlay = _tiles[0];
			turn.StartTurn(tileInPlay, this);
			_tiles.RemoveAt(0);
		}
	}
}
