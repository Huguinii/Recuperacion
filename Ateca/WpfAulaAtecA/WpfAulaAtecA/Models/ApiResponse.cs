using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAulaAtecA.Models
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }
        public T Result { get; set; }
    }
}

