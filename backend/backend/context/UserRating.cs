using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.context
{
    public class UserRating
    {
        public decimal Id { get; set; }
        public virtual User User { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
