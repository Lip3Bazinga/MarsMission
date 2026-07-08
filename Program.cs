using MarsMission;

// Monta o simulador com as implementações de console e inicia a execução.
var simulator = new MissionSimulator(new ConsoleInput(), new ConsoleOutput());
simulator.Run();
