using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LegendaryQuestion.Models
{
    public class CaseModel
    {
        public string Id { get; set; }
        public string CaseNumber { get; set; }
        public string SuppliedName { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
    public class CaseModel1
    {
        public string Status { get; set; }
    }
}