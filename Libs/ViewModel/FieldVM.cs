using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.ViewModel
{
    public class FieldVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Topic>? Topics { get; set; }

        public static implicit operator Field(FieldVM vm)
        {
            return new Field
            {
                Id = vm.Id,
                Name = vm.Name,
            };
        }
        public static implicit operator FieldVM(Field vm)
        {
            return new FieldVM
            {
                Id = vm.Id,
                Name = vm.Name,
            };
        }
    }
}
