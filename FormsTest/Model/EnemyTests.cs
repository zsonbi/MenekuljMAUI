
namespace Menekulj.Model.Tests
{
    [TestClass]
    public class EnemyTests
    {
        [TestMethod]
        public void CreateEnemy()
        {
            GameModel model = new GameModel(15, 13);

            Enemy enemy1 = new Enemy(model, 5, 7);
            Assert.IsFalse(enemy1.Dead);

            Assert.AreEqual(5, enemy1.Position.Row);
            Assert.AreEqual(7, enemy1.Position.Col);
            Assert.AreEqual(5, enemy1.PrevPosition.Row);
            Assert.AreEqual(7, enemy1.PrevPosition.Col);

            //Exception test

            Assert.ThrowsException<ArgumentException>(() => new Enemy(model, 20, 15));
            Assert.ThrowsException<ArgumentException>(() => new Enemy(model, 20, 1));
            Assert.ThrowsException<ArgumentException>(() => new Enemy(model, 1, 16));
            Assert.ThrowsException<ArgumentException>(() => new Enemy(model, 15, 15));

            //Json create test
            Position pos = new Position(1, 0);
            Position prevPos = new Position(0, 0);



            Enemy enemy2 = new Enemy(pos, prevPos, false);
            Assert.AreEqual(1, enemy2.Position.Row);
            Assert.AreEqual(0, enemy2.Position.Col);
            Assert.AreEqual(0, enemy2.PrevPosition.Row);
            Assert.AreEqual(0, enemy2.PrevPosition.Col);
            Assert.IsFalse(enemy2.Dead);
            Assert.ThrowsException<NullReferenceException>(() => enemy2.Move(Direction.Down));
            enemy2.SetGame(model);
            enemy2.Move(Direction.Down);
            Assert.AreEqual(2, enemy2.Position.Row);
        }

        [TestMethod]
        public void EnemyCalculateMoveDirTest()
        {
            GameModel game = new GameModel(15, 0);
            Player player1 = new Player(game, 0, 0);
            Player player2 = new Player(game, 14, 14);

            Enemy enemy1 = new Enemy(game, 14, 0);
            Enemy enemy2 = new Enemy(game, 0, 14);
            Enemy enemy3 = new Enemy(game, 0, 0);
            Enemy enemy4 = new Enemy(game, 0, 14);

            Assert.AreEqual(Direction.Up, enemy1.CalculateMoveDir(player1.Position));
            Assert.AreEqual(Direction.Left, enemy2.CalculateMoveDir(player1.Position));
            Assert.AreEqual(Direction.Right, enemy3.CalculateMoveDir(player2.Position));
            Assert.AreEqual(Direction.Down, enemy4.CalculateMoveDir(player2.Position));

        }

        [TestMethod]
        public void MoveEnemyTest()
        {
            GameModel model = new GameModel(10, 0);

            Enemy enemy = new Enemy(model, 0, 7);

            enemy.Move(Direction.Right);

            Assert.AreEqual(0, enemy.Position.Row);
            Assert.AreEqual(8, enemy.Position.Col);
            Assert.AreEqual(0, enemy.PrevPosition.Row);
            Assert.AreEqual(7, enemy.PrevPosition.Col);
            enemy.Move(Direction.Right);

            Assert.AreEqual(0, enemy.Position.Row);
            Assert.AreEqual(9, enemy.Position.Col);

            enemy.Move(Direction.Right);

            Assert.AreEqual(0, enemy.Position.Row);
            Assert.AreEqual(9, enemy.Position.Col);



            enemy.Move(Direction.Up);

            Assert.AreEqual(0, enemy.Position.Row);
            Assert.AreEqual(9, enemy.Position.Col);
            Assert.AreEqual(0, enemy.PrevPosition.Row);
            Assert.AreEqual(9, enemy.PrevPosition.Col);


            enemy.Move(Direction.Down);

            Assert.AreEqual(1, enemy.Position.Row);
            Assert.AreEqual(9, enemy.Position.Col);
            Assert.AreEqual(0, enemy.PrevPosition.Row);
            Assert.AreEqual(9, enemy.PrevPosition.Col);

            enemy.Move(Direction.Down);
            Assert.AreEqual(2, enemy.Position.Row);
            Assert.AreEqual(9, enemy.Position.Col);

            enemy.Die();
            Assert.ThrowsException<UnitIsDeadException>(() => enemy.Move(Direction.Down));
        }

        [TestMethod]
        public void DieTest()
        {
            GameModel model = new GameModel(10, 0);

            Player enemy = new Player(model, 0, 7);

            enemy.Die();
            Assert.IsTrue(enemy.Dead);
        }

    }
}
