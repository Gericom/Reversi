﻿using System;
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

            public int X { get; private set; }
            public int Y { get; private set; }

            public ReversiField(int x, int y)
            {
                X = x;
                Y = y;
            }

            public void Reverse()
            {
                if (Content == FieldContent.Empty)
                    return;
                if (Content == FieldContent.Player1)
                    Content = FieldContent.Player2;
                else
                    Content = FieldContent.Player1;
            }

            //for debugging
            public override string ToString()
            {
                return "(" + X + ", " + Y + ") = " + Content;
            }
        }

        private ReversiField[,] mFields;

        public int BoardSize { get; private set; }

        public ReversiGame(int boardSize)
        {
            //force multiple of 2
            BoardSize = (boardSize + 1) & ~1;
            mFields = new ReversiField[BoardSize, BoardSize];
            for (int y = 0; y < BoardSize; y++)
            {
                for (int x = 0; x < BoardSize; x++)
                {
                    mFields[x, y] = new ReversiField(x, y);
                }
            }
            //setup the default board
            mFields[BoardSize / 2 - 1, BoardSize / 2 - 1].Content = ReversiField.FieldContent.Player1;
            mFields[BoardSize / 2, BoardSize / 2 - 1].Content = ReversiField.FieldContent.Player2;
            mFields[BoardSize / 2 - 1, BoardSize / 2].Content = ReversiField.FieldContent.Player2;
            mFields[BoardSize / 2, BoardSize / 2].Content = ReversiField.FieldContent.Player1;
        }

        public ReversiField[] GetEnclosedFields(int x, int y, ReversiField.FieldContent player)
        {
            if (x < 0 || y < 0 || x >= BoardSize || y >= BoardSize ||
                player == ReversiField.FieldContent.Empty || mFields[x, y].Content != ReversiField.FieldContent.Empty)
                return new ReversiField[0];
            //call the GetEnclosedFieldsInDirection method 8 times, for each possible direction once
            //and add the resulting fields to the list
            List<ReversiField> enclosedFields = new List<ReversiField>();
            enclosedFields.AddRange(GetEnclosedFieldsInDirection(x, y, 0, -1, player));
            enclosedFields.AddRange(GetEnclosedFieldsInDirection(x, y, 1, -1, player));
            enclosedFields.AddRange(GetEnclosedFieldsInDirection(x, y, 1, 0, player));
            enclosedFields.AddRange(GetEnclosedFieldsInDirection(x, y, 1, 1, player));
            enclosedFields.AddRange(GetEnclosedFieldsInDirection(x, y, 0, 1, player));
            enclosedFields.AddRange(GetEnclosedFieldsInDirection(x, y, -1, 1, player));
            enclosedFields.AddRange(GetEnclosedFieldsInDirection(x, y, -1, 0, player));
            enclosedFields.AddRange(GetEnclosedFieldsInDirection(x, y, -1, -1, player));
            //return the list as array
            return enclosedFields.ToArray();
        }

        private ReversiField[] GetEnclosedFieldsInDirection(int x, int y, int dx, int dy, ReversiField.FieldContent player)
        {
            if (x < 0 || y < 0 || x >= BoardSize || y >= BoardSize || player == ReversiField.FieldContent.Empty)
                return new ReversiField[0];

            List<ReversiField> enclosedFields = new List<ReversiField>();
            //skip one field => the field where your color stone will be put
            x += dx;
            y += dy;
            //walk through the fields in the given direction, there are 3 cases possible
            // - Field containing a stone of your own color
            //      => return the contents of the list
            // - Field containing a stone of the other player
            //      => add the field to the list
            // - Empty field or outside the bounds of the board
            //      => return an empty list
            while (x >= 0 && y >= 0 && x < BoardSize && y < BoardSize)
            {
                ReversiField field = mFields[x, y];
                if (field.Content == player)
                    return enclosedFields.ToArray();
                else if (field.Content == ReversiField.FieldContent.Empty)
                    break;
                enclosedFields.Add(field);
                x += dx;
                y += dy;
            }
            return new ReversiField[0];
        }

        public int GetNrStonesForPlayer(ReversiField.FieldContent player)
        {
            int count = 0;
            for (int y = 0; y < BoardSize; y++)
            {
                for (int x = 0; x < BoardSize; x++)
                {
                    if (mFields[x, y].Content == player)
                        count++;
                }
            }
            return count;
        }

        //indexer to get a field by it's x and y coords
        public ReversiField this[int x, int y]
        {
            get { return mFields[x, y]; }
        }
    }
}
