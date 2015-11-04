using System;
using System.Data.Entity;

namespace LegendaryQuestion.Models
{
    public class Query
    {
        public int ID { get; set; }
        public string AskedBy { get; set; }
        public DateTime AskedWhen { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }

    public class QuestionDBContext : DbContext
    {
        public DbSet<Query> Queries { get; set; }
    }
}