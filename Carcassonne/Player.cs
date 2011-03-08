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
        public int playerId;
        public int points;
        public const int MAX_MEEPLES = 7;
        public Portrait portrait;
        public List<Meeple> meeples = new List<Meeple>();

        public Player(int inPlayerId, PortraitType portraitType)
        {
            playerId = inPlayerId;
            portrait = new Portrait(portraitType);
            points = 0;
            CreateMeeples();
            SetOrientation();
        }

        void CreateMeeples()
        {
            MeepleType[] colours = { MeepleType.Green, MeepleType.Blue, MeepleType.Red, MeepleType.Yellow };

            for (int i = 0; i < MAX_MEEPLES; i++)
            {
                Meeple m = new Meeple(colours[playerId]);
                meeples.Add(m);
            }
        }

        void SetOrientation()
        {
            int angle;

            switch (playerId)
            {
                case 0:
                    angle = 180;
                    break;
                case 1:
                    angle = 270;
                    break;
                case 2:
                    angle = 0;
                    break;
                default:
                    angle = 90;
                    break;
            }

            portrait.Angle(angle);
            foreach (Meeple m in meeples)
            {
                m.Angle(angle);
            }
        }
    }
}
