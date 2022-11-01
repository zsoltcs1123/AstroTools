using AstroTools.Events.Model;

namespace AstroTools.Scripts.Service;

public interface IAstroEventsScriptGenerator
{
    (string Timestamps, string Labels) Generate(IEnumerable<AstroEvent> astroEvents);
}