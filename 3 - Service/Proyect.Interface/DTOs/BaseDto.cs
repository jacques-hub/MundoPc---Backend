using System;
using System.Collections.Generic;
using System.Text;

namespace Proyect.Interface.DTOs
{
    public class BaseDto
    {
        public long Id { get; set; }

        public bool IsDeleted { get; set; }

        public string IsDeletedStr => IsDeleted ? "SI" : "NO";
    }
}
