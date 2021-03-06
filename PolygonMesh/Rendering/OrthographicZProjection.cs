﻿/*
Copyright (c) 2014, Lars Brubaker
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

The views and conclusions contained in the software and documentation are those
of the authors and should not be interpreted as representing official policies,
either expressed or implied, of the FreeBSD Project.
*/

using System;
using MatterHackers.Agg;
using MatterHackers.Agg.VertexSource;
using MatterHackers.VectorMath;

namespace MatterHackers.PolygonMesh.Rendering
{
	public static class OrthographicZProjection
	{
		public static void DrawTo(Graphics2D graphics2D, Mesh mesh, Matrix4X4 matrix, Vector2 offset, double scale, Color silhouetteColor)
		{
			graphics2D.Rasterizer.gamma(new gamma_power(.3));
			VertexStorage polygonProjected = new VertexStorage();
			for (int i = 0; i < mesh.Faces.Count; i++)
			{
				var face = mesh.Faces[i];
				if (mesh.Faces[i].normal.TransformNormal(matrix).Z > 0)
				{
					polygonProjected.remove_all();

					polygonProjected.MoveTo((new Vector2(mesh.Vertices[face.v0].Transform(matrix)) + offset) * scale);
					polygonProjected.LineTo((new Vector2(mesh.Vertices[face.v1].Transform(matrix)) + offset) * scale);
					polygonProjected.LineTo((new Vector2(mesh.Vertices[face.v2].Transform(matrix)) + offset) * scale);
				}
				graphics2D.Render(polygonProjected, silhouetteColor);
			}
			graphics2D.Rasterizer.gamma(new gamma_none());
		}
	}
}