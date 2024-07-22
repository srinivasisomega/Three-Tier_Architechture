using BackendLayer;

namespace BusinessLogic
{
    public class BusinessLogicLayer
    {
        private OperationStore _operationStore;

        public BusinessLogicLayer(OperationStore operationStore)
        {
            _operationStore = operationStore;
        }

        public static void Main(string[] args) { }

        public int BinaryOpp(string operation, int num1, int num2)
        {
            switch (operation)
            {
                case "Addition":
                    return num1 + num2;
                case "Subtraction":
                    return num1 - num2;
                case "Multiply":
                    return num1 * num2;
                case "Division":
                    return num1 / num2;
                default:
                    throw new NotImplementedException();
            }
        }

        public void GeometryCal(int radius, out float area, out float volume)
        {
            var circle = new Circle();
            area = circle.pi * radius * radius;
            volume = circle.VolumeConstant * circle.pi * radius * radius * radius;
        }

        public void StoreOperation(string name, string operation)
        {
            _operationStore.Name = name;
            _operationStore.Operation = operation;
            _operationStore.PrintStoredOperation();
        }
    }
}


//namespace BusinessLogic
//{
//    public class BusinessLogicLayer
//    {
//        public static void Main(string[] args) { }
//        public int BinaryOpp(string operation, int num1, int num2)
//        {
//            switch (operation)
//            {
//                case "Addition":
//                    return num1 + num2;
//                case "Subtraction":
//                    return num1 - num2;
//                case "Multiply":
//                    return num1 * num2;
//                case "Division":
//                    return num1 / num2;
//                default:
//                    throw new NotImplementedException();
//            }
//        }

//        public void GeometryCal(int radius, out float area, out float volume)
//        {
//            var circle = new Circle();
//            area = circle.pi * radius * radius;
//            volume = circle.vol * circle.pi * radius * radius * radius;
//        }
//    }


//}

//namespace BussinessLogic
//{
//    public class BusinessLogicLayer
//    {
//        public int BinaryOpp(string operation, int num1, int num2)
//        {
//            if (operation == "Addition")
//            {
//                return num1 + num2;
//            }
//            else if (operation == "Subtraction")
//            {
//                return num1 - num2;
//            }
//            else if (operation == "Multiply")
//            {
//                return num1 * num2;
//            }
//            else if (operation == "Division")
//            {
//                return num1 / num2;
//            }
//            else
//            {
//                throw new NotImplementedException();
//            }
//        }
//        public void GeometryCal(string shape, int num1,out float area, out float volume)
//        {
//            if (shape == "Sphere")
//            {
//                Circle newShape = new Circle();
//                area = newShape.pi * num1 * num1;
//                volume = newShape.vol * newShape.pi * num1 * num1 * num1;
//            }
//            else
//            {
//                area = 0;
//                volume = 0;
//            }

//        }
//        public static void Main() { }
//    }
//}
