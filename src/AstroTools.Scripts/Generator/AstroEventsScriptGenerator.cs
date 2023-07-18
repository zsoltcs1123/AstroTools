using System.Text;
using AstroTools.Events.Model;

namespace AstroTools.Scripts.Generator;

public class AstroEventsScriptGenerator : IAstroEventsScriptGenerator
{
    private static string GetFullPath(string fileName) => $"{AppDomain.CurrentDomain.BaseDirectory}\\{fileName}";

    public string Generate(IEnumerable<AstroEvent> astroEvents, string inputFileName)
    {
        var lines = File.ReadAllLines(GetFullPath(inputFileName));
        var sb = new StringBuilder();
        var tsAndLabels = GenerateTimeStampsAndLabels(astroEvents);

        for (int i = 0; i < lines.Length; i++)
        {
            if (i == 2)
            {
                sb.Append(tsAndLabels.Timestamps);
                sb.AppendLine();
                sb.AppendLine();
                sb.Append(tsAndLabels.Labels);
                sb.AppendLine();
            }
            else
            {
                sb.AppendLine(lines[i]);
            }
        }

        return sb.ToString();
    }


    private static (string Timestamps, string Labels) GenerateTimeStampsAndLabels(IEnumerable<AstroEvent> astroEvents)
    {
        StringBuilder timeStampsSb = new StringBuilder();
        StringBuilder labelsSb = new StringBuilder();

        timeStampsSb.Append("var int[] subLordBoundaryArray = array.from(\n");
        labelsSb.Append("var string[] subLordLabelArray = array.from(\n");

        foreach (var astroEvent in astroEvents)
        {
            timeStampsSb.Append($"     timestamp(\"{astroEvent.Date.ToString("yyyy-MM-dd HH:mm")} UTC\"),\n");
            labelsSb.Append($"     \"{astroEvent.Name} {astroEvent.Description}\",\n");
        }

        timeStampsSb.Remove(timeStampsSb.Length - 2, 2);
        labelsSb.Remove(labelsSb.Length - 2, 2);

        timeStampsSb.Append(')');
        labelsSb.Append(')');

        return (timeStampsSb.ToString(), labelsSb.ToString());
    }
}