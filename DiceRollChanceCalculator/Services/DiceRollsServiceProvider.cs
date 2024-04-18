namespace DiceRollChanceCalculator.Services
{
    class DiceRollsServiceProvider : IDiceRollsServiceProvider
    {
        private readonly DiceRollsSimulationService _simulationService;
        private readonly DiceRollsCalculationService _calculationService;

        public DiceRollsServiceProvider(DiceRollsSimulationService simulationService, DiceRollsCalculationService calculationService)
        {
            _simulationService = simulationService;
            _calculationService = calculationService;
        }

        public IDiceRollsService GetService(bool isSimulation)
        {
            return isSimulation ? _simulationService : _calculationService;
        }
    }
}
