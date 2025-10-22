namespace To_do_List__Простой_
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetHighDpiMode(HighDpiMode.SystemAware); // Важно для DPI
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}