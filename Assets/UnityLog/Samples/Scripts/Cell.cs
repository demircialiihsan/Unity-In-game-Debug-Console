using UnityEngine;
using UnityEngine.UI;

namespace UnityLog.Samples
{
    public class Cell : MonoBehaviour
    {
        public Text text;

        private TicTacToeManager gameManager;

        public Vector2Int Coord { get; private set; }
        public char Value { get; private set; }

        public void Init(TicTacToeManager gameManager, Vector2Int coord)
        {
            this.gameManager = gameManager;
            Coord = coord;
        }

        public void OnClick()
        {
            gameManager.OnCellClick(this);
        }

        public void Fill(char value)
        {
            Value = value;
            text.text = value.ToString();
        }
    }

}