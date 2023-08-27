
namespace SimulacionLotes
{
    // En 
    public partial class MainForm : Form
    {
        private void WriteOnFile(string filePath,  string documentText)
        {
            lock(filePath)
            {
                using StreamWriter writer = new(filePath);
                writer.Write(documentText);
            }
        }
    }
}
