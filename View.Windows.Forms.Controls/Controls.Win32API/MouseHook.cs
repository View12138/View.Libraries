using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace View.Windows.Forms.Controls.Win32API
{
    /// <summary>
    /// 全局鼠标钩子
    /// </summary>
    public class MouseHook
    {
        /// <summary>
        /// 表示将用来处理 MouseMove 事件的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void MouseMoveHandler(object sender, MouseEventArgs e);
        /// <summary>
        /// 表示将用来处理 MouseClick 事件的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void MouseClickHandler(object sender, MouseEventArgs e);
        /// <summary>
        /// 表示将用来处理 MouseDown 事件的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void MouseDownHandler(object sender, MouseEventArgs e);
        /// <summary>
        /// 
        /// 表示将用来处理 MouseUp 事件的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void MouseUpHandler(object sender, MouseEventArgs e);

        private Point point;

        private int hHook;

        private static int hMouseHook = 0;

        private const int WM_MOUSEMOVE = 512;

        private const int WM_LBUTTONDOWN = 513;

        private const int WM_RBUTTONDOWN = 516;

        private const int WM_MBUTTONDOWN = 519;

        private const int WM_LBUTTONUP = 514;

        private const int WM_RBUTTONUP = 517;

        private const int WM_MBUTTONUP = 520;

        private const int WM_LBUTTONDBLCLK = 515;

        private const int WM_RBUTTONDBLCLK = 518;

        private const int WM_MBUTTONDBLCLK = 521;

        public const int WH_MOUSE_LL = 14;

        public HookProc hProc;

        /// <summary>
        /// 当鼠标移动时发生
        /// </summary>
        public event MouseMoveHandler MouseMoveEvent;
        /// <summary>
        /// 当鼠标点击时发生
        /// </summary>
        public event MouseClickHandler MouseClickEvent;
        /// <summary>
        /// 当鼠标按下时发生
        /// </summary>
        public event MouseDownHandler MouseDownEvent;
        /// <summary>
        /// 当鼠标抬起时发生
        /// </summary>
        public event MouseUpHandler MouseUpEvent;

        private Point Point
        {
            get
            {
                return point;
            }
            set
            {
                bool flag = point != value;
                if (flag)
                {
                    point = value;
                    bool flag2 = MouseMoveEvent != null;
                    if (flag2)
                    {
                        MouseEventArgs e = new MouseEventArgs(MouseButtons.None, 0, point.X, point.Y, 0);
                        MouseMoveEvent(this, e);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public MouseHook()
        {
            Point = default;
        }
        /// <summary>
        /// 加载钩子
        /// </summary>
        /// <returns></returns>
        public int SetHook()
        {
            hProc = new HookProc(MouseHookProc);
            hHook = HookFunction.SetWindowsHookEx(14, hProc, IntPtr.Zero, 0);
            return hHook;
        }
        /// <summary>
        /// 卸载钩子
        /// </summary>
        public void UnHook()
        {
            HookFunction.UnhookWindowsHookEx(hHook);
        }

        private int MouseHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            MouseHookStruct mouseHookStruct = (MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseHookStruct));
            bool flag = nCode < 0;
            int result;
            if (flag)
            {
                result = HookFunction.CallNextHookEx(hHook, nCode, wParam, lParam);
            }
            else
            {
                switch ((int)wParam)
                {
                    case 513:
                    {
                        MouseButtons button = MouseButtons.Left;
                        int clicks = 1;
                        MouseDownEvent?.Invoke(this, new MouseEventArgs(button, clicks, point.X, point.Y, 0));
                        break;
                    }
                    case 514:
                    {
                        MouseButtons button = MouseButtons.Left;
                        int clicks = 1;
                        MouseUpEvent?.Invoke(this, new MouseEventArgs(button, clicks, point.X, point.Y, 0));
                        break;
                    }
                    case 516:
                    {
                        MouseButtons button = MouseButtons.Right;
                        int clicks = 1;
                        MouseDownEvent?.Invoke(this, new MouseEventArgs(button, clicks, point.X, point.Y, 0));
                        break;
                    }
                    case 517:
                    {
                        MouseButtons button = MouseButtons.Right;
                        int clicks = 1;
                        MouseUpEvent?.Invoke(this, new MouseEventArgs(button, clicks, point.X, point.Y, 0));
                        break;
                    }
                    case 519:
                    {
                        MouseButtons button = MouseButtons.Middle;
                        int clicks = 1;
                        MouseDownEvent?.Invoke(this, new MouseEventArgs(button, clicks, point.X, point.Y, 0));
                        break;
                    }
                    case 520:
                    {
                        MouseButtons button = MouseButtons.Middle;
                        int clicks = 1;
                        MouseUpEvent?.Invoke(this, new MouseEventArgs(button, clicks, point.X, point.Y, 0));
                        break;
                    }
                }
                Point = new Point(mouseHookStruct.pt.x, mouseHookStruct.pt.y);
                result = HookFunction.CallNextHookEx(hHook, nCode, wParam, lParam);
            }
            return result;
        }
    }

    /// <summary>
    /// 回调函数的类型
    /// </summary>
    /// <param name="nCode"></param>
    /// <param name="wParam"></param>
    /// <param name="lParam"></param>
    /// <returns></returns>
    public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// 点类型
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class POINT
    {
        public int x;

        public int y;
    }
    /// <summary>
    /// 鼠标钩子类型
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class MouseHookStruct
    {
        public POINT pt;

        public int hwnd;

        public int wHitTestCode;

        public int dwExtraInfo;
    }
}