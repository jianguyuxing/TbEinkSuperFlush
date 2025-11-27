using System;
using System.IO;
using System.Windows.Forms;

namespace TbEinkSuperFlush
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // 创建调试输出文件
            string debugFile = Path.Combine(AppContext.BaseDirectory, "debug_output.txt");
            try
            {
                // 全局异常捕获
                AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                {
                    var ex = (Exception)e.ExceptionObject;
                    File.AppendAllText(debugFile, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] FATAL UnhandledException: {ex}{Environment.NewLine}");
                };
                Application.ThreadException += (s, e) =>
                {
                    File.AppendAllText(debugFile, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] FATAL ThreadException: {e.Exception}{Environment.NewLine}");
                };

                File.AppendAllText(debugFile, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] === Application Starting ==={Environment.NewLine}");
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                File.AppendAllText(debugFile, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] High DPI mode set{Environment.NewLine}");
                ApplicationConfiguration.Initialize();
                File.AppendAllText(debugFile, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] Configuration initialized, creating MainForm...{Environment.NewLine}");
                var form = new MainForm();
                File.AppendAllText(debugFile, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] MainForm created successfully{Environment.NewLine}");
                
                // 确保窗体创建完成并显示托盘图标
                File.AppendAllText(debugFile, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] Starting message loop...{Environment.NewLine}");
                Application.Run(form);
                File.AppendAllText(debugFile, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] Application.Run completed{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                File.AppendAllText(debugFile, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] Fatal exception in Main: {ex.GetType().Name}: {ex.Message}{Environment.NewLine}");
                File.AppendAllText(debugFile, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] HRESULT: 0x{ex.HResult:X8}{Environment.NewLine}");
                File.AppendAllText(debugFile, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] StackTrace: {ex.StackTrace}{Environment.NewLine}");
                MessageBox.Show($"Fatal error: {ex.Message}\n\nHRESULT: 0x{ex.HResult:X8}\n\n{ex.StackTrace}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                File.AppendAllText(debugFile, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] === Application Exiting ==={Environment.NewLine}");
            }
        }
    }
}