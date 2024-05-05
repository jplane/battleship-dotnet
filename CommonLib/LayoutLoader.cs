
namespace CommonLib;

public interface ILayoutLoader
{
    IEnumerable<LayoutItem> GetLayout();
}

public class LayoutItem
{
    public int StartRow { get; set; }
    public int EndRow { get; set; }
    public int StartColumn { get; set; }
    public int EndColumn { get; set; }
    public CellStatus Status { get; set; }
}

internal static class LayoutLoaderUtils
{
    public static IEnumerable<LayoutItem> Parse(IEnumerable<string> lines)
    {
        foreach(var line in lines)
        {
            var parts = line.Split(' ');
            
            var status = parts[0] switch
            {
                "A" => CellStatus.AIRCRAFT_CARRIER,
                "B" => CellStatus.BATTLESHIP,
                "C" => CellStatus.CRUISER,
                "S" => CellStatus.SUB,
                "D" => CellStatus.DESTROYER,
                _ => CellStatus.NOTHING
            };

            var startrow = Move.GetRowIndex(parts[1][..1]);
            var startcol = int.Parse(parts[1][1..]) - 1;

            var endrow = Move.GetRowIndex(parts[2][..1]);
            var endcol = int.Parse(parts[2][1..]) - 1;

            yield return new LayoutItem
            {
                StartRow = startrow,
                EndRow = endrow,
                StartColumn = startcol,
                EndColumn = endcol,
                Status = status
            };
        }
    }
}

public class EnvironmentVariableLayoutLoader : ILayoutLoader
{
    private readonly string _envVarName;

    public EnvironmentVariableLayoutLoader(string envVarName)
    {
        _envVarName = envVarName;
    }

    public IEnumerable<LayoutItem> GetLayout()
    {
        var lines = Environment.GetEnvironmentVariable(_envVarName)?
                               .Split("|")
                               .Select(x => x.Trim());

        return LayoutLoaderUtils.Parse(lines ??
                                       throw new InvalidOperationException($"{_envVarName} environment variable not set."));
    }
}

public class FileLayoutLoader : ILayoutLoader
{
    private readonly string _filepath;

    public FileLayoutLoader(string filepath)
    {
        _filepath = filepath;
    }

    public IEnumerable<LayoutItem> GetLayout()
    {
        var lines = File.ReadAllLines(_filepath);
        return LayoutLoaderUtils.Parse(lines);
    }
}
