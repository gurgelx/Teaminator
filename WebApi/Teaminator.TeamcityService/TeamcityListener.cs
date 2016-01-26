using System;
using System.Linq;
using System.Threading.Tasks;

namespace Teaminator.TeamcityService
{
    public class TeamcityListener
    {

        public event EventHandler<EventArgs> BuildError;
        public event EventHandler<EventArgs> BuildSuccess;

        private readonly BuildHandler _handler;
        public TeamcityListener(string[] targetProjects)
        {
            _handler = new BuildHandler(targetProjects);
        }

        public void Begin()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    GetAndTriggerUpdates();
                    System.Threading.Thread.Sleep(20000);   
                }
            });
        }

        private void GetAndTriggerUpdates()
        {
            var updatedBuilds = _handler.GetUpdates();
            if (updatedBuilds != null)
            {
                if (BuildSuccess != null)
                    updatedBuilds.Where(b => b.status == "SUCCESS").ToList().ForEach(b => BuildSuccess.Invoke(b, null));

                if (BuildError != null)
                    updatedBuilds.Where(b => b.status == "FAILURE").ToList().ForEach(b => BuildError.Invoke(b, null));
            }
        }
    }
}
