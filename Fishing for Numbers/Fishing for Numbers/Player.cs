namespace Fishing_for_Numbers
{
    public class Player
    {
        public Player(string name)
        {
            Name = name;
        }

        private string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}