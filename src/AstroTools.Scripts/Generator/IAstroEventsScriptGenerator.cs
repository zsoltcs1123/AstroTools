using AstroTools.Events.Model;

namespace AstroTools.Scripts.Generator;

public interface IAstroEventsScriptGenerator
{
    string Generate(IEnumerable<AstroEvent> astroEvents, string inputFileName);
}