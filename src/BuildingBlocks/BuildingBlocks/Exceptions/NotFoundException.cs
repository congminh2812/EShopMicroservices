namespace BuildingBlocks.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string name, object value) : base($@"Entity ""{name}"" with ""{value}"" not found") { }
    }
}
