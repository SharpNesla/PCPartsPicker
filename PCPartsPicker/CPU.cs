using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCPartsPicker
{
    public class CPU
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public int SingleThreadMark { get; set; }
        public int MultiThreadMark { get; set; }
        public string Description { get; set; }
    }
}
