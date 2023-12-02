
namespace Menekulj.Model.Tests
{
    [TestClass]
    public class PositionTests
    {

        [TestMethod]
        public void CreatePositionTest()
        {
            Position position1 = new Position(0, 0);

            Assert.AreEqual(0, position1.Row);
            Assert.AreEqual(0, position1.Col);

            Position position2 = new Position(0, 0);

            Assert.IsTrue(position1.Equals(position2));

            Position position3 = new Position(-2, 0);

            Assert.AreEqual(-2, position3.Row);

        }

        [TestMethod]
        public void PositionModifyTest()
        {
            Position position1 = new Position(0, 0);
            position1.SetRow(10);
            Assert.AreEqual(10, position1.Row);
            Assert.AreEqual(0, position1.Col);

            position1.SetPosition(11, 11);
            Assert.AreEqual(11, position1.Row);
            Assert.AreEqual(11, position1.Col);

            Position position2 = new Position(2, 2);

            position2.SetCol(16);
            Assert.AreEqual(2, position2.Row);
            Assert.AreEqual(16, position2.Col);

            position1.SetPosition(position2);
            Assert.AreEqual(2, position1.Row);
            Assert.AreEqual(16, position1.Col);
        }

        [TestMethod]
        public void DistanceTest()
        {
            Position position1 = new Position(0, 0);

            Position position2 = new Position(0, 5);

            Position position3 = new Position(0, 4);

            Assert.IsTrue(position1.CalcDistance(position2) <= 5.1);
            Assert.IsTrue(position1.CalcDistance(position3) >= 3.9);

            Assert.IsTrue(position1.DistanceTo(10, 0) >= 9.9);
            Assert.IsTrue(position1.DistanceTo(11, 0) <= 11.1);



        }


    }
}
