using System;
using System.Threading.Tasks;

namespace SimpleAsync
{
    public class SimpleAsyncClass
    {
        private IDisplay _display;

        public SimpleAsyncClass(IDisplay display)
        {
            _display = display;
        }

        public async Task MyAsyncMethod()
        {
            var result = await LongOp();
            _display.DisplayText($"e. Operation completed.  Result is {result}.");
        }

        private async Task<int> LongOp()
        {
            _display.DisplayText(
                $"b. Before delay");
            //Simulate a process that takes between 2 and 4 seconds to complete.
            var r = new Random();
            await Task.Delay(r.Next(2000, 4000));
            _display.DisplayText($"d. After delay");
            return 42;
        }
    }
}