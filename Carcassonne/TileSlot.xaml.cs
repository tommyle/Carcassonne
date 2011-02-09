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
	public partial class TileSlot : UserControl
	{
		public TileSlot N = null, S = null, E = null, W = null;
		public Tile tile = null;
		public int number;

		public TileSlot()
		{
			InitializeComponent();
			number = 0;
		}

		public bool hasN()
		{
			return (N != null);
		}

		public bool hasS()
		{
			return (S != null);
		}

		public bool hasE()
		{
			return (E != null);
		}

		public bool hasW()
		{
			return (W != null);
		}

		public bool hasTile()
		{
			return !(tile == null);
		}
	}
}
