using System.Runtime.Serialization;

namespace XFormsRPNCalculator
{
    [DataContract]
    public class CalculatorState
    {
        [DataMember]
        public string Output { get; set; }
        [DataMember]
        public string Format { get; set; }
        [DataMember]
        public bool IsNewPending { get; set; }
        [DataMember]
        public double Memory { get; set; }
        [DataMember]
        public double XRegister { get; set; }
        [DataMember]
        public double? Accumulator { get; set; }
    }
}
