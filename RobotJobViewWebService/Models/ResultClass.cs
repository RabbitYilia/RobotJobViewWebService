using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotJobViewWebService.Models
{
    public class Result
    {
        public int ID { get; set; }
        public Job Job { get; set; }
        public String filename { get; set; }
        public String filelocalpath { get; set; }
    }
}
