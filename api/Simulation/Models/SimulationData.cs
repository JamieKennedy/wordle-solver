using Common.Models;

namespace Simulation.Models
{
    public class SimulationData
    {
        public GameState State { get; set; }
        public int RoundCount { get; set; }

        public SimulationData()
        {
            State = new GameState();
            RoundCount = 0;
        }
    }
}