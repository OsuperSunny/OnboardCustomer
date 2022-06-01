using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Domain.Entities.Externals
{
    public class Banks
    {
        public string bankName { get; set; }
        public string bankCode { get; set; }
    }

    public class Response
    {
        public Banks result { get; set; }
        public string errorMessage { get; set; }
        public List<string> errorMessages { get; set; }
        public bool hasError { get; set; }
        public DateTime timeGenerated { get; set; }
    }

}
