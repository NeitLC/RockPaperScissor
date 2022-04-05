namespace RockPaperScissor;

public enum Value
{
    Win,
    Lose,
    Draw
}
public class Result
{
    private int MoveCount;

    public Result(int moveCount)
    {
        MoveCount = moveCount;
    }

    public Value Decide(int first, int second)
    {
        if (first == second)
        {
            return Value.Draw;
        }

        if ((second < first && first - second == MoveCount / 2) || (second > first && second - first == MoveCount / 2))
        {
            return Value.Win;
        }

        return Value.Lose;
    }
}