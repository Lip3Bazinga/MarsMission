namespace MarsMission;

// Abstrai a saída visual, desacoplando o simulador do Console.
public interface IVisualOutput
{
    void ShowHeader();
    void ShowMessage(string message);
    void DrawField(LandingField field, IReadOnlyList<Vehicle> vehicles);
    void ShowFinalResult(IReadOnlyList<Vehicle> vehicles);
}
