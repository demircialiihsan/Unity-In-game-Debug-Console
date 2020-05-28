using UnityEngine;
using UnityLog;

namespace UnityLog.Samples
{
    public class TicTacToeManager : MonoBehaviour
    {
        public const int boardSize = 3;
        public Cell cellPrefab;
        public Transform cellsParent;

        private const char x = 'x';
        private const char o = 'o';
        private const char empty = ' ';

        private Cell[,] cells;
        private char turn = x;
        private int round;

        void Start()
        {
            cells = new Cell[boardSize, boardSize];

            for (int y = 0; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    Cell cell = Instantiate(cellPrefab, cellsParent);
                    cells[x, y] = cell;
                    cell.Init(this, new Vector2Int(x, y));
                }
            }

            ClearBoard();
        }

        public void ClearBoard()
        {
            for (int y = 0; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    cells[x, y].Fill(empty);
                }
            }
        }

        public void OnCellClick(Cell cell)
        {
            if (cell.Value == empty)
            {
                cell.Fill(turn);
                PrintGameStatus();

                if (IsGameOver(cell.Coord))
                {
                    Log.DebugLog(string.Format(">{0} HAS WON!\n\n", turn));
                    Restart();
                }
                else
                {
                    round++;
                    if (round == boardSize * boardSize)
                    {
                        Log.DebugLog(">It's a draw!\n\n");
                        Restart();
                    }
                    else
                    {
                        turn = (turn == x) ? o : x; //change turn
                        Log.DebugLog(string.Format(">waiting for {0}...\n\n", turn));
                    }
                }
            }
        }

        void Restart()
        {
            ClearBoard();
            turn = x;
            round = 0;
        }

        bool IsGameOver(Vector2Int lastCoord)
        {
            //check row
            for (int x = 0; x < boardSize; x++)
            {
                if (cells[x, lastCoord.y].Value != turn)
                    break;
                else if (x == boardSize - 1)
                    return true;
            }
            //check column
            for (int y = 0; y < boardSize; y++)
            {
                if (cells[lastCoord.x, y].Value != turn)
                    break;
                else if (y == boardSize - 1)
                    return true;
            }
            //check diagonal
            if (lastCoord.x == lastCoord.y)
            {
                for (int i = 0; i < boardSize; i++)
                {
                    if (cells[i, i].Value != turn)
                        break;
                    else if (i == boardSize - 1)
                        return true;
                }
            }
            //chack other diagonal
            if (lastCoord.x + lastCoord.y == (boardSize - 1))
            {
                for (int i = 0; i < boardSize; i++)
                {
                    if (cells[(boardSize - 1) - i, i].Value != turn)
                        break;
                    else if (i == boardSize - 1)
                        return true;
                }
            }

            return false;
        }

        void PrintGameStatus()
        {
            Log.DebugLog(string.Format(">{0} has played.\n", turn));
            PrintBoard();
        }

        void PrintBoard()
        {
            string log = string.Empty;
            for (int y = 0; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    log += ((cells[x, y].Value == empty ? '.' : cells[x, y].Value) + " "); //"." for placeholder
                }
                log += "\n";
            }
            Log.DebugLog(log);
        }
    }
}
