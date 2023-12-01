using System.Text.Json.Serialization;
using Menekulj.Model;

namespace Menekulj.Model
{
    public class Player : Unit
    {
        /// <summary>
        /// What direction does the player looking at
        /// </summary>
        public Direction LookingDirection { get; private set; } = Direction.Right;

        /// <summary>
        /// Constructs a new player
        /// </summary>
        /// <param name="game">Reference to the game which the player is in</param>
        /// <param name="row">The starting row position of the player</param>
        /// <param name="col">The starting column position of the player</param>
        public Player(GameModel game, byte row = 0, byte col = 0) : base(game, row, col)
        {

        }

        /// <summary>
        /// Constructs a new player from the read in json 
        /// Don't forget to set the game reference after creating a player this way!
        /// </summary>
        /// <param name="Position">The current position of the player</param>
        /// <param name="PrevPosition">The previous position of the player</param>
        /// <param name="Dead">Is the player dead</param>
        /// <param name="lookingDirection">The current looking direction of the player</param>
        [JsonConstructor]
        public Player(Position Position, Position PrevPosition, bool Dead, Direction lookingDirection) : base(Position, PrevPosition, Dead)
        {
            LookingDirection = lookingDirection;
        }

        /// <summary>
        /// Sets the player's lookingdirection
        /// </summary>
        /// <param name="lookingDirection">The new lookingdirection</param>
        public void SetDirection(Direction lookingDirection)
        {
            LookingDirection = lookingDirection;
        }

        /// <summary>
        /// Move towards the player's looking direction
        /// </summary>
        public void Move()
        {
            if (Dead)
            {
                throw new UnitIsDeadException();
            }

            Move(LookingDirection);
        }

    }
}
