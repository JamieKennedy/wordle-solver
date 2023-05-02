using Common.Models;

namespace Simulation.Models
{
    public class SimulationData
    {
        public List<GameState> State { get; set; }
        public int RoundCount { get; set; }

        public SimulationData()
        {
            State = new List<GameState>();
            RoundCount = 0;
        }
    }
}