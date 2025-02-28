﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Tetric
{
    public class Game
    {
        private readonly Canvas _gameCanvas;
        private Tetromino _currentTetromino;
        private readonly List<Rectangle> _placedBlocks;
        private const int Rows = 20;
        private const int Columns = 10;
        private const int BlockSize = 30;
        private readonly int[,] _grid;

        public int Score { get; set; }

        public Game(Canvas gameCanvas)
        {
            _gameCanvas = gameCanvas;
            _grid = new int[Columns, Rows];
            _placedBlocks = new List<Rectangle>();
            _currentTetromino = GenarateNewTetromino();
        }

        public void StartNewGame()
        {
            _gameCanvas.Children.Clear();
            _placedBlocks.Clear();
            Array.Clear(_grid, 0, _grid.Length);
            Score = 0;
            _currentTetromino = GenarateNewTetromino();
            DrawTetromino(_currentTetromino);
        }

        private Tetromino GenarateNewTetromino()
        {
            Random random = new Random();
            TetromineType type = (TetromineType)random.Next(0, 7);
            return new Tetromino(type, Columns / 2, 0);
        }

        private void DrawTetromino(Tetromino tetromino)
        {
            foreach (var block in tetromino.Blocks)
            {
                Rectangle rect = new Rectangle
                {
                    Width = BlockSize,
                    Height = BlockSize,
                    Fill = tetromino.Color
                };
                Canvas.SetLeft(rect, (tetromino.X + block.X) * BlockSize);
                Canvas.SetTop(rect, (tetromino.Y + block.Y) * BlockSize);
                _gameCanvas.Children.Add(rect);
            }
        }

        private void DrawPlacedBlocks()
        {
            foreach (var rect in _placedBlocks)
            {
                _gameCanvas.Children.Add(rect);
            }
        }

        public void MoveLeft()
        {
            MoveTetromino(-1, 0);
        }

        public void MoveRight()
        {
            MoveTetromino(1, 0);
        }

        public void MoveDown()
        {
            if (!MoveTetromino(0, 1))
            {
                PlaceTetromino();
                CheckForCompletedLines();
                _currentTetromino = GenarateNewTetromino();
                if (!CanMove(_currentTetromino, 0, 0))
                {
                    MessageBox.Show("Game Over");
                    StartNewGame();
                }
            }
        }


        public void Rotate()
        {
            _currentTetromino.Rotate();
            if (!CanMove(_currentTetromino, 0, 0))
            {
                _currentTetromino.Rotate();
                _currentTetromino.Rotate();
                _currentTetromino.Rotate();
            }
            RedrawGame();
        }

        private bool MoveTetromino(int offsetX, int offsetY)
        {
            if (CanMove(_currentTetromino, offsetX, offsetY))
            {
                _currentTetromino.X += offsetX;
                _currentTetromino.Y += offsetY;
                RedrawGame();
                return true;
            }
            return false;
        }

        private bool CanMove(Tetromino tetromino, int offsetX, int offsetY)
        {
            foreach (var block in tetromino.Blocks)
            {
                int newX = tetromino.X + (int)block.X + offsetX;
                int newY = tetromino.Y + (int)block.Y + offsetY;

                if (newX < 0 || newY < 0 || newX >= Columns || newY >= Rows)
                {
                    return false;
                }

                if (_grid[newX, newY] != 0) { return false; }
            }
            return true;
        }

        private void PlaceTetromino()
        {
            foreach (var block in _currentTetromino.Blocks)
            {
                int X = _currentTetromino.X + (int)block.X;
                int Y = _currentTetromino.Y + (int)block.Y;
                _grid[X, Y] = 1;

                Rectangle rect = new Rectangle
                {
                    Width = BlockSize,
                    Height = BlockSize,
                    Fill = _currentTetromino.Color
                };

                Canvas.SetLeft(rect, X * BlockSize);
                Canvas.SetTop(rect, Y * BlockSize);
                _placedBlocks.Add(rect);
            }
        }

        private void RedrawGame()
        {
            _gameCanvas.Children.Clear();
            DrawPlacedBlocks();
            DrawTetromino(_currentTetromino);
        }


        public void CheckForCompletedLines()
        {
            for (int y = 0; y < Rows; y++)
            {
                bool isComplete = true;
                for (int x = 0; x < Columns; x++)
                {
                    if (_grid[x, y] == 0)
                    {
                        isComplete = false;
                        break;
                    }
                }
                if (isComplete)
                {
                    RemoveLine(y);
                    Score++;
                    UpdateScore();
                    
                }
            }
        }

        private void RemoveLine(int line)
        {
            for (int y = line; y > 0; y--)
            {
                for (int x = 0; x < Columns; x++)
                {
                    _grid[x, y] = _grid[x, y - 1];
                }
            }

            foreach (var rect in _placedBlocks.Where(r => Canvas.GetTop(r) == line * BlockSize).ToList())
            {
                _gameCanvas.Children.Remove(rect);
                _placedBlocks.Remove(rect);
            }

            foreach (var rect in _placedBlocks.Where(r => Canvas.GetTop(r) < line * BlockSize))
            {
                Canvas.SetTop(rect, Canvas.GetTop(rect) + BlockSize);
            }
        }

        public void UpdateScore()
        {

        }

    }
}