using System;

using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace MessageContract
{
    [ServiceContract(Namespace="http://MessageContract")]
    public interface ICalculate
    {
        [OperationContract(Action="http://learn/TestMessage_action", ReplyAction="http://learn/TestMessage_action")]
        TestMessage Calculate(TestMessage request);
    }

    [MessageContract]
    public class TestMessage
    {
        private string op;
        private double num1;
        private double num2;
        private double output;

        public TestMessage() {}

        public TestMessage(double n1, double n2, string op, double output)
        {
            this.num1 = n1;
            this.num2 = n2;
            this.op = op;
            this.output = output;
        }

        public TestMessage(TestMessage message)
        {
            this.num1 = message.num1;
            this.num2 = message.num2;
            this.op = message.op;
            this.output = message.output;
        }

        [MessageHeader]
        public string Op
        {
            get { return op; }
            set { op = value; }
        }

        [MessageBodyMember]
        public double Num1
        {
            get { return num1; }
            set { num1 = value; }
        }

        [MessageBodyMember]
        public double Num2
        {
            get { return num2; }
            set { num2 = value; }
        }

        [MessageBodyMember]
        public double Output
        {
            get { return output; }
            set { output = value; }
        }
    }

    public class CalculateService : ICalculate
    {
        public TestMessage Calculate(TestMessage request)
        {
            TestMessage response = new TestMessage(request);
            switch (request.Op)
            {
                case "+":
                    response.Output = request.Num1 + request.Num2;
                    break;
                case "-":
                    response.Output = request.Num1 - request.Num2;
                    break;
                case "*":
                    response.Output = request.Num1 * request.Num2;
                    break;
                case "/":
                    response.Output = request.Num1 / request.Num2;
                    break;
                default:
                    response.Output = 0.0D;
                    break;
            }
            return response;
        }
    }
}
