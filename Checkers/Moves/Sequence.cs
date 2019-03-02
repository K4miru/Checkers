using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Sequence : ICloneable
    {
        private List<IMove> moves;

        public List<Position> Captures => moves.Select(s => s.Capture).ToList();

        public Position From => moves.First().From;

        public Position To => moves.Last().To;

        public Sequence()
        {
            moves = new List<IMove>();
        }

        public void AddMove(IMove move) => moves.Add(move);

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Sequence seq = (Sequence)obj;
            return seq.From.Equals(From)
                && seq.To.Equals(To)
                && seq.Captures.All(Captures.Contains);
        }

        public object Clone()
        {
            var seq = new Sequence();
            foreach (var m in moves)
                seq.AddMove((IMove)m.Clone());

            return seq;
        }
    }
}
