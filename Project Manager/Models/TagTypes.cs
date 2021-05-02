using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManage.Models
{
    public class TagTypes
    {
        public int Id { get; set; }
        public int fk_Project { get; set; }
        public string TagType { get; set; }
    }
}
