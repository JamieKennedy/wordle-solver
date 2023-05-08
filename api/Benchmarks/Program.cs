using Benchmarks;
using Benchmarks.CommonBenchmarks;
using Benchmarks.SolverBenchmarks;

var gameData = BenchmarkUtilities.ConfigureGameData();
var map = Common.PreComputeHelpers.BuildPatternMap(gameData.PossibleWords);

// Calc Probability
SolverUtilitiesBenchmarks.CalcProbabilityBenchmarks(gameData);

// // Get Pattern
// UtilitiesBenchmark.GetPatternBenchmark(gameData);