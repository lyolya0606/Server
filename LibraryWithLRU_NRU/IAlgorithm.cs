using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;

namespace LibraryWithLRU_NRU {
    [ServiceContract]
    public interface IAlgorithm {
        [OperationContract]
        int GetInterrupts(List<int> input, List<bool> modifiedBit, int buffer, int numOfFilled);
        [OperationContract]
        List<string> GetSteps();
    }
}
