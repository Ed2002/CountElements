using CountElements.Server.Domain;

namespace CountElements.Server
{
    public class CountFoods
    {
        public void CalculateTotalWeight(CountData countData)
        {
            lock (countData)
            {
                double totalWeight = 0;
                for (int i = 0; i < countData.TotalScannedFoods; i++)
                {
                    totalWeight += countData.FoodWeights[i];
                }

                countData.UpdateTotalWeight(totalWeight);

                Array.Clear(countData.FoodWeights, 0, countData.FoodWeights.Length);
                countData.UpdateTotalScannedFoodsToShow(countData.TotalScannedFoodsToShow + countData.TotalScannedFoods);
                countData.UpdateTotalScannedFoods(0);
            }
        }

        public void CountElements(CountData data)
        {
            Console.WriteLine("Threding Here....");
            CountData countData = data;
            Random random = new();

            try
            {
                while (countData.CountFoods)
                {
                    lock (countData)
                    {
                        if (countData.TotalScannedFoods >= CountData.MaxFoodsToScan)
                        {
                            if (countData.IsMaxFoodsScanned())
                            {
                                CalculateTotalWeight(countData);
                            }
                        }

                        if (countData.RunBeltOne/* && random.Next(0, 2) == 1*/)
                        {
                            countData.IncrementBeltOne();
                            countData.FoodWeights[countData.TotalScannedFoods] = countData.Weight.HeavyWeight;
                            countData.IncrementTotalScannedFoods();
                        }

                        if (countData.RunBeltTwo /*&& random.Next(0, 2) == 1*/)
                        {
                            countData.IncrementBeltTwo();
                            countData.FoodWeights[countData.TotalScannedFoods] = countData.Weight.MediumWeight;
                            countData.IncrementTotalScannedFoods();
                        }

                        if (countData.RunBeltThree /*&& random.Next(0, 2) == 1*/)
                        {
                            countData.IncrementBeltThree();
                            countData.FoodWeights[countData.TotalScannedFoods] = countData.Weight.LightWeight;
                            countData.IncrementTotalScannedFoods();
                        }
                    }

                    Thread.Sleep(100); // Simula o tempo de processamento de cada item
                }
            }
            catch (Exception error)
            {
                Console.WriteLine($"Erro: {error.Message}");
            }
        }

        public string GetCountDataAsString(CountData countData) =>
        @$"
            Esteira 1 - Alimentos Escaneados: {countData.BeltOne}
            Esteira 2 - Alimentos Escaneados: {countData.BeltTwo}
            Esteira 3 - Alimentos Escaneados: {countData.BeltThree}
            Total de alimentos escaneados: {countData.TotalScannedFoods}
            Peso total escaneado: {countData.TotalWeightScannedFoods}
        ";
    }
}
