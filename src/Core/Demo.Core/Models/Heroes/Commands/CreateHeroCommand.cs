namespace Demo.Core.Models.Heroes.Commands
{
    public sealed class CreateHeroCommand
    {
        public CreateHeroCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }
    }
}