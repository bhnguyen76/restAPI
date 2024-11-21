using restAPI.Models;

namespace restAPI.Services
{
    public class PointsService
    {
        private static List<Transaction> _transactions = new List<Transaction>();
        private readonly Dictionary<string, int> _balance = new Dictionary<string, int>();
        public void AddTransaction(Transaction transaction)
        {
            //Add transaction to the transactions list
            _transactions.Add(transaction);

            //add the transaction's points to the balance dict
            if (_balance.ContainsKey(transaction.Payer))
            {
                _balance[transaction.Payer] += transaction.Points;
            }
            else
            {
                _balance[transaction.Payer] = transaction.Points;
            }
        }

        public List<SpendTransaction> SpendPoints(int points)
        {
            //check if the User has enough points. If not, throw error 
            if (points > _balance.Values.Sum())
            {
                throw new InvalidOperationException("User does not have enough points.");
            }

            var result = new List<SpendTransaction>();
            var pointsToSpend = points;

            //sort the transactions by their Timestamps with the oldest coming first
            var sortedTransactions = _transactions.OrderBy(x => x.Timestamp).ToList();

            foreach (var transaction in sortedTransactions)
            {
                if (pointsToSpend <= 0) break;

                int pointsToDeduct = Math.Min(pointsToSpend, transaction.Points);

                _balance[transaction.Payer] -= pointsToDeduct;
                pointsToSpend -= pointsToDeduct;

                result.Add(new SpendTransaction { Payer = transaction.Payer, Points = -pointsToDeduct });
            }

            return result;
        }

        public Dictionary<string, int> GetPointsBalance()
        {
            return _balance;
        }
    }
}
