using System;
using System.Linq;
using Teaminator.MissileService;

namespace Teaminator.WebApi
{
    public class UserMissileService
    {
        private static readonly Launcher Laucher = new Launcher();

        public bool AimAtUser(string username)
        {
            var user = Settings.SettingsManager.Settings.Users.FirstOrDefault(u => u.Username == username);
            if (user == null) return false;

            var mapping = Settings.SettingsManager.Settings.UserPositionMappings.FirstOrDefault(m => m.UserId == user.Id);
            if (mapping == null) return false;

            var pos = Settings.SettingsManager.Settings.Positions.FirstOrDefault(p => p.Id == mapping.PositionId);

            if(this.GetCurrentDirection().Equals(new [] {pos.X, pos.Y}))  return true;

            Laucher.Reset();
            Laucher.Aim(pos.X, pos.Y);
            Console.WriteLine("Aiming on " + username);
            return true;
        }

        public bool AttackUser(string username)
        {
            if (!AimAtUser(username)) return false;
            Laucher.Fire();
            return true;
        }

        public int[] GetCurrentDirection()
        {
            return Laucher.GetCurrentDirection();
        }

        public void Aim(int x, int y)
        {
            Laucher.Aim(x, y);
        }

        public void Aim(int pos)
        {
            
        }

        public void Fire()
        {
            Laucher.Fire();
        }

        public void Reset()
        {
            Laucher.Reset();
        }

        public void Sweep()
        {
            Laucher.Reset();
            Laucher.Aim(2800,300);
        }

        public void Nod()
        {
            if(Laucher.GetCurrentDirection() == new int[] { 0,0}) Laucher.Aim(2900,200);

            Laucher.Aim(0, 300);
            Laucher.Aim(0, -600);
            Laucher.Aim(0, 600);
            Laucher.Aim(0, -600);
            Laucher.Aim(0, 600);
            Laucher.Aim(0, -300);
        }

        public void Shake()
        {
            if (Laucher.GetCurrentDirection() == new int[] { 0, 0 }) Laucher.Aim(2900, 200);

            Laucher.Aim(300, 0);
            Laucher.Aim(-600, 0);
            Laucher.Aim(600, 0);
            Laucher.Aim(-600, 0);
            Laucher.Aim(600, 0);
            Laucher.Aim(-300, 0);
        }

    }
}
