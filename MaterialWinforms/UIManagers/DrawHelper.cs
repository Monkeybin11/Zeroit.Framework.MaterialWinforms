﻿// ***********************************************************************
// Assembly         : Zeroit.Framework.MaterialWinforms
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 06-18-2018
// ***********************************************************************
// <copyright file="DrawHelper.cs" company="Zeroit Dev Technlologies">
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
using System.Drawing;
using System.Drawing.Drawing2D;

/// <summary>
/// Class DrawHelper.
/// </summary>
static class DrawHelper
{
    /// <summary>
    /// Creates the round rect.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="radius">The radius.</param>
    /// <returns>GraphicsPath.</returns>
    public static GraphicsPath CreateRoundRect(float x, float y, float width, float height, float radius)
    {
        GraphicsPath gp = new GraphicsPath();
        gp.AddLine(x + radius, y, x + width - (radius * 2), y);
        gp.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90);

        gp.AddLine(x + width, y + radius, x + width, y + height - (radius * 2));
        gp.AddArc(x + width - (radius * 2), y + height - (radius * 2), radius * 2, radius * 2, 0, 90);

        gp.AddLine(x + width - (radius * 2), y + height, x + radius, y + height);
        gp.AddArc(x, y + height - (radius * 2), radius * 2, radius * 2, 90, 90);

        gp.AddLine(x, y + height - (radius * 2), x, y + radius);
        gp.AddArc(x, y, radius * 2, radius * 2, 180, 90);

        gp.CloseFigure();
        return gp;
    }
    /// <summary>
    /// Creates up round rect.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="radius">The radius.</param>
    /// <returns>GraphicsPath.</returns>
    public static GraphicsPath CreateUpRoundRect(float x, float y, float width, float height, float radius)
    {
        GraphicsPath gp = new GraphicsPath();

        gp.AddLine(x + radius, y, x + width - (radius * 2), y);
        gp.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90);

        gp.AddLine(x + width, y + radius, x + width, y + height - (radius * 2) + 1);
        gp.AddArc(x + width - (radius * 2), y + height - (radius * 2), radius * 2, 2, 0, 90);

        gp.AddLine(x + width, y + height, x + radius, y + height);
        gp.AddArc(x, y + height - (radius * 2) + 1, radius * 2, 1, 90, 90);

        gp.AddLine(x, y + height, x, y + radius);
        gp.AddArc(x, y, radius * 2, radius * 2, 180, 90);

        gp.CloseFigure();
        return gp;
    }
    /// <summary>
    /// Creates the left round rect.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="radius">The radius.</param>
    /// <returns>GraphicsPath.</returns>
    public static GraphicsPath CreateLeftRoundRect(float x, float y, float width, float height, float radius)
    {
        GraphicsPath gp = new GraphicsPath();
        gp.AddLine(x + radius, y, x + width - (radius * 2), y);
        gp.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90);

        gp.AddLine(x + width, y + 0, x + width, y + height);
        gp.AddArc(x + width - (radius * 2), y + height - (1), radius * 2, 1, 0, 90);

        gp.AddLine(x + width - (radius * 2), y + height, x + radius, y + height);
        gp.AddArc(x, y + height - (radius * 2), radius * 2, radius * 2, 90, 90);

        gp.AddLine(x, y + height - (radius * 2), x, y + radius);
        gp.AddArc(x, y, radius * 2, radius * 2, 180, 90);

        gp.CloseFigure();
        return gp;
    }
    /// <summary>
    /// Blends the color.
    /// </summary>
    /// <param name="backgroundColor">Color of the background.</param>
    /// <param name="frontColor">Color of the front.</param>
    /// <returns>Color.</returns>
    public static Color BlendColor(Color backgroundColor, Color frontColor)
    {
        double ratio = 0 / 255d;
        double invRatio = 1d - ratio;
        int r = (int)((backgroundColor.R * invRatio) + (frontColor.R * ratio));
        int g = (int)((backgroundColor.G * invRatio) + (frontColor.G * ratio));
        int b = (int)((backgroundColor.B * invRatio) + (frontColor.B * ratio));
        return Color.FromArgb(r, g, b);
    }
}
