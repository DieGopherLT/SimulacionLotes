namespace SimulacionLotes
{
    /*
     * Esta clase es una abstracción para tener los datos que requiere cada proceso en un tipo de dato.
     * La clase tiene dos responsabilidades:
     * 
     * La primera es almacenar todos los datos de un proceso.
     * La segudna es encargarse de las representaciones en cadenas para mostrar en la UI, he hecho que 
     * el proceso se resuelva a sí mismo porque así para en la vida real.
     */
    internal class Process
    {
        public int ID { get; set; }
        public required string ProgrammerName { get; set; }
        public required string Operation { get; set; }
        public int TME { get; set; }
        public int Time { get; set; }
        public bool HasError { get; set; }

        /*
         * Es la representación en string de un proceso pendiente y en ejecución.
         * 
         * La identación está así ya que al usar string interpolation, los espacios que tiene aquí en el editor
         * se reflejan en la UI, así es como considero se ve mejor.
         */
        public override string ToString()
        {
            return 
            $@"
    ===========
    ID: {ID}
    Iniciado por: {ProgrammerName}
    Operacion: {Operation}
    TME: {TME}
    ===========
            ";
        }

        /*
         * Es la representación en string de un proceso ya completado.
         * 
         * La identación está así ya que al usar string interpolation, los espacios que tiene aquí en el editor
         * se reflejan en la UI, así es como considero se ve mejor.
         */
        public string ToStringSolved()
        {
            string operationSolved = SolveOperation();
            string completedIn = HasError ? "Proceso cancelado" : $"Completado en: {Time} segundos";
            return
            $@"
    ===========
    ID: {ID}
    Iniciado por: {ProgrammerName}
    {operationSolved}
    {completedIn}
    ===========
            ";
        }

        // Resuelve la operación de cada proceso
        private string SolveOperation()
        {
            if (HasError)
                return "ERROR";

            string[] splittedOperation = Operation.Split(" ");
            double result = 0;

            double firstOperand = Convert.ToDouble(splittedOperation.ElementAt(0));
            string op = splittedOperation.ElementAt(1);
            double secondOperand = Convert.ToDouble(splittedOperation.ElementAt(2));

            switch(op)
            {
                case "+":
                    result = firstOperand + secondOperand;
                    break;
                case "-":
                    result = firstOperand - secondOperand;
                    break;
                case "*":
                    result = firstOperand * secondOperand;
                    break;
                case "/":
                    if (secondOperand == 0)
                    {
                        result = double.NaN;
                        break;
                    }
                    result = firstOperand / secondOperand;
                    break; ;
            }

            return $"{Operation} = {result.ToString("0.00")}";
        }
    }
}
