using AstroTools.Events.Model;

namespace AstroTools.Scripts.Service;

public interface IAstroEventsScriptGenerator
{
    string Generate(IEnumerable<AstroEvent> astroEvents, string inputFileName);
}