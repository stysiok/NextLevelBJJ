namespace NextLevelBJJ.Infrastructure.EF
{
    public class EfOptions
    {
        public string ConnectionString { get; set; }

        public bool InMemory { get; set; }

        public bool SeedData { get; set; }
    }
}