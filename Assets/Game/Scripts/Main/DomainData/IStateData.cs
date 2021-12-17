namespace Main.DomainData
{
    public interface IStateData
    {
        string StateName { get; }
        int    Amount    { get; }
    }
}
