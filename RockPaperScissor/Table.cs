using ConsoleTables;

namespace RockPaperScissor;

public class Table
{
    private readonly List<string> _players;

    public Table(List<string> players)
    {
        _players = players;
    }

    public void PrintTable()
    {
        var header = _players.Prepend("PC \\ User");
        var table = new ConsoleTable(header.ToArray());
        var result = new Result(_players.Count);

        for (int i = 0; i < _players.Count; i++)
        {
            var curRow = new string?[_players.Count + 1];
            curRow[0] = _players[i];

            for (int j = 0; j < _players.Count; j++)
            {
                curRow[j + 1] = Enum.GetName(typeof(Value), result.Decide(i, j));
            }
            
            table.AddRow(curRow.ToArray());
        }
        table.Write(Format.Alternative);
    }
    
}