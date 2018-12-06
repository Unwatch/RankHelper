using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessWeb
{
    public class Domain
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public eAccessStatus Status { get; set; }

        public Domain(int ID=0, String Name = "",eAccessStatus Status = eAccessStatus.none)
        {
            this.ID = ID;
            this.Name =Name;
            this.Status = Status;
        }
    }

    public enum eAccessStatus
    {
        none,
        success,
        fail
    }
}
