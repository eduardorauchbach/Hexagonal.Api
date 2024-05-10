namespace Hexagonal.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static void CopyTo<T1, T2>(this T1 source, T2 destination)
        {
            var sourceProperties = typeof(T1).GetProperties();
            var destinationProperties = typeof(T2).GetProperties()
                                                    .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

            foreach (var sourceProperty in sourceProperties)
            {
                if (destinationProperties.TryGetValue(sourceProperty.Name, out var destinationProperty))
                {
                    if (destinationProperty.CanWrite)
                    {
                        var sourceValue = sourceProperty.GetValue(source);
                        destinationProperty.SetValue(destination, sourceValue);
                    }
                }
            }
        }
    }
}
