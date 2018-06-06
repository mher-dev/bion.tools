namespace BionCore.Tools.Console
{
    public interface IBCColorAmbit : IBDisposable, IBUID
    {
        IBCColorExtended AmbitColor { get; }
    }
}