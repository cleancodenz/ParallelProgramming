

using System.Threading.Tasks;

namespace AsyncClasses
{
    public static class MyClass
    {
        public static async Task<int> Divide(int numerator, int denominator)
        {
            // Work for a while...
            await Task.Delay(10); // (Use TaskEx.Delay on VS2010)

            // Return the result
            return numerator / denominator;
        }
    }
}
