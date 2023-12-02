namespace Menekulj.Model.Tests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void CreatePlayerTest()
        {
            GameModel model = new GameModel(15, 13);

            Player player1 = new Player(model, 5, 7);
            Assert.IsFalse(player1.Dead);

            Assert.AreEqual(5, player1.Position.Row);
            Assert.AreEqual(7, player1.Position.Col);
            Assert.AreEqual(5, player1.PrevPosition.Row);
            Assert.AreEqual(7, player1.PrevPosition.Col);

            //Exception test

            Assert.ThrowsException<ArgumentException>(() => new Player(model, 20, 15));
            Assert.ThrowsException<ArgumentException>(() => new Player(model, 20, 1));
            Assert.ThrowsException<ArgumentException>(() => new Player(model, 1, 16));
            Assert.ThrowsException<ArgumentException>(() => new Player(model, 15, 15));

            //Json create test
            Position pos = new Position(1, 0);
            Position prevPos = new Position(0, 0);
            Direction lookDir = Direction.Down;



            Player player2 = new Player(pos, prevPos, false, lookDir);
            Assert.AreEqual(1, player2.Position.Row);
            Assert.AreEqual(0, player2.Position.Col);
            Assert.AreEqual(0, player2.PrevPosition.Row);
            Assert.AreEqual(0, player2.PrevPosition.Col);
            Assert.IsFalse(player2.Dead);
            Assert.AreEqual(Direction.Down, player2.LookingDirection);
            Assert.ThrowsException<NullReferenceException>(() => player2.Move());
            player2.SetGame(model);
            player2.Move();
            Assert.AreEqual(2, player2.Position.Row);
        }

        [TestMethod]
        public void MovePlayerTest()
        {
            GameModel model = new GameModel(10, 0);

            Player player = new Player(model, 0, 7);

            player.Move();

            Assert.AreEqual(0, player.Position.Row);
            Assert.AreEqual(8, player.Position.Col);
            Assert.AreEqual(0, player.PrevPosition.Row);
            Assert.AreEqual(7, player.PrevPosition.Col);
            player.Move();

            Assert.AreEqual(0, player.Position.Row);
            Assert.AreEqual(9, player.Position.Col);

            player.Move();

            Assert.AreEqual(0, player.Position.Row);
            Assert.AreEqual(9, player.Position.Col);

            player.SetDirection(Direction.Up);
            Assert.AreEqual(Direction.Up, player.LookingDirection);

            player.Move();

            Assert.AreEqual(0, player.Position.Row);
            Assert.AreEqual(9, player.Position.Col);
            Assert.AreEqual(0, player.PrevPosition.Row);
            Assert.AreEqual(9, player.PrevPosition.Col);

            player.SetDirection(Direction.Down);

            player.Move();

            Assert.AreEqual(1, player.Position.Row);
            Assert.AreEqual(9, player.Position.Col);
            Assert.AreEqual(0, player.PrevPosition.Row);
            Assert.AreEqual(9, player.PrevPosition.Col);

            player.Move();
            Assert.AreEqual(2, player.Position.Row);
            Assert.AreEqual(9, player.Position.Col);

            player.Die();
            Assert.ThrowsException<UnitIsDeadException>(() => player.Move());
        }

        [TestMethod]
        public void DieTest()
        {
            GameModel model = new GameModel(10, 0);

            Player player1 = new Player(model, 0, 7);

            player1.Die();
            Assert.IsTrue(player1.Dead);
        }

    }
}