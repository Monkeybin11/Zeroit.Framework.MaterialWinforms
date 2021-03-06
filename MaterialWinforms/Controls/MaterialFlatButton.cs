﻿// ***********************************************************************
// Assembly         : Zeroit.Framework.MaterialWinforms
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 06-22-2018
// ***********************************************************************
// <copyright file="MaterialFlatButton.cs" company="Zeroit Dev Technlologies">
//    This program is for creating Material Design controls.
//    Copyright ©  2017  Zeroit Dev Technologies
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
//    You can contact me at zeroitdevnet@gmail.com or zeroitdev@outlook.com
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using Zeroit.Framework.MaterialWinforms.Animations;

namespace Zeroit.Framework.MaterialWinforms.Controls
{
    /// <summary>
    /// Class MaterialFlatButton.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Button" />
    /// <seealso cref="Zeroit.Framework.MaterialWinforms.IMaterialControl" />
    public class MaterialFlatButton : Button, IMaterialControl
    {
        /// <summary>
        /// Gets or sets the depth.
        /// </summary>
        /// <value>The depth.</value>
        [Browsable(false)]
        public int Depth { get; set; }
        /// <summary>
        /// Gets the skin manager.
        /// </summary>
        /// <value>The skin manager.</value>
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        /// <summary>
        /// Gets or sets the state of the mouse.
        /// </summary>
        /// <value>The state of the mouse.</value>
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MaterialFlatButton"/> is primary.
        /// </summary>
        /// <value><c>true</c> if primary; otherwise, <c>false</c>.</value>
        public bool Primary { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MaterialFlatButton"/> is accent.
        /// </summary>
        /// <value><c>true</c> if accent; otherwise, <c>false</c>.</value>
        public bool Accent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MaterialFlatButton"/> is selected.
        /// </summary>
        /// <value><c>true</c> if selected; otherwise, <c>false</c>.</value>
        [Browsable(false)]
        public bool Selected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MaterialFlatButton"/> is capitalized.
        /// </summary>
        /// <value><c>true</c> if capitalized; otherwise, <c>false</c>.</value>
        public bool Capitalized { get; set; }

        /// <summary>
        /// Gets or sets the background color of the control.
        /// </summary>
        /// <value>The color of the back.</value>
        public new Color BackColor { get { return Parent == null ? SkinManager.GetApplicationBackgroundColor() : typeof(IShadowedMaterialControl).IsAssignableFrom(Parent.GetType()) ? ((IMaterialControl)Parent).BackColor : Parent.BackColor; } }

        /// <summary>
        /// The animation manager
        /// </summary>
        private readonly AnimationManager animationManager;
        /// <summary>
        /// The hover animation manager
        /// </summary>
        private readonly AnimationManager hoverAnimationManager;

        /// <summary>
        /// The text size
        /// </summary>
        private SizeF textSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialFlatButton"/> class.
        /// </summary>
        public MaterialFlatButton()
        {
            Primary = false;
            Accent = false;
            Capitalized = true;

            animationManager = new AnimationManager(false)
            {
                Increment = 0.03,
                AnimationType = AnimationType.EaseOut
            };
            hoverAnimationManager = new AnimationManager
            {
                Increment = 0.07,
                AnimationType = AnimationType.Linear
            };

            hoverAnimationManager.OnAnimationProgress += sender => Invalidate();
            animationManager.OnAnimationProgress += sender => Invalidate();

            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AutoSize = true;
            Margin = new Padding(4, 6, 4, 6);
            Padding = new Padding(0);
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                if (String.IsNullOrEmpty(value))
                {
                    textSize = new Size(5, 5);
                }
                else
                {
                textSize = CreateGraphics().MeasureString(value.ToUpper(), SkinManager.ROBOTO_MEDIUM_10);
                }
                if(IconImage!= null)
                textSize = new Size((int)textSize.Width + (int)ClientRectangle.Height, (int)textSize.Height);
                if (AutoSize)
                    Size = GetPreferredSize();
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the icon image.
        /// </summary>
        /// <value>The icon image.</value>
        public Image IconImage
        {
            get { return base.Image; }
            set
            {
                base.Image = value;
                textSize = CreateGraphics().MeasureString(Text.ToUpper(), SkinManager.ROBOTO_MEDIUM_10);
                if (IconImage != null)
                textSize = new Size((int)textSize.Width + (int)textSize.Height, (int)textSize.Height);
                if (AutoSize)
                    Size = GetPreferredSize();
                Invalidate();
            }
        }

        /// <summary>
        /// Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnPaint(System.Windows.Forms.PaintEventArgs)" /> event.
        /// </summary>
        /// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs pevent)
        {
            bool ImageDrawn = false;
            var g = pevent.Graphics;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            g.Clear(BackColor);

            if (Image != null)
            {
                ImageDrawn = true;
                g.DrawImage(Image, 8, 2, Height-4, Height-4);
            }

            if (Selected)
            {
                    g.FillRectangle(SkinManager.GetFlatButtonHoverBackgroundBrush(), ClientRectangle);
            }

            //Hover
            Color c = SkinManager.GetFlatButtonHoverBackgroundColor();
            using (Brush b = new SolidBrush(Color.FromArgb((int)(hoverAnimationManager.GetProgress() * c.A), c.RemoveAlpha())))
                g.FillRectangle(b, ClientRectangle);

            //Ripple
            if (animationManager.IsAnimating())
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                for (int i = 0; i < animationManager.GetAnimationCount(); i++)
                {
                    var animationValue = animationManager.GetProgress(i);
                    var animationSource = animationManager.GetSource(i);

                    using (Brush rippleBrush = new SolidBrush(Color.FromArgb((int)(101 - (animationValue * 100)), Color.Black)))
                    {
                        var rippleSize = (int)(animationValue * Width * 2);
                        g.FillEllipse(rippleBrush, new Rectangle(animationSource.X - rippleSize / 2, animationSource.Y - rippleSize / 2, rippleSize, rippleSize));
                    }
                }
                g.SmoothingMode = SmoothingMode.None;
            }
            g.DrawString(Capitalized ?Text.ToUpper():Text, SkinManager.ROBOTO_MEDIUM_10, Enabled ? (Primary ? SkinManager.ColorScheme.PrimaryBrush : Accent ? SkinManager.ColorScheme.AccentBrush : SkinManager.GetPrimaryTextBrush()) : SkinManager.GetFlatButtonDisabledTextBrush(), ClientRectangle, new StringFormat { Alignment = (IconImage == null ? StringAlignment.Center : StringAlignment.Far), LineAlignment = StringAlignment.Center });
        }

        /// <summary>
        /// Gets the size of the preferred.
        /// </summary>
        /// <returns>Size.</returns>
        private Size GetPreferredSize()
        {
            return GetPreferredSize(new Size(0, 0));
        }

        /// <summary>
        /// Retrieves the size of a rectangular area into which a control can be fitted.
        /// </summary>
        /// <param name="proposedSize">The custom-sized area for a control.</param>
        /// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
        public override Size GetPreferredSize(Size proposedSize)
        {
            return new Size((int) textSize.Width + 8, 36);
        }

        /// <summary>
        /// Raises the <see cref="M:System.Windows.Forms.Control.CreateControl" /> method.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (DesignMode) return;

            MouseState = MouseState.OUT;
            MouseEnter += (sender, args) =>
            {
                MouseState = MouseState.HOVER;
                hoverAnimationManager.StartNewAnimation(AnimationDirection.In);
                Invalidate();
            };
            MouseLeave += (sender, args) =>
            {
                MouseState = MouseState.OUT;
                hoverAnimationManager.StartNewAnimation(AnimationDirection.Out);
                Invalidate();
            };
            MouseDown += (sender, args) =>
            {
                if (args.Button == MouseButtons.Left)
                {
                    MouseState = MouseState.DOWN;

                    animationManager.StartNewAnimation(AnimationDirection.In, args.Location);
                    Invalidate();
                }
            };
            MouseUp += (sender, args) =>
            {
                MouseState = MouseState.HOVER;

                Invalidate();
            };
        }
    }
}
