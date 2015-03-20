using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using AeroSuite.Controls;

namespace AeroSuite.Test.TestAdapters
{
    public static class TestAdapters
    {
        private static Dictionary<Type, Type> testAdapters = Assembly.GetAssembly(typeof(TestAdapters)).GetTypes().Where(t => t.IsClass && !t.IsAbstract && typeof(TestAdapter).IsAssignableFrom(t) && t.BaseType.GetGenericArguments().Length == 1).ToDictionary(t => t.BaseType.GetGenericArguments()[0], t => t);
        public static TestAdapter Item(Control control)
        {
            Type testAdapterType;
            if (testAdapters.TryGetValue(control.GetType(), out testAdapterType))
            {
                return testAdapterType.GetConstructor(new Type[] { control.GetType() }).Invoke(new object[] {control}) as TestAdapter;
            }
            return null;
        }
    }

    public class AeroListViewTestAdapter
        : TestAdapterBase<AeroListView>
    {
        public AeroListViewTestAdapter(AeroListView control) : base(control)
        {
            var smallImageList = new ImageList() { ColorDepth = ColorDepth.Depth32Bit, ImageSize = new Size(16, 16) };
            var largeImageList = new ImageList() { ColorDepth = ColorDepth.Depth32Bit, ImageSize = new Size(48, 48) };

            try
            {
                smallImageList.Images.Add(new Icon(Properties.Resources.All_Users_Folder, new Size(16, 16)).ToBitmap());
                smallImageList.Images.Add(new Icon(Properties.Resources.Pictures, new Size(16, 16)).ToBitmap());
                smallImageList.Images.Add(new Icon(Properties.Resources.Recycle_Bin, new Size(16, 16)).ToBitmap());
                smallImageList.Images.Add(new Icon(Properties.Resources.UAC, new Size(16, 16)).ToBitmap());
                smallImageList.Images.Add(new Icon(Properties.Resources.Videos, new Size(16, 16)).ToBitmap());

                largeImageList.Images.Add(new Icon(Properties.Resources.All_Users_Folder, new Size(48, 48)).ToBitmap());
                largeImageList.Images.Add(new Icon(Properties.Resources.Pictures, new Size(48, 48)).ToBitmap());
                largeImageList.Images.Add(new Icon(Properties.Resources.Recycle_Bin, new Size(48, 48)).ToBitmap());
                largeImageList.Images.Add(new Icon(Properties.Resources.UAC, new Size(48, 48)).ToBitmap());
                largeImageList.Images.Add(new Icon(Properties.Resources.Videos, new Size(48, 48)).ToBitmap());
            }
            catch { }

            control.SmallImageList = smallImageList;
            control.LargeImageList = largeImageList;
            control.Items.AddRange(new string[] { "First Item", "Second Item", "Third Item", "Fourth Item", "Fifth Item" }.Select((s, i) => new ListViewItem(s, i)).ToArray());
            control.Size = new Size(400, 300);
        }
    }

    public class AeroProgressBarTestAdapter
        : TestAdapterBase<AeroProgressBar>
    {
        public AeroProgressBarTestAdapter(AeroProgressBar control) : base(control)
        {
            control.Value = 66;
        }
    }

    public class VerticalAeroProgressBarTestAdapter
        : TestAdapterBase<VerticalAeroProgressBar>
    {
        public VerticalAeroProgressBarTestAdapter(VerticalAeroProgressBar control) : base(control)
        {
            control.Value = 66;
        }
    }

    public class AeroTreeViewTestAdapter
        : TestAdapterBase<AeroTreeView>
    {
        public AeroTreeViewTestAdapter(AeroTreeView control) : base(control)
        {
            var imageList = new ImageList() { ColorDepth = ColorDepth.Depth32Bit, ImageSize = new Size(16, 16) };
            try
            {
                imageList.Images.Add(new Icon(Properties.Resources.All_Users_Folder, new Size(16, 16)).ToBitmap());
                imageList.Images.Add(new Icon(Properties.Resources.Pictures, new Size(16, 16)).ToBitmap());
                imageList.Images.Add(new Icon(Properties.Resources.Recycle_Bin, new Size(16, 16)).ToBitmap());
                imageList.Images.Add(new Icon(Properties.Resources.UAC, new Size(16, 16)).ToBitmap());
                imageList.Images.Add(new Icon(Properties.Resources.Videos, new Size(16, 16)).ToBitmap());
            }
            catch { }

            control.ImageList = imageList;
            TreeNode root = new TreeNode("Root", 0, 0, new TreeNode[] { new TreeNode("First Child", 1, 1, new TreeNode[] { new TreeNode("Second Child", 2, 2), new TreeNode("Third Child", 3, 3) }), new TreeNode("Fourth Child", 4, 4), });
            control.Nodes.Add(root);
            control.ExpandAll();
            control.Size = new Size(175, 100);
        }
    }

    public class CaptionLabelTestAdapter
        : TestAdapterBase<CaptionLabel>
    {
        public CaptionLabelTestAdapter(CaptionLabel control) : base(control)
        {
            control.Font = new Font(SystemFonts.MessageBoxFont.FontFamily, control.Font.Size);
        }
    }

    public class CommandLinkTestAdapter
        : TestAdapterBase<CommandLink>
    {
        public CommandLinkTestAdapter(CommandLink control) : base(control)
        {
            //Make it display a note for testing
            control.Height = 60;
            control.Note = "Test Note";
        }
    }

    public class CueComboBoxTestAdapter
        : TestAdapterBase<CueComboBox>
    {
        public CueComboBoxTestAdapter(CueComboBox control) : base(control)
        {
            control.Text = "";
            control.Cue = "No Item selected";
            control.Items.AddRange(new string[] { "First Item", "Second Item", "Third Item", "Fourth Item", "Fifth Item" });
        }
    }

    public class CueTextBoxTestAdapter
        : TestAdapterBase<CueTextBox>
    {
        public CueTextBoxTestAdapter(CueTextBox control) : base(control)
        {
            control.Text = "";
            control.Cue = "Search";
        }
    }

    public class HeaderlessTabControlTestAdapter
        : TestAdapterBase<HeaderlessTabControl>
    {
        public HeaderlessTabControlTestAdapter(HeaderlessTabControl control) : base(control)
        {
            control.TabPages.AddRange(new TabPage[] { new TabPage("First TabPage"), new TabPage("Second TabPage"), new TabPage("Third TabPage") });
        }
    }
}
