using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace AeroSuite.Forms
{
    /// <summary>
    /// A <see cref="Form"/> base class which allows the creation of borderless windows which supports AeroSnap, AeroPeek, the system window context menu, optionally an Aero shadow, all of Windows' window animations and even windows-like dragging & resizing in specified areas.
    /// </summary>
    /// <remarks>
    /// The various features of this borderless form are implemented by using a normal window as a base and removing all of its "features" like the titlebar and borders visually and functionally during runtime by handling the appropriate windows messages.
    /// That makes this variant of a borderless window superior to just setting the border style to <see cref="FormBorderStyle.None"/> which lacks all of the features described in the summary.
    /// To implement the various areas for resizing & dragging, you have to override the <see cref="BorderlessForm.PerformHitTest(Point)"/>-method and check for the areas you desire.
    /// </remarks>
    [DesignerCategory("Code")]
    [DisplayName("Borderless Form")]
    [Description("A borderless form with support")]
    public class BorderlessForm
        : Form
    {
        private const int CS_DROPSHADOW = 0x20000;

        /// <summary>
        /// Initializes a new instance of the <see cref="BorderlessForm"/> class.
        /// </summary>
        public BorderlessForm()
        {
            base.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = SystemColors.Window;
            this.Font = SystemFonts.MessageBoxFont;
        }

        /// <summary>
        /// Gets or sets the border style of the form.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(FormBorderStyle.None)]
        protected new virtual FormBorderStyle FormBorderStyle
        {
            get
            {
                return this.Borderless ? FormBorderStyle.None : FormBorderStyle.Sizable;
            }
            set
            {
                switch (value)
                {
                    case FormBorderStyle.None:
                        this.Borderless = true;
                        break;
                    default:
                        this.borderless = false;
                        break;
                }
            }
        }

        private bool borderless = true;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BorderlessForm"/> should be displayed without a window border.
        /// </summary>
        /// <value>
        ///   <c>true</c> if borderless; otherwise, <c>false</c>.
        /// </value>
        [Bindable(true)]
        [DefaultValue(true)]
        [Category("Appearance")]
        [Description("Determines, whether this form should be displayed without a window border.")]
        public virtual bool Borderless
        {
            get
            {
                return this.borderless;
            }
            set
            {
                this.borderless = value;
                base.FormBorderStyle = value ? FormBorderStyle.None : FormBorderStyle.Sizable;
                //if (this.IsHandleCreated)
                //{
                //    this.UpdateStyle();
                //}
            }
        }

        private bool showShadow = true;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BorderlessForm"/> should be displayed with an aero shadow underneath. This only applies when the form is borderless.
        /// </summary>
        /// <value>
        ///   <c>true</c> if a shadow is cast; otherwise, <c>false</c>.
        /// </value>
        [Bindable(true)]
        [DefaultValue(true)]
        [Category("Appearance")]
        [Description("Determines, whether this form should be displayed with an aero shadow underneath. This only applies when the form is borderless.")]
        public virtual bool Shadow
        {
            get
            {
                return this.showShadow;
            }
            set
            {
                this.showShadow = value;
                if (this.IsHandleCreated)
                {
                    //this.UpdateStyle();
                    this.RecreateHandle();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the default behaviour when clicking on the form is to drag it or to ignore the action. The effect of this property can change if <see cref="BorderlessForm.PerformHitTest(Point)"/> is overriden.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the form is automatically dragged when the cursor clicks on it; otherwise, <c>false</c>.
        /// </value>
        [Bindable(true)]
        [DefaultValue(true)]
        [Category("Behaviour")]
        [Description("Determines, whether the default behaviour when clicking on the form is to drag it or to ignore the action.")]
        public virtual bool AutoDrag { get; set; } = true;



        /// <summary>
        /// Performs a hit-test which determines how the form behaves when the cursor hovers or clicks on certain areas of the form. Override this method to customize how this form handles those events.
        /// </summary>
        /// <param name="location">The location of the cursor.</param>
        /// <returns>A <see cref="HitTestResult"/> determining the area underneath the cursor and therefore the forms behaviour at that location.</returns>
        protected virtual HitTestResult PerformHitTest(Point location)
        {
            return this.AutoDrag ? HitTestResult.Caption : HitTestResult.Client;
        }

        /// <summary>
        /// Creates an unsigned integer determining the window style of this <see cref="BorderlessForm"/>.
        /// </summary>
        /// <returns></returns>
        protected virtual uint GetWindowStyle()
        {
            if (this.Borderless)
            {
                if (PlatformHelper.VistaOrHigher && NativeMethods.DwmIsCompositionEnabled())
                {
                    return (uint)(WindowStyles.Overlapped | WindowStyles.ThickFrame | WindowStyles.Caption | WindowStyles.SysMenu | WindowStyles.MinimizeBox | WindowStyles.MaximizeBox);
                }
                else
                {
                    return (uint)(WindowStyles.Overlapped | WindowStyles.ThickFrame | WindowStyles.SysMenu);
                }
            }
            else
            {
                //return (uint)(WindowStyles.Popup | WindowStyles.ThickFrame | WindowStyles.Caption | WindowStyles.SysMenu | WindowStyles.MinimizeBox | WindowStyles.MaximizeBox);
                return (uint)base.CreateParams.Style;
            }
        }

        /// <summary>
        /// Initializes the shadow underneath this <see cref="BorderlessForm"/>. Only call this method if <see cref="BorderlessForm.Borderless"/> is set to <c>true</c>, otherwise it might cause undefined behaviour.
        /// </summary>
        protected virtual void InitializeShadow()
        {
            if (this.Shadow && this.Borderless)
            {
                if (PlatformHelper.VistaOrHigher && NativeMethods.DwmIsCompositionEnabled())
                {
                    if (this.Shadow)
                    {
                        Margins margins = new Margins() { BottomHeight = 1, LeftWidth = 1, RightWidth = 1, TopHeight = 1 };
                        NativeMethods.DwmExtendFrameIntoClientArea(this.Handle, ref margins);
                    }
                    else
                    {
                        Margins margins = new Margins() { BottomHeight = 0, LeftWidth = 0, RightWidth = 0, TopHeight = 0 };
                    }
                }
            }
        }

        ///// <summary>
        ///// Updates the style of this <see cref="BorderlessForm"/>. This method is called after every custom property change which affects the visual appearance. Try calling this method if you try to change something about the form and it does not work as expected.
        ///// </summary>
        //protected virtual void UpdateStyle()
        //{
        //    //This should mostly work but it causes some visual errors to occur and failes to load the form's icon so I'm going to disable this option for now and use the quick and not even so dirty alternative way
        //    //if (IntPtr.Size == 8)
        //    //{
        //    //    NativeMethods.SetWindowLongPtr64(this.Handle, GWL_STYLE, new IntPtr(unchecked((int)this.GetWindowStyle())));
        //    //}
        //    //else
        //    //{
        //    //    NativeMethods.SetWindowLong32(this.Handle, GWL_STYLE, new IntPtr(unchecked((int)this.GetWindowStyle())));
        //    //}
        //
        //    //if (this.Borderless)
        //    //{
        //    //    this.InitializeShadow();
        //    //}
        //
        //    //NativeMethods.SetWindowPos(this.Handle, IntPtr.Zero, 0, 0, 0, 0, SWP_FRAMECHANGED | SWP_NOMOVE | SWP_NOSIZE);
        //    //this.Show();
        //}



        /// <summary>
        /// Gets the required creation parameters when the control handle is created.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                if (this.DesignMode)
                {
                    return base.CreateParams;
                }

                CreateParams baseParams = base.CreateParams;
                base.CreateParams.Style = unchecked((int)this.GetWindowStyle());

                if (this.Borderless)
                {
                    this.InitializeShadow();

                    if (!PlatformHelper.VistaOrHigher || !NativeMethods.DwmIsCompositionEnabled())
                    {
                        baseParams.ClassStyle |= CS_DROPSHADOW;
                    }
                }

                return baseParams;
            }
        }

        /// <summary>
        /// Processes Windows messages.
        /// </summary>
        /// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
        protected override void WndProc(ref Message m)
        {
            if (!this.DesignMode)
            {
                switch (m.Msg)
                {
                    case (int)WindowsMessages.NonClientCalcSize:
                        if (this.Borderless)
                        {
                            m.Result = IntPtr.Zero;
                            return;
                        }
                        break;
                    case (int)WindowsMessages.NonClientHitTest:
                        if (this.Borderless)
                        {
                            m.Result = new IntPtr((int)this.PerformHitTest(new Point(unchecked((short)m.LParam.ToInt64()), unchecked((short)(m.LParam.ToInt64() >> 16)))));
                        }
                        else
                        {
                            base.WndProc(ref m);
                            if (this.AutoDrag && m.Result.ToInt32() == (int)HitTestResult.Client)
                            {
                                m.Result = new IntPtr((int)HitTestResult.Caption);
                            }
                        }
                        return;
                    case (int)WindowsMessages.NonClientActivate:
                        if (this.Borderless && (!PlatformHelper.VistaOrHigher || !NativeMethods.DwmIsCompositionEnabled()))
                        {
                            m.Result = new IntPtr(1);
                            return;
                        }
                        break;
                    case (int)WindowsMessages.ThemeChanged:
                    case (int)WindowsMessages.DwmCompositionChanged:
                        this.RecreateHandle();
                        break;
                }
            }

            base.WndProc(ref m);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            if (this.Borderless && (!PlatformHelper.VistaOrHigher || !NativeMethods.DwmIsCompositionEnabled()))
            {
                this.Invalidate();
            }

            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if (this.Borderless && (!PlatformHelper.VistaOrHigher || !NativeMethods.DwmIsCompositionEnabled()))
            {
                this.Invalidate();
            }

            base.OnLostFocus(e);
        }



        [StructLayout(LayoutKind.Sequential)]
        protected internal struct Margins
        {
            public int LeftWidth;
            public int RightWidth;
            public int TopHeight;
            public int BottomHeight;
        }

        protected internal enum WindowStyles
            : uint
        {
            Overlapped = 0,
            MaximizeBox = 0x10000,
            MinimizeBox = 0x20000,
            ThickFrame = 0x40000,
            SysMenu = 0x80000,
            Caption = 0xC00000,
            Popup = 0x80000000,
        }

        protected internal enum WindowsMessages
            : int
        {
            NonClientCalcSize = 0x83,
            NonClientHitTest = 0x84,
            NonClientActivate = 0x86,
            ThemeChanged = 0x31A,
            DwmCompositionChanged = 0x31E
        }
    }

    public enum HitTestResult
        : int
    {
        Nowhere = 0,
        Client = 1,
        Caption = 2,
        Left = 10,
        Right = 11,
        Top = 12,
        TopLeft = 13,
        TopRight = 14,
        Bottom = 15,
        BottomLeft = 16,
        BottomRight = 17,
        Border = 18
    }
}
