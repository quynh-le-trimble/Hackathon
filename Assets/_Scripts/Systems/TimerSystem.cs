
using System;
using System.Threading.Tasks;

namespace Hackathon
{
    public class TimerSystem : PersistentSingleton<TimerSystem>
    {
        public event EventHandler Timer;

        public void StartTimer(int milliseconds, EventArgs eventArgs = null)
        {
            Task.Delay(milliseconds).ContinueWith(t => Timer?.Invoke(this, eventArgs ?? EventArgs.Empty));
        }
    }
}