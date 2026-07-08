namespace MarsMission;

// Lê o campo, processa um veículo de cada vez e no fim mostra o resumo.
// Depende só das interfaces de entrada/saída.
public sealed class MissionSimulator
{
    private readonly IUserInput _input;
    private readonly IVisualOutput _output;

    public MissionSimulator(IUserInput input, IVisualOutput output)
    {
        _input = input;
        _output = output;
    }

    public void Run()
    {
        _output.ShowHeader();

        var field = _input.ReadField();
        var vehicles = new List<Vehicle>();

        int vehicleNumber = 1;
        do
        {
            ProcessVehicle(field, vehicles, vehicleNumber);
            vehicleNumber++;
        }
        while (_input.WantsToAddVehicle());

        _output.ShowFinalResult(vehicles);
    }

    private void ProcessVehicle(LandingField field, List<Vehicle> vehicles, int vehicleNumber)
    {
        var startPosition = _input.ReadStartingPosition(field, vehicleNumber);
        var commands = _input.ReadCommands(vehicleNumber);

        var vehicle = new Vehicle(startPosition, field);
        vehicles.Add(vehicle);

        // Mostra o grid antes de mover e depois de mover, para poder comparar.
        _output.ShowMessage($"--- Campo ANTES da movimentação do veículo {vehicleNumber} ---");
        _output.DrawField(field, vehicles);

        vehicle.Execute(commands);

        _output.ShowMessage($"--- Campo DEPOIS da movimentação do veículo {vehicleNumber} ---");
        _output.DrawField(field, vehicles);

        _output.ShowMessage($"Posição final do veículo {vehicleNumber}: {vehicle.Position}");
        _output.ShowMessage("");
    }
}
