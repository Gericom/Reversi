using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    public class ReversiGame
    {
        public class ReversiField
        {
            public enum FieldContent
            {
                Empty,
                Player1,
                Player2
            }

            public FieldContent Content { get; set; }
        }

        private ReversiField[,] mFields;

        public int BoardSize { get; private set; }

        public ReversiGame(int boardSize)
        {
            BoardSize = boardSize;
            mFields = new ReversiField[BoardSize, BoardSize];
        }

        public ReversiField[] GetEnclosedFields(int x, int y)
        {
            if (x < 0 || y < 0 || x >= BoardSize || y >= BoardSize)
                return new ReversiField[0];
            List<ReversiField> enclosedFields = new List<ReversiField>();

            return enclosedFields.ToArray();
        }


    }
}
