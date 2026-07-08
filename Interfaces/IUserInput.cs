namespace MarsMission;

// Contrato de leitura dos dados informados pelo usuário no terminal.
public interface IUserInput
{
    LandingField ReadField();
    Position ReadStartingPosition(LandingField field, int vehicleNumber);
    IReadOnlyList<Command> ReadCommands(int vehicleNumber);
    bool WantsToAddVehicle();
}
