using System;
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
       
        public void Aim(int x, int y)
        {
            if (x < 0)
                _launcher.Left(Math.Abs(x));
            else
                _launcher.Right(x);
            if (y < 0)
                _launcher.Down(Math.Abs(y));
            else
                _launcher.Up(y);

            _currentX += x;
            _currentY += y;
        }

        public void Fire(int shots = 1)
        {
            Console.WriteLine("FIRE @ X:" + _currentX + " Y:" + _currentY);
            _launcher.Fire(shots);
        }

        public int[] GetCurrentDirection()
        {
            return new[] {_currentX, _currentY};
        }
    }
}
