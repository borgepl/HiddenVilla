using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Registration
{
    public class RegistrationResponseDTO
    {
        public bool IsRegisterationSuccessful { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
