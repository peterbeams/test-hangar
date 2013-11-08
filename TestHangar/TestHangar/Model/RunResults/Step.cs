namespace TestHangar.Model.RunResults
{
    public class Step
    {
        public string keyword { get; set; }
        public string name { get; set; }
        public Match match { get; set; }
        public Result result { get; set; }
        public Embedding[] embeddings { get; set; }
    }
}