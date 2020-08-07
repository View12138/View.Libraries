using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace View.Windows.Forms.Controls.Win32API
{
    /// <summary>
    /// 钩子API
    /// </summary>
    public static class HookFunction
    {
        /// <summary>
        /// 设置一个钩子
        /// </summary>
        /// <param name="idHook">钩子类型</param>
        /// <param name="lpfn">回调函数地址</param>
        /// <param name="hInstance">实例句柄</param>
        /// <param name="threadId">线程ID</param>
        /// <returns>若此函数执行成功,则返回值就是该挂钩处理过程的句柄;若此函数执行失败,则返回值为NULL(0)</returns>
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        /// <summary>
        /// 删除一个钩子
        /// </summary>
        /// <param name="idHook">要删除的钩子的句柄。这个参数是上一个函数SetWindowsHookEx的返回值</param>
        /// <returns></returns>
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
        public static extern bool UnhookWindowsHookEx(int idHook);
        /// <summary>
        /// 将钩子信息传递到当前钩子链中的下一个子程
        /// </summary>
        /// <param name="idHook">当前钩子的句柄</param>
        /// <param name="nCode">钩子代码; 就是给下一个钩子要交待的</param>
        /// <param name="wParam">钩子代码; 就是给下一个钩子要交待的</param>
        /// <param name="lParam">要传递的参数; 由钩子类型决定是什么参数</param>
        /// <returns>返回这个值链中的下一个钩子程序。当前Hook过程也必须返回该值。返回值的含义取决于钩型。</returns>
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
        public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);
    }
}
