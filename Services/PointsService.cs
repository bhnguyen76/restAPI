using restAPI.Models;

namespace restAPI.Services
{
    public class PointsService
    {
        private static List<Transaction> _transactions = new List<Transaction>();
        private readonly Dictionary<string, int> _balance = new Dictionary<string, int>();
        public void AddTransaction(Transaction transaction)
        {
            //Handle negative point transactions
            if (transaction.Points < 0)  
            {
                //Make sure the Payer is already in balance and has enough points
                if (!_balance.ContainsKey(transaction.Payer) || _balance[transaction.Payer] + transaction.Points < 0)
                {
                    throw new InvalidOperationException("Not enough points to deduct.");
                }
            }

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
                //break if transactions have already covered the points to spend
                if (pointsToSpend <= 0) break;

                //find minimum between points to spend and the transaction's points to figure out which one to deduct
                int pointsToDeduct = Math.Min(pointsToSpend, transaction.Points);

                _balance[transaction.Payer] -= pointsToDeduct;
                pointsToSpend -= pointsToDeduct;

                //add transaction to result
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
