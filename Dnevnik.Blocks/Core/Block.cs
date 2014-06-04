namespace Dnevnik.Blocks.Core
{
    public class Block
    {
        public string Name { get; set; }

        public string[] Mods { get; set; }

        public Block[] Inners { get; set; }

        public object Model { get; set; }

        public bool WithJs { get; set; }
    }
}