using System.Windows.Media;
using System.Windows;
using System.Collections.Generic;

namespace Tetric
{
    public enum TetromineType
    {
        I, O, T, S, Z, J, L
    }

    public class Tetromino
    {
        public TetromineType Type { get; private set; }
        public int X { get; set; }
        public int Y { get; set; }
        public List<Point> Blocks { get; private set; }
        public Brush Color { get; private set; }

        public Tetromino(TetromineType type, int x, int y)
        {
            Type = type;
            X = x;
            Y = y;
            InitializeBlocks();
        }

        private void InitializeBlocks()
        {
            Blocks = new List<Point>();
            switch (Type)
            {
                case TetromineType.I:
                    Blocks.Add(new Point(0, 0));
                    Blocks.Add(new Point(1, 0));
                    Blocks.Add(new Point(2, 0));
                    Blocks.Add(new Point(3, 0));
                    Color = Brushes.Cyan;
                    break;
                case TetromineType.O:
                    Blocks.Add(new Point(0, 0));
                    Blocks.Add(new Point(1, 0));
                    Blocks.Add(new Point(0, 1));
                    Blocks.Add(new Point(1, 1));
                    Color = Brushes.Yellow;
                    break;
                case TetromineType.T:
                    Blocks.Add(new Point(1, 0));
                    Blocks.Add(new Point(0, 1));
                    Blocks.Add(new Point(1, 1));
                    Blocks.Add(new Point(2, 1));
                    Color = Brushes.Purple;
                    break;
                case TetromineType.S:
                    Blocks.Add(new Point(1, 0));
                    Blocks.Add(new Point(2, 0));
                    Blocks.Add(new Point(0, 1));
                    Blocks.Add(new Point(1, 1));
                    Color = Brushes.Green;
                    break;
                case TetromineType.Z:
                    Blocks.Add(new Point(0, 0));
                    Blocks.Add(new Point(1, 0));
                    Blocks.Add(new Point(1, 1));
                    Blocks.Add(new Point(2, 1));
                    Color = Brushes.Red;
                    break;
                case TetromineType.J:
                    Blocks.Add(new Point(0, 0));
                    Blocks.Add(new Point(0, 1));
                    Blocks.Add(new Point(1, 1));
                    Blocks.Add(new Point(2, 1));
                    Color = Brushes.Blue;
                    break;
                case TetromineType.L:
                    Blocks.Add(new Point(2, 0));
                    Blocks.Add(new Point(0, 1));
                    Blocks.Add(new Point(1, 1));
                    Blocks.Add(new Point(2, 1));
                    Color = Brushes.Orange;
                    break;
            }
        }

        public void Rotate()
        {
            for (int i = 0; i < Blocks.Count; i++)
            {
                double x = Blocks[i].X;
                Blocks[i] = new Point(Blocks[i].Y, -x);
            }
        }
    }
}