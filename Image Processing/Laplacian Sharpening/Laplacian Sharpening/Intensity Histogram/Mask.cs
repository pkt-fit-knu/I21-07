namespace LaplacianSharper
{
    class Mask
    {
        private int[,] array;

        public Mask(int[,] array)
        {
            this.array = array;
        }

        public int GetCell(int x, int y)
        {
            return this.array[x, y];
        }
    }
}
