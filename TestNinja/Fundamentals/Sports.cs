namespace TestNinja.Fundamentals
{
    public class Sports // indexer example and expression-bodied property
    {
        private string[] types = { "Baseball", "Basketball", "Football",
                              "Hockey", "Soccer", "Tennis",
                              "Volleyball" };

        public string this[int i]
        {
            get => types[i];
            set => types[i] = value;
        }
    }
}