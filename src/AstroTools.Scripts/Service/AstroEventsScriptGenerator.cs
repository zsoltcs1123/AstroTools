using System.Text;
using AstroTools.Events.Model;

namespace AstroTools.Scripts.Service;

public class AstroEventsScriptGenerator : IAstroEventsScriptGenerator
{
    public (string Timestamps, string Labels) Generate(IEnumerable<AstroEvent> astroEvents)
    {
        StringBuilder timeStampsSb = new StringBuilder();
        StringBuilder labelsSb = new StringBuilder();

        timeStampsSb.Append("array.from(\n");
        labelsSb.Append("array.from(\n");

        foreach (var astroEvent in astroEvents)
        {
            timeStampsSb.Append($"timestamp(\"{astroEvent.Date.ToString("yyyy-MM-dd hh:mm")} UTC\"),\n");
            labelsSb.Append($"\"{astroEvent.Description}\",\n");
        }

        timeStampsSb.Remove(timeStampsSb.Length-2, 2);
        labelsSb.Remove(labelsSb.Length-2, 2);

        timeStampsSb.Append(')');
        labelsSb.Append(')');

        return (timeStampsSb.ToString(), labelsSb.ToString());
    }
}