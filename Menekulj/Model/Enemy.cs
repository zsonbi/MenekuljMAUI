using System.Text.Json.Serialization;
using Menekulj.Model;

namespace Menekulj.Model
{
    public class Enemy : Unit
    {
        /// <summary>
        /// Constructs a new enemy
        /// </summary>
        /// <param name="game">Reference to the game which the enemy is in</param>
        /// <param name="row">The starting row position of the enemy</param>
        /// <param name="col">The starting column position of the player</param>
        public Enemy(GameModel game, byte row, byte col) : base(game, row, col)
        {
        }

        /// <summary>
        /// Constructs a new enemy from the read in json 
        /// Don't forget to set the game reference after creating a player this way!
        /// </summary>
        /// <param name="Position">The current position of the enemy</param>
        /// <param name="PrevPosition">The previous position of the enemy</param>
        /// <param name="Dead">Is the enemy dead</param>
        [JsonConstructor]
        public Enemy(Position Position, Position PrevPosition, bool Dead) : base(Position, PrevPosition, Dead)
        {

        }

        /// <summary>
        /// Calculates the direction of the next best move
        /// Based on which cell is the nearest to the player
        /// </summary>
        /// <param name="playerPos">The position of the player</param>
        /// <returns>The closest direction</returns>
        public Direction CalculateMoveDir(Position playerPos)
        {
            Direction dir = Direction.Left;
            float least = playerPos.DistanceTo(Position.Row, Position.Col - 1);

            //Calculated the top cell's distance
            float newLeast = playerPos.DistanceTo(Position.Row - 1, Position.Col);
            if (least > newLeast)
            {
                dir = Direction.Up;
                least = newLeast;
            }
            //Calculate the right cell's distance
            newLeast = playerPos.DistanceTo(Position.Row, Position.Col + 1);
            if (least > newLeast)
            {
                dir = Direction.Right;
                least = newLeast;
            }
            //Calculate the bottom cell's distance
            newLeast = playerPos.DistanceTo(Position.Row + 1, Position.Col);
            if (least > newLeast)
            {
                dir = Direction.Down;
            }

            return dir;

        }

    }
}
