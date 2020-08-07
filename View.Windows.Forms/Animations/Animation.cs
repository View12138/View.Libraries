using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace View.Windows.Forms.Animations
{
    /// <summary>
    /// 适用于winform的动画类，创建此类的对象后通过Set函数来启动需要的动画
    /// </summary>
    public partial class Animation
    {
        private int fps;

        private Dictionary<object, List<AnimJob>> objects;

        private Timer tmrAnim = new Timer();

        /// <summary>
        /// 获取或设置fps属性
        /// </summary>
        public int Fps
        {
            get
            {
                return this.fps;
            }
            set
            {
                bool flag = value < 10;
                if (flag)
                {
                    this.fps = 10;
                }
                else
                {
                    bool flag2 = value > 100;
                    if (flag2)
                    {
                        this.fps = 100;
                    }
                    else
                    {
                        this.fps = value;
                    }
                }
                this.tmrAnim.Interval = 1000 / this.Fps;
            }
        }

        /// <summary>
        /// 构造Animation对象
        /// </summary>
        /// <param name="FPS">初始化动画帧数<para>默认40帧</para></param>
        public Animation(int FPS = 40)
        {
            this.objects = new Dictionary<object, List<AnimJob>>();
            this.tmrAnim.Enabled = true;
            this.tmrAnim.Tick += new EventHandler(this.TmrAnim_Tick);
            this.Fps = FPS;
        }

        /// <summary>
        /// 设置动画属性并启动动画
        /// </summary>
        /// <param name="obj">产生动画的控件对象</param>
        /// <param name="prop">动画变化的属性
        /// <para>即次控件对象的属性</para>
        /// </param>
        /// <param name="value">次属性对应的要变化到的值</param>
        public void Set(object obj, Properties prop, object value)
        {
            if (obj == null) return;
            bool flag = !this.objects.ContainsKey(obj);
            if (flag)
            {
                List<AnimJob> value2 = new List<AnimJob>();
                this.objects.Add(obj, value2);
            }
            AnimJob animJob = new AnimJob
            {
                Property = prop,
                Value = value
            };
            bool flag2 = !this.UpdateExistProperty(this.objects[obj], animJob);
            if (flag2)
            {
                this.objects[obj].Add(animJob);
            }
        }

        /// <summary>
        /// 动画构造器
        /// </summary>
        /// <param name="thisValue">当前值</param>
        /// <param name="desValue">要变化到的值</param>
        /// <returns></returns>
        private int GetStep(int thisValue, int desValue)
        {
            bool flag = thisValue == desValue;
            int result;
            if (flag)
            {
                result = 0;
            }
            else
            {
                int num = (desValue - thisValue) / 3;
                bool flag2 = num == 0;
                if (flag2)
                {
                    bool flag3 = desValue > thisValue;
                    if (flag3)
                    {
                        num = 1;
                    }
                    else
                    {
                        num = -1;
                    }
                }
                result = num;
            }
            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="intStep"></param>
        /// <returns></returns>
        private double ConvertStepToPersent(int intStep)
        {
            double num = (double)intStep / 100.0;
            bool flag = Math.Abs(num) < 0.01;
            if (flag)
            {
                bool flag2 = intStep > 0;
                if (flag2)
                {
                    num = 0.01;
                }
                else
                {
                    num = -0.01;
                }
            }
            return num;
        }

        /// <summary>
        /// 动画执行器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmrAnim_Tick(object sender, EventArgs e)
        {
            foreach (KeyValuePair<object, List<AnimJob>> current in this.objects)
            {
                Control control;
                bool flag = (control = (current.Key as Control)) != null;
                if (flag)
                {
                    List<AnimJob> value = current.Value;
                    foreach (AnimJob current2 in value)
                    {
                        switch (current2.Property)
                        {
                            case Properties.Left:
                            {
                                int step = this.GetStep(control.Left, (int)current2.Value);
                                bool flag2 = step == 0;
                                if (flag2)
                                {
                                    value.Remove(current2);
                                    return;
                                }
                                control.Left += step;
                                break;
                            }
                            case Properties.Top:
                            {
                                int step = this.GetStep(control.Top, (int)current2.Value);
                                bool flag3 = step == 0;
                                if (flag3)
                                {
                                    value.Remove(current2);
                                    return;
                                }
                                control.Top += step;
                                break;
                            }
                            case Properties.Width:
                            {
                                int step = this.GetStep(control.Width, (int)current2.Value);
                                bool flag4 = step == 0;
                                if (flag4)
                                {
                                    value.Remove(current2);
                                    return;
                                }
                                control.Width += step;
                                break;
                            }
                            case Properties.Height:
                            {
                                int step = this.GetStep(control.Height, (int)current2.Value);
                                bool flag5 = step == 0;
                                if (flag5)
                                {
                                    value.Remove(current2);
                                    return;
                                }
                                control.Height += step;
                                break;
                            }
                            case Properties.Backcolor:
                            {
                                Color color = (Color)current2.Value;
                                int r = (int)color.R;
                                int g = (int)color.G;
                                int b = (int)color.B;
                                int step2 = this.GetStep((int)control.BackColor.R, r);
                                int step3 = this.GetStep((int)control.BackColor.G, g);
                                int step4 = this.GetStep((int)control.BackColor.B, b);
                                bool flag6 = Math.Abs(step2) + Math.Abs(step3) + Math.Abs(step4) == 0;
                                if (flag6)
                                {
                                    value.Remove(current2);
                                    return;
                                }
                                Color backColor = Color.FromArgb((int)control.BackColor.R + step2, (int)control.BackColor.G + step3, (int)control.BackColor.B + step4);
                                control.BackColor = backColor;
                                break;
                            }
                            case Properties.Forecolor:
                            {
                                Color color2 = (Color)current2.Value;
                                int r2 = (int)color2.R;
                                int g2 = (int)color2.G;
                                int b2 = (int)color2.B;
                                int step5 = this.GetStep((int)control.ForeColor.R, r2);
                                int step6 = this.GetStep((int)control.ForeColor.G, g2);
                                int step7 = this.GetStep((int)control.ForeColor.B, b2);
                                bool flag7 = Math.Abs(step5) + Math.Abs(step6) + Math.Abs(step7) == 0;
                                if (flag7)
                                {
                                    value.Remove(current2);
                                    return;
                                }
                                Color foreColor = Color.FromArgb((int)control.ForeColor.R + step5, (int)control.ForeColor.G + step6, (int)control.ForeColor.B + step7);
                                control.ForeColor = foreColor;
                                break;
                            }
                            case Properties.Opacity:
                            {
                                bool flag8 = current.Key is Form;
                                if (!flag8)
                                {
                                    value.Remove(current2);
                                    MessageBox.Show("Control不支持透明度属性，只有Form支持。");
                                    return;
                                }
                                Form form = (Form)control;
                                int step = this.GetStep((int)(form.Opacity * 100.0), (int)(Convert.ToDouble(current2.Value) * 100.0));
                                bool flag9 = step == 0;
                                if (flag9)
                                {
                                    value.Remove(current2);
                                    return;
                                }
                                form.Opacity += this.ConvertStepToPersent(step);
                                break;
                            }
                            default:
                                value.Remove(current2);
                                MessageBox.Show("Control不支持[" + current2.Property.ToString() + "]属性。");
                                return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 判断是否已经有了这个属性，可以避免用户连续多次执行某个动画
        /// </summary>
        /// <param name="jobs"></param>
        /// <param name="aJob"></param>
        /// <returns></returns>
        private bool UpdateExistProperty(List<AnimJob> jobs, AnimJob aJob)
        {
            bool result = false;
            foreach (AnimJob current in jobs)
            {
                bool flag = current.Property == aJob.Property;
                if (flag)
                {
                    result = true;
                    current.Value = aJob.Value;
                    break;
                }
            }
            return result;
        }
    }
}
