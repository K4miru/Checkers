using System;
using System.Linq;
using System.Collections.Generic;
using Checkers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers_Tests
{
    [TestClass]
    public class PawnTest
    {
        [TestMethod]
        public void Pawn_RegularMoves_Test()
        {
            IPiece pawn = new Pawn(Player.BLACK);

            var moves = new List<IMove>();
            var pawnPos = new Position(5, 5);
            moves.Add(new Move(pawnPos, pawnPos + new Position(-1, -1)));
            moves.Add(new Move(pawnPos, pawnPos + new Position(-1, 0)));
            moves.Add(new Move(pawnPos, pawnPos + new Position(-1, 1)));
            moves.Add(new Move(pawnPos, pawnPos + new Position(0, -1)));
            moves.Add(new Move(pawnPos, pawnPos + new Position(0, 0)));
            moves.Add(new Move(pawnPos, pawnPos + new Position(0, 1)));
            moves.Add(new Move(pawnPos, pawnPos + new Position(1, -1)));
            moves.Add(new Move(pawnPos, pawnPos + new Position(1, 0)));
            moves.Add(new Move(pawnPos, pawnPos + new Position(1, 1)));


            var pawnMoves = pawn.RegularMoves(pawnPos);
            bool allSame = moves.All(pawnMoves.Contains);
            Assert.AreEqual(true, allSame);
        }

        [TestMethod]
        public void Pawn_CaptureMoves_Test()
        {
            IPiece pawn = new Pawn(Player.BLACK);

            var moves = new List<IMove>();
            var pawnPos = new Position(5, 5);
            moves.Add(new Move(pawnPos, pawnPos + new Position(-2, -2), pawnPos + new Position(-1, -1)));
            moves.Add(new Move(pawnPos, pawnPos + new Position(-2, 0), pawnPos + new Position(-1, 0)));
            moves.Add(new Move(pawnPos, pawnPos + new Position(-2, 2), pawnPos + new Position(-1, 1)));
            moves.Add(new Move(pawnPos, pawnPos + new Position(0, -2), pawnPos + new Position(0, -1)));
            moves.Add(new Move(pawnPos, pawnPos + new Position(0, 0), pawnPos + new Position(0, 0)));
            moves.Add(new Move(pawnPos, pawnPos + new Position(0, 2), pawnPos + new Position(0, 1)));
            moves.Add(new Move(pawnPos, pawnPos + new Position(2, -2), pawnPos + new Position(1, -1)));
            moves.Add(new Move(pawnPos, pawnPos + new Position(2, 0), pawnPos + new Position(1, 0)));
            moves.Add(new Move(pawnPos, pawnPos + new Position(2, 2), pawnPos + new Position(1, 1)));

            bool allSame = moves.All(pawn.CaptureMoves(pawnPos).Contains);
            Assert.AreEqual(true, allSame);
        }
    }
}
