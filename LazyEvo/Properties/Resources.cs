namespace LazyEvo.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, DebuggerNonUserCode, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    internal class Resources
    {
        private static System.Resources.ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        internal Resources()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (ReferenceEquals(resourceMan, null))
                {
                    resourceMan = new System.Resources.ResourceManager("LazyEvo.Properties.Resources", typeof(Resources).Assembly);
                }
                return resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get => 
                resourceCulture;
            set => 
                resourceCulture = value;
        }

        internal static Bitmap Disable_ctm =>
            (Bitmap) ResourceManager.GetObject("Disable_ctm", resourceCulture);

        internal static Bitmap Enable_auto_loot =>
            (Bitmap) ResourceManager.GetObject("Enable_auto_loot", resourceCulture);

        internal static Bitmap Enable_auto_selfcast =>
            (Bitmap) ResourceManager.GetObject("Enable_auto_selfcast", resourceCulture);

        internal static Bitmap Interact_with_mouseover =>
            (Bitmap) ResourceManager.GetObject("Interact_with_mouseover", resourceCulture);

        internal static Bitmap Interact_with_target =>
            (Bitmap) ResourceManager.GetObject("Interact_with_target", resourceCulture);

        internal static Bitmap Reset_keybindings =>
            (Bitmap) ResourceManager.GetObject("Reset_keybindings", resourceCulture);

        internal static Bitmap Reset_keybindings1 =>
            (Bitmap) ResourceManager.GetObject("Reset_keybindings1", resourceCulture);

        internal static Bitmap Target_last_target =>
            (Bitmap) ResourceManager.GetObject("Target_last_target", resourceCulture);

        internal static Bitmap Target_last_target1 =>
            (Bitmap) ResourceManager.GetObject("Target_last_target1", resourceCulture);
    }
}

