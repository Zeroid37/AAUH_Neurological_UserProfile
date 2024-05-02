using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEndAAUH.Model;

namespace BackEndAAUH.DB {
    public interface FlagDAO {
        public Flag getFlagById(int id);
        public int addFlagToDB(Flag flag);
    }
}
