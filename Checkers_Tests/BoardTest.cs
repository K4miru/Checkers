using System;
using System.Linq;
using Checkers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers_Tests
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void CaptureSequences_Pawn_Test()
        {
            Board board = new Board(5,10,1,0,0);


            board.Move(new Position(4, 0), new Position(1, 1));
            board.Move(new Position(4, 4), new Position(3, 3));

            var sequences = board.CaptureSequences(new Position(0,0));

            IMove move1 = new Move(new Position(0,0), new Position(2, 2), new Position(1, 1));
            IMove move2 = new Move(new Position(2,2), new Position(4, 4), new Position(3, 3));

            var seq1 = new Sequence();
            var seq2 = new Sequence();

            seq1.AddMove(move1);

            seq2.AddMove(move1);
            seq2.AddMove(move2);

            var isSeq1 = sequences.Contains(seq1);
            var isSeq2 = sequences.Contains(seq2);

            Assert.AreEqual(2, sequences.Count);
            Assert.AreEqual(true,isSeq1);
            Assert.AreEqual(true,isSeq2);
        }

        [TestMethod]
        public void CaptureSequences_Pawn_Test2()
        {
            Board board = new Board(8, 16, 1, 0, 0);

            board.Move(new Position(0, 0), new Position(4, 3));
            board.Move(new Position(0, 1), new Position(4, 5));
            board.Move(new Position(0, 2), new Position(2, 1));
            board.Move(new Position(0, 3), new Position(2, 3));

            board.Move(new Position(7, 0), new Position(5, 4));

            var sequences = board.CaptureSequences(new Position(5, 4));

            IMove move1 = new Move(new Position(5, 4), new Position(3, 2), new Position(4, 3));
            IMove move2 = new Move(new Position(5, 4), new Position(3, 6), new Position(4, 5));

            IMove move1_1 = new Move(new Position(3, 2), new Position(1, 0), new Position(2, 1));
            IMove move1_2 = new Move(new Position(3, 2), new Position(1, 4), new Position(2, 3));


            var seq1 = new Sequence();
            var seq2 = new Sequence();

            var seq1_1 = new Sequence();
            var seq1_2 = new Sequence();


            seq1.AddMove(move1);

            seq2.AddMove(move2);

            seq1_1.AddMove(move1);
            seq1_1.AddMove(move1_1);

            seq1_2.AddMove(move1);
            seq1_2.AddMove(move1_2);

            var isSeq1 = sequences.Contains(seq1);
            var isSeq2 = sequences.Contains(seq2);
            var isSeq1_1 = sequences.Contains(seq1_1);
            var isSeq1_2 = sequences.Contains(seq1_2);

            Assert.AreEqual(4, sequences.Count);
            Assert.AreEqual(true, isSeq1);
            Assert.AreEqual(true, isSeq2);
            Assert.AreEqual(true, isSeq1_1);
            Assert.AreEqual(true, isSeq1_2);
        }


        [TestMethod]
        public void CaptureSequences_Pawn_Test3()
        {
            Board board = new Board(8, 16, 1, 0, 0);

            board.Move(new Position(0, 0), new Position(4, 3));
            board.Move(new Position(0, 1), new Position(4, 5));
            board.Move(new Position(0, 2), new Position(6, 5));

            board.Move(new Position(7, 0), new Position(5, 4));

            var sequences = board.CaptureSequences(new Position(5, 4));

            IMove move1 = new Move(new Position(5, 4), new Position(3, 2), new Position(4, 3));
            IMove move2 = new Move(new Position(5, 4), new Position(3, 6), new Position(4, 5));


            var seq1 = new Sequence();
            var seq2 = new Sequence();

            seq1.AddMove(move1);

            seq2.AddMove(move2);

            var isSeq1 = sequences.Contains(seq1);
            var isSeq2 = sequences.Contains(seq2);

            Assert.AreEqual(2, sequences.Count);
            Assert.AreEqual(true, isSeq1);
            Assert.AreEqual(true, isSeq2);
        }

    }
}
