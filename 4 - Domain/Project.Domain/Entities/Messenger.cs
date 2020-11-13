namespace Project.Domain.Entities
{
    public class Messenger: EntityBase
    {
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string query { get; set; }
        public bool isAnswered { get; set; }
    }
}
