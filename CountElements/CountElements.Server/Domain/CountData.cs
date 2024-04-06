namespace CountElements.Server.Domain
{
    public class CountData
    {
        private readonly object _lock = new();
        public const int MaxFoodsToScan = 1500;

        public int BeltOne { get; private set; }
        public int BeltTwo { get; private set; }
        public int BeltThree { get; private set; }
        public int TotalScannedFoods { get; private set; }
        public int TotalScannedFoodsToShow { get; private set; }
        public double TotalWeightScannedFoods { get; private set; }
        public double[] FoodWeights { get; private set; }
        public Weight Weight { get; }
        public bool CountFoods { get; private set; }
        public bool RunBeltOne { get; private set; }
        public bool RunBeltTwo { get; private set; }
        public bool RunBeltThree { get; private set; }

        public CountData()
        {
            BeltOne = 0;
            BeltTwo = 0;
            BeltThree = 0;
            TotalScannedFoods = 0;
            TotalScannedFoodsToShow = 0;
            TotalWeightScannedFoods = 0;
            FoodWeights = new double[MaxFoodsToScan];
            Weight = new Weight(5, 2, 0.5);
            CountFoods = true;
            RunBeltOne = true;
            RunBeltTwo = true;
            RunBeltThree = true;
        }

        public void IncrementBeltOne()
        {
            lock (_lock)
            {
                BeltOne++;
            }
        }

        public void IncrementBeltTwo()
        {
            lock (_lock)
            {
                BeltTwo++;
            }
        }

        public void IncrementBeltThree()
        {
            lock (_lock)
            {
                BeltThree++;
            }
        }

        public void IncrementTotalScannedFoods()
        {
            lock (_lock)
            {
                TotalScannedFoods++;
            }
        }

        public void UpdateTotalScannedFoodsToShow(int value)
        {
            TotalScannedFoodsToShow = value;
        }

        public void UpdateTotalScannedFoods(int value)
        {
            TotalScannedFoods = value;
        }

        public void UpdateTotalWeight(double weight)
        {
            lock (_lock)
            {
                TotalWeightScannedFoods += weight;
            }
        }

        public bool IsMaxFoodsScanned()
        {
            lock (_lock)
            {
                return TotalScannedFoods % MaxFoodsToScan == 0;
            }
        }

        public void StopCounting()
        {
            lock (_lock)
            {
                CountFoods = false;
            }
        }
    }
}
