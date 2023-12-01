namespace Menekulj.Model
{
    /// <summary>
    /// 2d position
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Row (y coordinate)
        /// </summary>
        public int Row { get; private set; }
        /// <summary>
        /// Col (x coordinate)
        /// </summary>
        public int Col { get; private set; }

        /// <summary>
        /// Create a new Position
        /// </summary>
        /// <param name="row">The row (y)</param>
        /// <param name="col">The col (x)</param>
        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }

        /// <summary>
        /// Override the row and the column of the position
        /// </summary>
        /// <param name="row">New row</param>
        /// <param name="col">New column</param>
        public void SetPosition(int row, int col)
        {
            Row = row;
            Col = col;
        }

        /// <summary>
        /// Override it to match the given position's values
        /// </summary>
        /// <param name="pos">The position to copy</param>
        public void SetPosition(Position pos)
        {
            Row = pos.Row;
            Col = pos.Col;
        }

        /// <summary>
        /// Update only the column
        /// </summary>
        /// <param name="col">The new column value</param>
        public void SetCol(int col)
        {
            Col = col;
        }

        /// <summary>
        /// Update only the row
        /// </summary>
        /// <param name="row">The now row value</param>
        public void SetRow(int row)
        {
            Row = row;
        }

        /// <summary>
        /// Calculate distance between this and an another position
        /// </summary>
        /// <param name="other">The other position</param>
        /// <returns>The distance between the two positions</returns>
        public float CalcDistance(Position other)
        {
            return (float)Math.Sqrt(Math.Pow(Row - other.Row, 2) + Math.Pow(Col - other.Col, 2));
        }

        /// <summary>
        /// Calculate the distance between this and the given (row,col) position 
        /// </summary>
        /// <param name="row">The row to calculate</param>
        /// <param name="col">The column to calculate</param>
        /// <returns>The distance between the two positions</returns>
        public float DistanceTo(int row, int col)
        {
            return (float)Math.Sqrt(Math.Pow(Row - row, 2) + Math.Pow(Col - col, 2));
        }

        /// <summary>
        /// Check if it is the same as the other Position
        /// </summary>
        /// <param name="other">Other Position</param>
        /// <returns>true-if the row and col is the same  false-otherwise</returns>
        public bool Equals(Position other)
        {
            return Row == other.Row && Col == other.Col;
        }
    }
}
