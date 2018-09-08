namespace Demo.Core.Models.Heroes.Commands
{
    public sealed class ChangeHeroNameCommand
    {
        public ChangeHeroNameCommand(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; }
        public string Name { get; }

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Id))
                return false;

            if (string.IsNullOrWhiteSpace(Name))
                return false;

            return true;
        }
    }
}