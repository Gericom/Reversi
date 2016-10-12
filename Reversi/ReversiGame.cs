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

            public FieldContent Content { get; set; } = FieldContent.Empty;
        }

        private ReversiField[,] mFields;

        public int BoardSize { get; private set; }

        public ReversiGame(int boardSize)
        {
            BoardSize = boardSize;
            mFields = new ReversiField[BoardSize, BoardSize];
        }

        public ReversiField[] GetEnclosedFields(int x, int y, ReversiField.FieldContent player)
        {
            if (x < 0 || y < 0 || x >= BoardSize || y >= BoardSize || player == ReversiField.FieldContent.Empty)
                return new ReversiField[0];
            List<ReversiField> enclosedFields = new List<ReversiField>();

            enclosedFields.AddRange(GetEnclosedFieldsInDirection(x, y, 0, -1, player));
            enclosedFields.AddRange(GetEnclosedFieldsInDirection(x, y, 1, -1, player));
            enclosedFields.AddRange(GetEnclosedFieldsInDirection(x, y, 1, 0, player));
            enclosedFields.AddRange(GetEnclosedFieldsInDirection(x, y, 1, 1, player));
            enclosedFields.AddRange(GetEnclosedFieldsInDirection(x, y, 0, 1, player));
            enclosedFields.AddRange(GetEnclosedFieldsInDirection(x, y, -1, 1, player));
            enclosedFields.AddRange(GetEnclosedFieldsInDirection(x, y, -1, 0, player));
            enclosedFields.AddRange(GetEnclosedFieldsInDirection(x, y, -1, -1, player));


            return enclosedFields.ToArray();
        }

        private ReversiField[] GetEnclosedFieldsInDirection(int x, int y, int dx, int dy, ReversiField.FieldContent player)
        {
            if ( x < 0 || y < 0 || x >= BoardSize || y >= BoardSize || player == ReversiField.FieldContent.Empty )
                return new ReversiField[0];

            List<ReversiField> enclosedFields = new List<ReversiField>();
            while ( x > 0 && y > 0 && x < BoardSize && y < BoardSize )
            {
                ReversiField field = mFields[y,x];
                if ( field.Content == player )
                    return enclosedFields.ToArray();
                else if ( field.Content == ReversiField.FieldContent.Empty )
                    break;
                enclosedFields.Add(field);
                x += dx;
                y += dy;
            
                  
            }
            return new ReversiField[0];
        }


    }
}
