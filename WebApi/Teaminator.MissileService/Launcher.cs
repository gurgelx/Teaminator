using System.Reflection;
using MissileSharp;

namespace Teaminator.MissileService
{
    public class Launcher
    {
        private readonly CommandCenter _launcher;
        private int _currentX = 0;
        private int _currentY = 0;
        public Launcher()
        {
            _launcher = new CommandCenter(new ThunderMissileLauncher());
        }

        public void Reset()
        {
            _launcher.Reset();
            _currentX = 0;
            _currentY = 0;
        }

        public void Aim(int x, int y, bool resetBeforeAim = true)
        {
            if(resetBeforeAim)
                Reset();
            _launcher.Right(x);
            _launcher.Up(y);
            _currentX += x;
            _currentY += y;
        }

        public void Fire(int shots = 1)
        {
            _launcher.Fire(shots);
        }

        public int[] GetCurrentDirection()
        {
            return new[] {_currentX, _currentY};
        }
    }
}
