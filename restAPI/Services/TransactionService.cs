using restAPI.Models;

namespace restAPI.Services
{
    public class TransactionService
    {
        private static List<Transaction> _transactions = new List<Transaction>();

        public void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        public List<SpendTransaction> SpendPoints(int pointsToSpend)
        {
            List<SpendTransaction> result = new List<SpendTransaction>();

            int totalPoints = _transactions.Sum(x => x.Points);

            if (pointsToSpend > totalPoints)
            {
                throw new InvalidOperationException("User does not have enough points.");
            }

            var sortedTransactions = _transactions.OrderBy(x => x.Timestamp).ToList();

            foreach (var transaction in sortedTransactions)
            {
                if (pointsToSpend == 0)
                    break;

                int pointsToDeduct = Math.Min(pointsToSpend, transaction.Points);

                transaction.Points -= pointsToDeduct;
                pointsToSpend -= pointsToDeduct;

                result.Add(new SpendTransaction { Payer = transaction.Payer, Points = -pointsToDeduct });

                //if (transaction.Points == 0)
                //{
                //    _transactions.Remove(transaction);
                //}
            }

            return result;
        }

        public Dictionary<string, int> GetPointsBalance()
        {
            var balance = new Dictionary<string, int>();

            foreach (var transaction in _transactions)
            {
                if (balance.ContainsKey(transaction.Payer))
                {
                    balance[transaction.Payer] += transaction.Points;
                }
                else
                {
                    balance[transaction.Payer] = transaction.Points;
                }
            }

            return balance;
        }
    }
}
