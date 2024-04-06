namespace CountElements.Server.Domain
{
    public class Weight(double lightWeight, double mediumWeight, double heavyWeight)
    {
        public double LightWeight { get; set; } = lightWeight;
        public double MediumWeight { get; set; } = mediumWeight;
        public double HeavyWeight { get; set; } = heavyWeight;
    }
}
