using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeroSuite
{
    /// <summary>
    /// Provides information about the platform support of a specific feature
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    public sealed class PlatformSupportAttribute
        : Attribute
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformSupportAttribute"/> class.
        /// </summary>
        /// <param name="platforms">The platforms this attribute applies to.</param>
        public PlatformSupportAttribute(Platform platforms, PlatformSupportType supportType)
        {
            this.Platforms = platforms;
            this.SupportType = supportType;
        }

        /// <summary>
        /// Returns the platforms this attribute applies to.
        /// </summary>
        /// <value>
        /// The platforms.
        /// </value>
        public Platform Platforms { get; protected set; }

        /// <summary>
        /// Returns the type of support.
        /// </summary>
        /// <value>
        /// The type of support.
        /// </value>
        public PlatformSupportType SupportType { get; protected set; }
    }

    /// <summary>
    /// Describes a platform, used for the <see cref="PlatformSupportAttribute"/> class.
    /// </summary>
    [Flags]
    public enum Platform
    {
        WindowsAero = WindowsVista | Windows7 | Windows8 | Windows10,
        Windows = WindowsClassic | WindowsXP | WindowsVista | Windows7 | Windows8 | Windows10,
        All = Windows | LinuxMono,

        WindowsClassic = 1,
        WindowsXP = 2,
        WindowsVista = 4,
        Windows7 = 8,
        Windows8 = 16,
        Windows10 = 32,
        LinuxMono = 64,
    }

    /// <summary>
    /// Describes the way a platform is supported, used for the <see cref="PlatformSupportAttribute"/> class.
    /// </summary>
    public enum PlatformSupportType
    {
        Unknown,
        Native,
        Workaround,
        Partial,
        NotSupported
    }
}
