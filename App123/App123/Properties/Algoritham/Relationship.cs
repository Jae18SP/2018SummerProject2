namespace WebApplication2.Algoritham
{
    public class Relationship
    {
        public string Left { get; set; }
        public string Right { get; set; }
        public int Score { get; set; }

        public override string ToString()
        {
            return Left + " + " + Right + ", Score: " + Score;
        }
    }
}