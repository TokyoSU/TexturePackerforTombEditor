using System;
using System.Drawing;
using System.Windows.Forms;

namespace TexturePacker
{
    public static class ControlExtension
    {
        public static void UpdateThreaded(this Label label, string text)
        {
            Action action = () => label.Text = text;
            label.Invoke(action);
        }

        public static void RefreshThreaded(this Label label)
        {
            Action action = label.Refresh;
            label.Invoke(action);
        }

        public static void UpdateThreaded(this ProgressBar bar, int value)
        {
            Action action = () => bar.Value = value;
            bar.Invoke(action);
        }

        public static void RefreshThreaded(this ProgressBar bar)
        {
            Action action = bar.Refresh;
            bar.Invoke(action);
        }

        public static void AddThreaded(this ListView view, ImageList list, string index, Image value)
        {
            Action action = () =>
            {
                list.Images.Add(index, value);
                var item = view.Items.Add(index);
                item.ImageKey = index;
                view.Refresh();
                view.Update();
            };
            view.Invoke(action);
        }

        public static bool ContainsThreaded(this ListView view, ImageList list, string index)
        {
            Func<bool> action = () =>
            {
                return list.Images.ContainsKey(index);
            };
            return (bool)view.Invoke(action);
        }

        public static void UpdateThreaded(this ListView view)
        {
            Action action = view.Update;
            view.Invoke(action);
        }

        public static void RefreshThreaded(this ListView view)
        {
            Action action = view.Refresh;
            view.Invoke(action);
        }
    }
}
