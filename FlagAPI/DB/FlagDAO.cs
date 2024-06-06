﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlagAPI.Model;

namespace FlagAPI.DB {
    public interface FlagDAO {
        public Flag getFlagById(int id);
        public int addFlagToDB(Flag flag);
        public List<PatientAnswer> getNewPatientAnswers(DateOnly lastRead);
    }
}