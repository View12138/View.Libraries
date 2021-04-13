using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using File = System.IO.File;
using IWshRuntimeLibrary;

namespace View.Windows.Shortcut
{
    public class ShortcutCore
    {
        private readonly string extension = ".lnk";

        private string _productName;
        private string _exeFullPath;
        private string _arguments;
        private string _iconFullPath;

        /// <summary>
        /// 初始化快捷方式构造器。
        /// </summary>
        /// <param name="productName">产品名称。<para>快捷方式名称</para></param>
        /// <param name="exeFullPath">快捷方式指向的可执行程序的完整路径。</param>
        /// <param name="arguments">快捷方式的启动参数。</param>
        /// <param name="iconFullPath">快捷方式图标的完整路径。<para>为空则使用可执行程序的默认图标</para></param>
        public ShortcutCore(string productName, string exeFullPath, string arguments = "", string iconFullPath = "")
        {
            if (string.IsNullOrWhiteSpace(productName)) { throw new ArgumentException("快捷方式名称不能为空", nameof(productName)); }
            if (string.IsNullOrWhiteSpace(exeFullPath)) { throw new ArgumentException("可执行程序路径不能为空", nameof(exeFullPath)); }
            if (productName.EndsWith(extension)) { productName = productName.Remove(productName.LastIndexOf('.')); }

            _productName = productName;
            _exeFullPath = exeFullPath;
            _arguments = arguments;
            _iconFullPath = iconFullPath ?? throw new ArgumentNullException(nameof(iconFullPath));
        }

        /// <summary>
        /// 获取或设置产品名称。
        /// <para>即快捷方式的名称</para>
        /// </summary>
        public string ProductName { get => _productName; set => _productName = value; }
        /// <summary>
        /// 获取或设置可执行程序的完整路径。
        /// </summary>
        public string ExeFullPath { get => _exeFullPath; set => _exeFullPath = value; }
        /// <summary>
        /// 获取或设置快捷方式的参数
        /// </summary>
        public string Arguments { get => _arguments; set => _arguments = value; }
        /// <summary>
        /// 获取或设置快捷方式图标的完整路径。
        /// </summary>
        public string IconFullPath { get => _iconFullPath; set => _iconFullPath = value; }

        /// <summary>
        /// 在指定位置上创建快捷方式
        /// </summary>
        /// <param name="type">指定类型</param>
        /// <returns>成功或失败</returns>
        public bool CreateShortcut(ShortcutType type)
        {
            if (Exists(type))
            { return true; }
            try
            {
                WshShell shell = new WshShell();
                //快捷键方式创建的位置、名称
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(GetLnkPath(type, _productName));
                shortcut.TargetPath = _exeFullPath; //目标文件
                                                    //该属性指定应用程序的工作目录，当用户没有指定一个具体的目录时，快捷方式的目标应用程序将使用该属性所指定的目录来装载或保存文件。
                shortcut.WorkingDirectory = Path.GetDirectoryName(_exeFullPath);
                shortcut.WindowStyle = 1; //目标应用程序的窗口状态分为普通、最大化、最小化【1,3,7】
                shortcut.Description = _productName; //描述
                shortcut.IconLocation = string.IsNullOrWhiteSpace(IconFullPath) ? _exeFullPath : IconFullPath;  //快捷方式图标
                shortcut.Arguments = _arguments;
                //shortcut.Hotkey = "CTRL+ALT+F11"; // 快捷键
                shortcut.Save(); //必须调用保存快捷才成创建成功
                return true;
            }
            catch
            { return false; }
        }

        /// <summary>
        /// 获取一个值，指示是否已经存在指定类型的快捷方式。
        /// </summary>
        /// <param name="type">指定类型</param>
        /// <returns></returns>
        public bool Exists(ShortcutType type) => File.Exists(GetLnkPath(type, _productName));

        /// <summary>
        /// 移除指定类型的快捷方式
        /// </summary>
        /// <param name="type">指定类型</param>
        /// <returns></returns>
        public bool RemoveShortcut(ShortcutType type)
        {
            try
            {
                File.Delete(GetLnkPath(type, _productName));
                return true;
            }
            catch
            { return false; }
        }

        /// <summary>
        /// 获取指定类型的快捷方式路径。
        /// </summary>
        /// <param name="type">指定类型</param>
        /// <param name="productName">产品名称</param>
        /// <returns></returns>
        private string GetLnkPath(ShortcutType type, string productName)
        {
            if (type == ShortcutType.Desktop)
            { return $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\{productName}{extension}"; }
            else
            { return $@"{Environment.GetFolderPath(Environment.SpecialFolder.Startup)}\{productName}{extension}"; }
        }
    }
}
