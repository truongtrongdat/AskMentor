using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Entity
{
    public class ViewProfile
    {
        public ApplicationUser ApplicationUsers { get; set; }
        public List<Evaluate> Evaluates { get; set; }
    }
}
