namespace XamarinForms.Core.Infrastructure
{
    public interface IMemoryService
    {
        MemoryInfo GetInfo();
    }

    public class MemoryInfo
    {
        public long FreeMemory { get; set; }
        public long MaxMemory { get; set; }
        public long TotalMemory { get; set; }

        public long UsedMemory => (TotalMemory - FreeMemory);

        public double HeapUsage()
        {
            return (double)(UsedMemory) / (double)TotalMemory;
        }

        public double Usage()
        {
            return (double)(UsedMemory) / (double)MaxMemory;
        }
    }
}
