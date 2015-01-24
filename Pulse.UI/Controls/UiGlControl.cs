﻿using System;
using System.Windows.Forms.Integration;
using OpenTK;
using OpenTK.Graphics;
using Pulse.OpenGL;

namespace Pulse.UI.Controls
{
    public class UiGlControl : WindowsFormsHost
    {
        public GLControl Control { get; private set; }

        public UiGlControl()
        {
            Control = new GLControl(GraphicsMode.Default);
            Child = Control;
        }

        public IDisposable AcquireContext()
        {
            return GLService.AcquireContext(Control.WindowInfo);
        }

        public void SwapBuffers()
        {
            Control.Context.SwapBuffers();
        }
    }
}