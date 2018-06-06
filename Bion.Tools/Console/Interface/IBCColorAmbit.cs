namespace Bion.Tools.Console
{
    public interface IBCColorAmbit : IBDisposable, IBUID
    {
        IBCColorExtended AmbitColor { get; }
    }
}