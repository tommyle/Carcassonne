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
		private const int TILES_IN_A_ROW = 11;

        private Turn turn = new Turn();
		private Boolean isCheckmarkMouseCapture = false;
		private Boolean isRectMouseCapture = false;
		private Point clickPosition;
		private DateTime lastClick = new DateTime();
		private double CLICK_SPEED = 0.15;

        private const int MAX_PLAYERS = 4;
        private List<Player> players = new List<Player>();

		List<Tile> _tiles = new List<Tile>();
		List<Tile> _tilesPlayed = new List<Tile>();
		List<TileSlot> _slots = new List<TileSlot>();

		Random _randomizer = new Random();

		public MainPage()
		{
			InitializeComponent();
            CreatePlayers();
			CreateTileSlots();
			CreateTiles();
			InitializeBoard();

            turn.StartTurn(ref _tiles, this);
            ShowValidSlots();
		}

		#region init

        void CreatePlayers()
        {
            Player count = new Player(0, PortraitType.Count);
            players.Add(count);
            Player warlock = new Player(1, PortraitType.Warlock);
            players.Add(warlock);
            Player maid = new Player(2, PortraitType.Maid);
            players.Add(maid);
            Player servant = new Player(3, PortraitType.Servant);
            players.Add(servant);
        }

        void InitializeBoard()
		{
			checkmark.MouseLeftButtonDown += new MouseButtonEventHandler(checkmark_MouseLeftButtonDown);
			checkmark.MouseLeftButtonUp += new MouseButtonEventHandler(checkmark_MouseLeftButtonUp);

			// Place a D tile in the centre of the board
			foreach (Tile t in _tiles)
			{
				if (t.tileType == TileType.D)
				{
					_tilesPlayed.Add(t);
					_tiles.Remove(t);
					
					TileSlot center = FindSlotByNumber(39);

					gameSurface.Children.Add(t);

					InsertTileInSlot(center, t);
                    t.slot.tile = t;

					break;
				}
			}

            // Place the player's portrait and meeples on the board
            foreach (Player p in players)
            {
                int top = 0;
                int left = 0;

                switch (p.playerId)
                {
                    case 0:
                        left = 670;
                        top = 12;

                        Canvas.SetLeft(p.portrait, left);
                        Canvas.SetTop(p.portrait, top);
                        gameSurface.Children.Add(p.portrait);

                        left -= 60;

                        for (int i = 0; i < p.meeples.Count; i++)
                        {
                            Canvas.SetLeft(p.meeples[i], left - i * 52);
                            Canvas.SetTop(p.meeples[i], top);
                            gameSurface.Children.Add(p.meeples[i]);
                        }

                        break;
                    case 1:
                        left = 946;
                        top = 530;

                        Canvas.SetLeft(p.portrait, left);
                        Canvas.SetTop(p.portrait, top);
                        gameSurface.Children.Add(p.portrait);

                        top -= 60;

                        for (int i = 0; i < p.meeples.Count; i++)
                        {
                            Canvas.SetLeft(p.meeples[i], left + 14);
                            Canvas.SetTop(p.meeples[i], top - i * 52);
                            gameSurface.Children.Add(p.meeples[i]);
                        }

                        break;
                    case 2:
                        left = 290;
                        top = 686;

                        Canvas.SetLeft(p.portrait, left);
                        Canvas.SetTop(p.portrait, top);
                        gameSurface.Children.Add(p.portrait);

                        left += 80;

                        for (int i = 0; i < p.meeples.Count; i++)
                        {
                            Canvas.SetLeft(p.meeples[i], left + i * 52);
                            Canvas.SetTop(p.meeples[i], top + 17);
                            gameSurface.Children.Add(p.meeples[i]);
                        }

                        break;
                    default:
                        left = 2;
                        top = 150;

                        Canvas.SetLeft(p.portrait, left);
                        Canvas.SetTop(p.portrait, top);
                        gameSurface.Children.Add(p.portrait);

                        top += 80;

                        for (int i = 0; i < p.meeples.Count; i++)
                        {
                            Canvas.SetLeft(p.meeples[i], left);
                            Canvas.SetTop(p.meeples[i], top + i * 52);
                            gameSurface.Children.Add(p.meeples[i]);
                        }

                        break;
                }
            }
		}

		void CreateTiles()
		{
			Dictionary<TileType, int> tileList = new Dictionary<TileType, int>();
			tileList.Add(TileType.A, 2);
			tileList.Add(TileType.B, 4);
			tileList.Add(TileType.C, 1);
			tileList.Add(TileType.D, 4);
			tileList.Add(TileType.E, 5);
			tileList.Add(TileType.F, 2);
			tileList.Add(TileType.G, 1);
			tileList.Add(TileType.H, 3);
			tileList.Add(TileType.I, 2);
			tileList.Add(TileType.J, 3);
			tileList.Add(TileType.K, 3);
			tileList.Add(TileType.L, 3);
			tileList.Add(TileType.M, 2);
			tileList.Add(TileType.N, 3);
			tileList.Add(TileType.O, 2);
			tileList.Add(TileType.P, 3);
			tileList.Add(TileType.Q, 1);
			tileList.Add(TileType.R, 3);
			tileList.Add(TileType.S, 2);
			tileList.Add(TileType.T, 1);
			tileList.Add(TileType.U, 8);
			tileList.Add(TileType.V, 9);
			tileList.Add(TileType.W, 4);
			tileList.Add(TileType.X, 1);

			foreach (Key key in tileList.Keys)
			{
				int numTiles = tileList[(TileType)key];

				for (int i = 0; i < numTiles; i++)
				{
					Tile t = new Tile((TileType)key);


					t.MouseLeftButtonUp += new MouseButtonEventHandler(rect_MouseLeftButtonUp);
					t.MouseLeftButtonDown += new MouseButtonEventHandler(rect_MouseLeftButtonDown);
					t.MouseMove += new MouseEventHandler(rect_MouseMove);

					_tiles.Add(t);
				}
			}
		}

		void CreateTileSlots()
		{
			int k = 1;
			for (int i = 0; i < 7; i++)
			{
				for (int j = 0; j < 11; j++)
				{

					TileSlot t = new TileSlot();
					t.number = k++;

					_slots.Add(t);
					gameSurface.Children.Add(t);

					Canvas.SetLeft(t, 80 + j * 79);
					Canvas.SetTop(t, 100 + i * 79);

					t.N = (i == 0) ? null : FindSlotByNumber(t.number - TILES_IN_A_ROW);
					if (t.N != null)
						t.N.S = t;

					t.S = null;

					t.W = (j == 0) ? null : FindSlotByNumber(t.number - 1);
					if (t.W != null)
						t.W.E = t;

					t.E = null;
				}
			}
		}

		private TileSlot FindSlotByNumber(int p)
		{
			foreach (TileSlot slot in _slots)
				if (slot.number == p)
					return slot;

			return null;
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
            /* Turn Progression
             * 1. Place Tile
             * 2. Confirm Placement
             * 3. Start Meeple Placement
             * 4. Confirm Meeple Placement
             * 5. Start New Turn
             */

            if (isCheckmarkMouseCapture)
            {
                if (turn.mode.Equals(Mode.PlaceTile))
                {
                    if (turn.ConfirmTilePlacement(ref _tiles))
                    {
                        ((Rectangle)sender).ReleaseMouseCapture();
                        isCheckmarkMouseCapture = false;
                        turn.StartMeeplePlacement(this);
                    }
                }
                else if (turn.mode.Equals(Mode.PlaceMeeple))
                {
                    if (turn.ConfirmMeeplePlacement(this))
                    {
                        turn.StartTurn(ref _tiles, this);
                        ShowValidSlots();
                    }
                }
            }
		}

		#endregion

		#region tile mouse events

		void rect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			Tile tile = (Tile)sender;

			if (isRectMouseCapture && tile.Equals(turn.activeTile))
			{
                tile.ReleaseMouseCapture();
                isRectMouseCapture = false;

				TimeSpan clickSpeed = DateTime.Now - lastClick;

                if (clickSpeed.TotalSeconds < CLICK_SPEED)
                {
                    ((Tile)sender).Rotate();
                    ShowValidSlots();
                }
                else
                {
                    TileSlot slot = FindNearestSlot(tile);
                    if (slot != null && IsValidSlot(slot, tile))
                    {
                        InsertTileInSlot(slot, tile);
                    }
                    else
                    {
                        Canvas.SetLeft(tile.rect, 10);
                        Canvas.SetTop(tile.rect, 10);
                        tile.slot = null;
                    }
                }
			}
		}

		void rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Tile target = (Tile)sender;

			if (turn.mode.Equals(Mode.PlaceTile) && target.canBeMoved)
			{
				clickPosition = e.GetPosition(target.rect as UIElement);
				target.CaptureMouse();

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

					Canvas.SetZIndex(tile, 0);

					TileSlot slot = FindNearestSlot(tile);
					if(slot != null)
						HighlightTileSlot(slot);
					else
						ClearTileSlotHighlights();
				}
			}
		}

		private void HighlightTileSlot(TileSlot slot)
		{
			ClearTileSlotHighlights();
			slot.rect.Fill = new SolidColorBrush(Color.FromArgb(255, 154, 205, 50));
		}

		private void ClearTileSlotHighlights()
		{
			SolidColorBrush clearBrush = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
			foreach (TileSlot s in _slots)
				s.rect.Fill = clearBrush;
		}
		#endregion

		#region helpers

		private TileSlot FindNearestSlot(Tile tile)
		{
			double xCentre = Canvas.GetLeft(tile.rect) + tile.rect.Width / 2;
			double yCentre = Canvas.GetTop(tile.rect) + tile.rect.Height / 2;

			foreach (TileSlot slot in _slots)
			{
				// The plus/minus 1/2 accounts for the gap
				double x1Bound = Canvas.GetLeft(slot) - 1;
				double x2Bound = x1Bound + slot.rect.Width + 2;
				double y1Bound = Canvas.GetTop(slot) - 1;
				double y2Bound = y1Bound + slot.rect.Height + 2;

				if (xCentre >= x1Bound && xCentre <= x2Bound && yCentre >= y1Bound && yCentre <= y2Bound)
					return slot;
			}

			return null;
		}

		private bool IsValidSlot(TileSlot slot, Tile tile)
		{

			// flag to explore for adjacent files as we check for
			// other failure conditions
			bool adjacent = false;
			
			// if the target slot has a tile to the North
			// and if that tile has incompatible borders,
			// then reject
			if (slot.hasN() && slot.N.hasTile())
			{
				adjacent = true;
				if(slot.N.tile.borders.S != tile.borders.N)
					return false;
			}

			if(slot.hasS() && slot.S.hasTile())
			{
				adjacent = true;
				if(slot.S.tile.borders.N != tile.borders.S)
					return false;
			}

			if(slot.hasE() && slot.E.hasTile())
			{
				adjacent = true;
				if (slot.E.tile.borders.W != tile.borders.E)
					return false;
			}

			if(slot.hasW() && slot.W.hasTile())
			{
				adjacent = true;
				if(slot.W.tile.borders.E != tile.borders.W)
					return false;
			}

			// if no adjacent tiles were found, then the slot is not valid
			if(!adjacent)
				return false;
			
			return true;
		}
		
		private void InsertTileInSlot(TileSlot slot, Tile tile)
		{
			// position the tile on the slot
			// and tell the tile which slot its in
			Canvas.SetLeft(tile.rect, Canvas.GetLeft(slot) - 1);
			Canvas.SetTop(tile.rect, Canvas.GetTop(slot) - 1);
			tile.slot = slot;
		}

        private void ShowValidSlots()
        {
            foreach (TileSlot slot in _slots)
            {
                if (IsValidSlot(slot, turn.activeTile))
                {
                    slot.rect.Visibility = Visibility.Visible;
                }
                else
                {
                    slot.rect.Visibility = Visibility.Collapsed;
                }
            }
        }

		#endregion
	}
}
