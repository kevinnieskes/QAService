using System;
using System.Data.Entity;

namespace LegendaryQuestion.Models
{
    public class Query
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Question { get; set; }

    }

    public class QuestionDBContext : DbContext
    {
        public DbSet<Query> Queries { get; set; }
    }
}