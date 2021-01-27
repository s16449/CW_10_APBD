using CW_10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CW_10.DTOs.Response
{
    public class StudentPromotionResponse
    {
        public Enrollment enrollment { get; set; }
        public string answer { get; set; }
    }
}
