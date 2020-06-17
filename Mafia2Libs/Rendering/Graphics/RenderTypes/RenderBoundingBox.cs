﻿using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;

namespace Rendering.Graphics
{
    public class RenderBoundingBox : IRenderer
    {
        private VertexLayouts.BasicLayout.Vertex[] vertices;
        private ushort[] indices;
        private Vector4 colour;
        private BoundingBox bbox;
        public BoundingBox BBox { get { return bbox; } }

        public RenderBoundingBox()
        {
            DoRender = true;
            SetTransform(Matrix.Identity);
            colour = new Vector4(1.0f);
        }

        public bool Init(BoundingBox bbox)
        {
            this.bbox = bbox;

            vertices = new VertexLayouts.BasicLayout.Vertex[8];

            Vector3[] corners = bbox.GetCorners();
            for(int i = 0; i < corners.Length; i++)
            {
                vertices[i] = new VertexLayouts.BasicLayout.Vertex();
                vertices[i].Position = corners[i];
                vertices[i].Colour = colour;
            }

            indices = new ushort[] {
                0, 1, 1, 2, 2, 3, 3, 0, // Front edges
                4, 5, 5, 6, 6, 7, 7, 4, // Back edges
                0, 4, 1, 5, 2, 6, 3, 7 // Side edges connecting front and back
            };

            shader = RenderStorageSingleton.Instance.ShaderManager.shaders[1];
            return true;
        }

        public void Update(BoundingBox box)
        {
            isUpdatedNeeded = true;
            Init(box);
        }

        public override void InitBuffers(Device d3d, DeviceContext context)
        {
            vertexBuffer = Buffer.Create(d3d, BindFlags.VertexBuffer, vertices, 0, ResourceUsage.Dynamic, CpuAccessFlags.Write);
            indexBuffer = Buffer.Create(d3d, BindFlags.IndexBuffer, indices, 0, ResourceUsage.Dynamic, CpuAccessFlags.Write);
        }

        public void SetColour(Vector4 vec)
        {
            colour = vec;
        }

        public override void SetTransform(Matrix matrix)
        {
            this.Transform = matrix;
        }

        public override void Render(Device device, DeviceContext deviceContext, Camera camera)
        {
            if (!DoRender)
                return;

            deviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertexBuffer, Utilities.SizeOf<VertexLayouts.BasicLayout.Vertex>(), 0));
            deviceContext.InputAssembler.SetIndexBuffer(indexBuffer, SharpDX.DXGI.Format.R16_UInt, 0);
            deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.LineList;

            shader.SetSceneVariables(deviceContext, Transform, camera);
            shader.Render(deviceContext, PrimitiveTopology.LineList, indices.Length, 0);
        }

        public override void Shutdown()
        {
            vertices = null;
            indices = null;
            indexBuffer?.Dispose();
            indexBuffer = null;
            vertexBuffer?.Dispose();
            vertexBuffer = null;
        }

        public override void UpdateBuffers(Device device, DeviceContext deviceContext)
        {
            if(isUpdatedNeeded)
            {
                DataBox dataBox;
                dataBox = deviceContext.MapSubresource(vertexBuffer, 0, MapMode.WriteDiscard, MapFlags.None);
                Utilities.Write(dataBox.DataPointer, vertices, 0, vertices.Length);
                deviceContext.UnmapSubresource(vertexBuffer, 0);
                isUpdatedNeeded = false;
            }
        }

        public override void Select()
        {
            colour = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);

            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].Colour = colour;
            }

            isUpdatedNeeded = true;
        }

        public override void Unselect()
        {
            colour = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);

            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].Colour = colour;
            }

            isUpdatedNeeded = true;
        }
    }
}
