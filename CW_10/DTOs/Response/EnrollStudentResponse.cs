﻿using CW_10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CW_10.DTOs.Response
{
    public class EnrollStudentResponse
    {
        public string LastName { get; set; }
        public string IndexNumber { get; set; }
        public int Semestrer { get; set; }
        public Enrollment enrollment { get; set; }
        public string answer { get; set; }
    }
}
