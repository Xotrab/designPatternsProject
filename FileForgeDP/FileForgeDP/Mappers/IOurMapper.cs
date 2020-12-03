namespace FileForgeDP
{
    public interface IOurMapper
    {
        T Map<T>(object objectToMap) where T : new();
    }
}
