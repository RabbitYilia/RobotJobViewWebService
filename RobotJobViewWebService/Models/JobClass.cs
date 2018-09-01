using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotJobViewWebService.Models
{
    public class Job
    {
        public int ID { get; set; }
        public string JobName { get; set; }
        public string Type { get; set; }
        public string Arg { get; set; }
        public string Status { get; set; }
        public string User { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
