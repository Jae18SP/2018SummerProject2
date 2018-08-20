using System.Collections.Generic;

namespace WebApplication2.Algoritham
{
    public class Configuration
    {
        public int NumberOfTables { get; set; }
        public int PeoplePerTable { get; set; }

        public List<Person> People { get; set; }
        public List<Relationship> Relationships { get; set; }

        public Configuration()
        {
            People = new List<Person>();
            Relationships = new List<Relationship>();
        }
    }
}