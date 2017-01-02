using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace AeroSuite.Controls
{
    /// <summary>
    /// A TabControl-style control that does not have headers.
    /// </summary>
    /// <remarks>
    /// Instead of using the usual approach (suppressing the TCM_ADJUSTRECT message), I redid the TabControl completely to eliminate any bugs and to make it work on every platform.
    /// </remarks>
    [DesignerCategory("Code")]
    [DisplayName("Headerless Tab Control")]
    [Description("A TabControl-style control that does not have headers.")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(HeaderlessTabControl))]
    public class HeaderlessTabControl
        : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderlessTabControl"/> class.
        /// </summary>
        public HeaderlessTabControl()
        {
            this.TabPages = new ObservableCollection<HeaderlessTabPage>
            {
                new HeaderlessTabPage()
            };
        }

        private ObservableCollection<HeaderlessTabPage> tabPages;
        /// <summary>
        /// Returns the collection of tab pages in this tab control.
        /// </summary>
        /// <value>
        /// The tab pages.
        /// </value>
        [Category("Appearance")]
        [Description("The tab pages in this tab control.")]
        [Localizable(true)]
        [Bindable(true)]
        public ObservableCollection<HeaderlessTabPage> TabPages
        {
            get
            {
                return this.tabPages;
            }
            private set
            {
                if (this.tabPages != null)
                {
                    this.tabPages.CollectionChanged -= this.OnTabPageCollectionChanged;
                }

                this.tabPages = value;
                this.tabPages.CollectionChanged += this.OnTabPageCollectionChanged;
            }
        }

        /// <summary>
        /// Called when the tab page collection has been changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnTabPageCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.UpdateTabPage();
        }

        /// <summary>
        /// Gets or sets the update tab page.
        /// </summary>
        /// <value>
        /// The update tab page.
        /// </value>
        protected virtual void UpdateTabPage()
        {
            //Remove old tab page
            this.Controls.OfType<HeaderlessTabPage>().FirstOrDefault().DoIf(tab => tab != null, tab => this.Controls.Remove(tab));
            //Add tab to form if one is selected
            this.SelectedTab.DoIf(tab => tab != null, tab => {
                tab.Dock = DockStyle.Fill;
                this.Controls.Add(tab);
            });
        }

        private int selectedIndex = -1;
        /// <summary>
        /// Gets the index of the selected tab.
        /// </summary>
        /// <value>
        /// The index of the selected tab.
        /// </value>
        public virtual int SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }
            set
            {
                this.selectedIndex = value;
                this.UpdateTabPage();
            }
        }

        /// <summary>
        /// Returns the selected tab.
        /// </summary>
        /// <value>
        /// The selected tab.
        /// </value>
        /// <exception cref="System.Exception">SelectedIndex returned an invalid index.</exception>
        public virtual HeaderlessTabPage SelectedTab
        {
            get
            {
                if (this.SelectedIndex == -1)
                {
                    return null;
                }
                else if (this.SelectedIndex >= this.TabPages.Count)
                {
                    throw new Exception("SelectedIndex returned an invalid index.");
                }
                else
                {
                    return this.TabPages[this.SelectedIndex];
                }
            }
            set
            {
                this.SelectedIndex = this.TabPages.IndexOf(value);
            }
        }

        /// <summary>
        /// Extends the design mode behaviour of a <see cref="HeaderlessTabControl"/>.
        /// </summary>
        internal class HeaderlessTabControlDesigner
            : ParentControlDesigner
        {
            /// <summary>
            /// Initializes the designer with the specified component.
            /// </summary>
            /// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to associate with the designer.</param>
            public override void Initialize(IComponent component)
            {
                base.Initialize(component);

                if (component is HeaderlessTabControl)
                {
                    HeaderlessTabControl tabControl = component as HeaderlessTabControl;
                    this.EnableDesignMode(tabControl.SelectedTab, "SelectedTab");
                }
            }
        }
    }
}
