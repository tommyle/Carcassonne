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
    public class Player
    {
        public string name;
        public int points;
        public List<int> meeple = new List<int>();

        public Player(string inName)
        {
            name = inName;
            points = 0;
        }
    }
}
